using System;

class Program
{
    static void Main(string[] args)
    {
        // UTF-8 인코딩 설정 (박스문자 등 유니코드 지원)
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // 커서 숨김
        Console.CursorVisible = false;

        // StartScreen 출력
        StartScreen.Show();

        // 속도 선택 입력
        int speed = 1; // 기본값: Slow
        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        if (keyInfo.Key == ConsoleKey.D2 || keyInfo.Key == ConsoleKey.NumPad2)
            speed = 2;
        else if (keyInfo.Key == ConsoleKey.D3 || keyInfo.Key == ConsoleKey.NumPad3)
            speed = 3;
        // ENTER 또는 기타 입력은 기본값 유지

        // 콘솔 클리어
        Console.Clear();

        // 게임 루프 진입
        GameManager.RunGameLoop(speed);
    }
}
