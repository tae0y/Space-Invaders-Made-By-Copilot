using System;
using System.Collections.Generic;

public static class GameManager
{
    // 게임 상태 관련 변수들 (예시)
    private static int score = 0;
    private static int timeSec = 0;
    private static int playerBullets = 3;
    // ...엔티티 리스트 등 추가 예정...

    // 더블 버퍼용
    private static char[,] currentBuffer;
    private static ConsoleColor[,] colorBuffer;
    private static char[,] prevBuffer;
    private static ConsoleColor[,] prevColorBuffer;

    public static void RunGameLoop(int speed)
    {
        int width = Console.WindowWidth;
        int height = Console.WindowHeight;
        int boxLeft = 1, boxTop = 1, boxWidth = width - 2, boxHeight = height - 2;

        // 버퍼 초기화
        currentBuffer = new char[width, height];
        colorBuffer = new ConsoleColor[width, height];
        prevBuffer = new char[width, height];
        prevColorBuffer = new ConsoleColor[width, height];

        // 커서 숨김
        Console.CursorVisible = false;

        DateTime startTime = DateTime.Now;
        bool running = true;
        while (running)
        {
            // 입력 처리
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q)
                    running = false;
                // ...플레이어 이동/발사 등 추가...
            }

            // 엔티티 업데이트 (플레이어, 적, 총알 등)
            // ...구현 예정...

            // UI 정보 갱신
            timeSec = (int)(DateTime.Now - startTime).TotalSeconds;

            // 버퍼에 박스/엔티티/UI 그리기
            DrawBox(boxLeft, boxTop, boxWidth, boxHeight);
            DrawUI(boxLeft, boxTop);
            // ...플레이어/적/총알 등 추가...

            // 더블 버퍼 렌더링
            RenderDiff(width, height);

            // 프레임 속도 조절
            System.Threading.Thread.Sleep(GetFrameDelay(speed));
        }
    }

    private static int GetFrameDelay(int speed)
    {
        return speed switch
        {
            1 => 80, // Slow
            2 => 40, // Medium
            3 => 20, // Fast
            _ => 80
        };
    }

    private static void DrawBox(int left, int top, int w, int h)
    {
        // 박스 테두리 (유니코드 박스문자)
        currentBuffer[left, top] = '┌';
        currentBuffer[left + w - 1, top] = '┐';
        currentBuffer[left, top + h - 1] = '└';
        currentBuffer[left + w - 1, top + h - 1] = '┘';
        for (int x = left + 1; x < left + w - 1; x++)
        {
            currentBuffer[x, top] = '─';
            currentBuffer[x, top + h - 1] = '─';
        }
        for (int y = top + 1; y < top + h - 1; y++)
        {
            currentBuffer[left, y] = '│';
            currentBuffer[left + w - 1, y] = '│';
        }
    }

    private static void DrawUI(int left, int top)
    {
        string ui = $"Score: {score:D4}   Time: {timeSec:D2}s   Bullets: {playerBullets}/3";
        for (int i = 0; i < ui.Length && left + 2 + i < currentBuffer.GetLength(0); i++)
        {
            currentBuffer[left + 2 + i, top + 1] = ui[i];
            colorBuffer[left + 2 + i, top + 1] = ConsoleColor.White;
        }
    }

    private static void RenderDiff(int width, int height)
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (currentBuffer[x, y] != prevBuffer[x, y] || colorBuffer[x, y] != prevColorBuffer[x, y])
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = colorBuffer[x, y];
                    Console.Write(currentBuffer[x, y] == '\0' ? ' ' : currentBuffer[x, y]);
                }
                prevBuffer[x, y] = currentBuffer[x, y];
                prevColorBuffer[x, y] = colorBuffer[x, y];
                currentBuffer[x, y] = '\0';
                colorBuffer[x, y] = ConsoleColor.Black;
            }
        }
        Console.ResetColor();
    }

    // ScreenshotService 연동용: 현재 렌더 상태 반환
    public static (char[,], ConsoleColor[,]) GetRenderState()
    {
        return (prevBuffer, prevColorBuffer);
    }
}
