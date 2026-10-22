using UnityEngine;

public class AircraftController : MonoBehaviour
{
    [Header("Movement Speed")]
    public float forwardSpeed = 10f;
    public float controlSpeed = 8f;

    [Header("Swipe Sensitivity Boost")]
    public float swipeSensitivityX = 5f;
    public float swipeSensitivityY = 5f;

    [Header("Boundary Limits")]
    public float minY = -2f;
    public float maxY = 6f;
    public float minZ = -5f;
    public float maxZ = 5f;

    [Header("Audio Settings")]
    public AudioSource audioSource;   // 引擎聲專用（循環播放）
    public AudioSource sfxSource;     // 音效專用（撞擊、經過）
    public AudioClip flyingSound;
    public AudioClip hitSound;
    public AudioClip passSound;

    private bool isDead = false;

    void Start()
    {
        if (audioSource != null && flyingSound != null)
        {
            audioSource.clip = flyingSound;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void Update()
    {
        if (isDead) return;

        Vector3 currentPos = transform.position;
        currentPos.x += forwardSpeed * Time.deltaTime;

        Vector2 inputDelta = GetSwipeDelta();

        currentPos.y += inputDelta.y * controlSpeed * swipeSensitivityY * Time.deltaTime;
        currentPos.z -= inputDelta.x * controlSpeed * swipeSensitivityX * Time.deltaTime;

        currentPos.y = Mathf.Clamp(currentPos.y, minY, maxY);
        currentPos.z = Mathf.Clamp(currentPos.z, minZ, maxZ);

        transform.position = currentPos;
    }

    Vector2 GetSwipeDelta()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                return touch.deltaPosition / 10f;
            }
        }

        if (Input.GetMouseButton(0))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            return new Vector2(mouseX, mouseY);
        }

        return Vector2.zero;
    }

    // 由 ObstacleSensor 呼叫：撞到方塊
    public void OnHitObstacle()
    {
        if (isDead) return;

        isDead = true;
        forwardSpeed = 0;

        if (audioSource != null) audioSource.Stop();   // 停止引擎聲
        if (sfxSource != null && hitSound != null)
            sfxSource.PlayOneShot(hitSound);           // 撞擊聲

        Debug.Log("Game Over! Crashed into an obstacle.");
    }

    // 由 ObstacleSensor 呼叫：成功閃過方塊
    public void OnPassObstacle()
    {
        if (isDead) return;

        if (sfxSource != null && passSound != null)
            sfxSource.PlayOneShot(passSound);          // 經過聲

        Debug.Log("Successfully dodged the obstacle!");
    }
}