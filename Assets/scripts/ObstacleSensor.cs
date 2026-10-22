using UnityEngine;

public class ObstacleSensor : MonoBehaviour
{
    [Header("自動讀取方塊/飛機實際大小")]
    public bool useRendererBounds = true;

    [Header("手動大小（只在讀不到 Renderer 時使用）")]
    public float halfSize = 5f;

    [Header("判定縮小比例，越小越寬容（0.5 = 只算飛機一半大小）")]
    [Range(0f, 1f)] public float planeShrink = 0.5f;

    [Header("飛機飛過方塊後多遠就刪除方塊")]
    public float destroyDistance = 30f;

    // 保留給 obspawner 設定用，避免舊版 spawner 編譯錯誤
    [HideInInspector] public float planeMargin = 0f;

    private AircraftController player;
    private Renderer playerRenderer;

    private float obsHalfX, obsHalfY, obsHalfZ;
    private float planeHalfX, planeHalfY, planeHalfZ;

    private bool hitChecked = false;
    private bool passChecked = false;

    void Start()
    {
        player = FindObjectOfType<AircraftController>();
        if (player == null)
        {
            Debug.LogWarning("找不到 AircraftController");
            enabled = false;
            return;
        }

        playerRenderer = player.GetComponentInChildren<Renderer>();

        // 方塊大小
        Renderer obsRenderer = GetComponentInChildren<Renderer>();
        if (useRendererBounds && obsRenderer != null)
        {
            Vector3 e = obsRenderer.bounds.extents;
            obsHalfX = e.x; obsHalfY = e.y; obsHalfZ = e.z;
        }
        else
        {
            obsHalfX = obsHalfY = obsHalfZ = halfSize;
        }

        // 飛機大小
        if (useRendererBounds && playerRenderer != null)
        {
            Vector3 e = playerRenderer.bounds.extents * planeShrink;
            planeHalfX = e.x; planeHalfY = e.y; planeHalfZ = e.z;
        }

        // 如果一生成就已經在飛機後方或重疊，直接忽略，避免開局誤判
        float px = player.transform.position.x;
        if (px >= transform.position.x - obsHalfX)
        {
            hitChecked = true;
            passChecked = true;
        }
    }

    void Update()
    {
        if (player == null) return;

        Vector3 p = player.transform.position;
        Vector3 o = transform.position;

        // 已處理完，飛機離開夠遠就清掉方塊
        if (passChecked)
        {
            if (p.x > o.x + destroyDistance) Destroy(gameObject);
            return;
        }

        // 階段一：飛機機頭到達方塊前表面 → 判斷是否撞上
        if (!hitChecked && (p.x + planeHalfX) >= (o.x - obsHalfX))
        {
            hitChecked = true;

            float dy = Mathf.Abs(p.y - o.y);
            float dz = Mathf.Abs(p.z - o.z);

            bool hitY = dy < (obsHalfY + planeHalfY);
            bool hitZ = dz < (obsHalfZ + planeHalfZ);

            Debug.Log($"[{name}] 判定: dy={dy:F2} (限 {obsHalfY + planeHalfY:F2}), " +
                      $"dz={dz:F2} (限 {obsHalfZ + planeHalfZ:F2}), " +
                      $"方塊半徑=({obsHalfX:F2},{obsHalfY:F2},{obsHalfZ:F2})");

            if (hitY && hitZ)
            {
                player.OnHitObstacle();
                passChecked = true;
                return;
            }
        }

        // 階段二：飛機飛過方塊中心 → 成功閃過
        if (hitChecked && p.x >= o.x)
        {
            passChecked = true;
            player.OnPassObstacle();
        }
    }
}