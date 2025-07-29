## 1차 디버깅
- [x] ScreenshotService.cs: System.Drawing 네임스페이스 관련 오류 (Bitmap, Graphics, Font, SolidBrush, ImageFormat 등)
- [x] ScreenshotService.cs: System.Drawing.Common 패키지 참조 필요
- [ ] GameManager.cs: null을 허용하지 않는 필드 경고 (currentBuffer, colorBuffer, prevBuffer, prevColorBuffer)
- [ ] ScreenshotService.cs: System.Drawing.* API가 Windows에서만 동작하는 경고 (CA1416)
