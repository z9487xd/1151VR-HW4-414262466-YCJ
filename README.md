# VR 課程作業 4 - 飛機

---

## 🔗 相關連結與截圖

### 1. 專案截圖
![專案畫面](https://github.com/z9487xd/1151VR-HW4-414262466-YCJ/blob/main/image.png?raw=true)

### 2. GitHub 連結
* 專案儲存庫：[點此前往 GitHub](https://github.com/z9487xd/1151VR-HW4-414262466-YCJ)

### 3. YouTube 連結
* 成果演示影片：[點此觀看 YouTube 影片](https://youtube.com/shorts/vFX4fjEupp8)

---

## 🛠️ 製作流程與操作說明

### 1. 製作流程
1. **邏輯編寫**：在 Unity 中建立飛機與方塊物件，編寫 `AircraftController` 讓飛機沿 X 軸自動前進，並以滑動控制 Y 軸（上下）與 Z 軸（左右）；編寫 `obspawner` 沿 X 軸前方隨機生成障礙方塊。
2. **碰撞與音效整合**：新增兩個 AudioSource（引擎聲、音效專用），並以 `ObstacleSensor` 依座標距離判定「撞擊」或「閃過」，分別播放爆炸聲與通過聲，不依賴 Rigidbody 與 Trigger。
3. **輸入設定優化**：將 Active Input Handling 改為 `Input Manager (Old)`，並移除 Input System 套件，避免 Android 平台輸入異常。
4. **多平台打包**：在 Player Settings 修正符合格式的 `Package Name`，完成 Android 環境配置，Build 出可實機安裝的 `.apk` 檔案。

### 2. 相關操作方法
* **控制飛機**：飛機會自動向前飛行。在畫面上按住「滑鼠左鍵」拖曳（手機為「單指滑動」），上下滑動控制飛行高度，左右滑動控制左右位置。
* **閃躲與音效**：成功從方塊旁邊飛過時會播放通過音效；撞進方塊時會播放爆炸音效、停止引擎聲，飛機停下並判定 Game Over，可在 Unity 的 Console 視窗（或手機 Log）查看對應訊息。