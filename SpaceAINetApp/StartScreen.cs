using System;

public static class StartScreen
{
    public static void Show()
    {
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;
        string title = "Space.AI.NET()";
        string subtitle = "Built with .NET + AI for galactic defense";
        string[] instructions = new string[] {
            "How to Play:",
            "←   Move Left",
            "→   Move Right",
            "SPACE   Shoot",
            "S   Take Screenshot",
            "Q   Quit",
            "Select Game Speed:",
            "[1] Slow (default)",
            "[2] Medium",
            "[3] Fast",
            "Press ENTER for default"
        };

        Console.Clear();

        // 타이틀 중앙 정렬
        int titleLeft = (width - title.Length) / 2;
        Console.SetCursorPosition(titleLeft > 0 ? titleLeft : 0, 2);
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(title);

        // 서브타이틀 중앙 정렬
        int subtitleLeft = (width - subtitle.Length) / 2;
        Console.SetCursorPosition(subtitleLeft > 0 ? subtitleLeft : 0, 3);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(subtitle);

        // 조작법 및 속도 옵션 좌측 정렬
        int startY = 5;
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < instructions.Length; i++)
        {
            Console.SetCursorPosition(2, startY + i);
            Console.WriteLine(instructions[i]);
        }
        Console.ResetColor();
    }
}
