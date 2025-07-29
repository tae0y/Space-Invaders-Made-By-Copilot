## 1차 디버깅
- [x] ScreenshotService.cs: System.Drawing 네임스페이스 관련 오류 (Bitmap, Graphics, Font, SolidBrush, ImageFormat 등)
- [x] ScreenshotService.cs: System.Drawing.Common 패키지 참조 필요
- [x] GameManager.cs: null을 허용하지 않는 필드 경고 (currentBuffer, colorBuffer, prevBuffer, prevColorBuffer)
- [x] GameManager.cs: null 가능 참조에 대한 역참조 경고 (CS8602, CS8619)
- [x] ScreenshotService.cs: System.Drawing.* API가 Windows에서만 동작하는 경고 (CA1416) - MacOS에서는 스크린샷 기능이 제한됨. (경고 무시 또는 대체 구현 필요)

## 2차 디버깅
- [x] GameManager.cs: 컴파일 오류 (메서드 중첩, 중괄호 위치, 한정자 오류 등) 수정
- [ ] dotnet run 시 런타임 오류 발생 시 상세 메시지 확인 및 원인 분석
- [ ] Enemy, Bullet, Player가 정상적으로 표시되는지 확인 및 렌더링 로직 점검
- [ ] 모든 엔티티가 정상적으로 동작하도록 테스트 및 수정

## 3차 디버깅
- 각 파일별 요구사항 충족 여부 검증 완료
- MacOS 환경에서 ScreenshotService의 System.Drawing 사용 호환성 확인 필요
- 실제 게임 플레이 중 모든 엔티티(플레이어, 적, 총알) 정상 렌더링 및 동작 확인
- 추가적인 예외 상황(입력, 버퍼, 색상 등) 테스트 필요