# VR 課程作業 3 - 幸運輪盤

---

## 🔗 相關連結與截圖

### 1. 專案截圖
![專案畫面](https://github.com/z9487xd/1151VR-HW3-414262466-YCJ/blob/main/image.png)


### 2. GitHub 連結
* 專案儲存庫：[點此前往 GitHub](https://github.com/z9487xd/1151VR-HW3-414262466-YCJ)

### 3. YouTube 連結
* 成果演示影片：[點此觀看 YouTube 影片]([https://youtube.com/shorts/OEY7X-R8A-w?si=_x0VHnJibzrNV08o
])

---

## 🛠️ 製作流程與操作說明

### 1. 製作流程
1. **邏輯編寫**：在 Unity 中建立輪盤物件，編寫 C# 腳本模擬加速與平滑減速物理效果。
2. **音效整合**：新增 AudioSource 元件，並在腳本的 `GetResult()` 結算處加入自動播放中獎音效的邏輯。
3. **路徑與編碼優化**：為配合 Android 打包工具限制，並在 Player Settings 中手動修正符合格式的 `Package Name`。
4. **多平台打包**：完成 Android 環境配置，成功 Build 出可用於實機安裝的 `.apk` 檔案。

### 2. 相關操作方法
* **旋轉輪盤**：在遊戲畫面中點擊「滑鼠左鍵」（或手機畫面的「單指點擊」），輪盤即會隨機決定最高轉速並開始旋轉。
* **查看結果與音效**：當輪盤因阻力完全停止時，畫面會發出中獎音效，同時可在 Unity 的 Console 視窗（或手機 Log）中即時查看到對應的吉凶判定結果。
