using System;
using System.Collections.Generic;

public static class GameManager
{
    // 게임 상태 관련 변수들
    private static int score = 0;
    private static int timeSec = 0;
    private static int playerBullets = 3;

    // 엔티티
    private static Player player;
    private static EnemyManager enemyManager;
    private static List<Bullet> bullets = new List<Bullet>();

    // 더블 버퍼용
    private static char[,]? currentBuffer;
    private static ConsoleColor[,]? colorBuffer;
    private static char[,]? prevBuffer;
    private static ConsoleColor[,]? prevColorBuffer;

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

        // 엔티티 초기화
        int playerStartX = width / 2;
        int playerStartY = height - 4;
        player = new Player(playerStartX, playerStartY);
        enemyManager = new EnemyManager();
        bullets.Clear();

        DateTime startTime = DateTime.Now;
        bool running = true;
        int enemyMoveTick = 0;
        int enemyMoveInterval = 10; // 10프레임마다 적 이동
        while (running)
        {
            // 입력 처리
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Q)
                    running = false;
                else if (key.Key == ConsoleKey.LeftArrow)
                    player.MoveLeft(2); // 박스 안에서만 이동
                else if (key.Key == ConsoleKey.RightArrow)
                    player.MoveRight(width - 3);
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    if (player.CanShoot())
                    {
                        bullets.Add(new Bullet(player.X, player.Y - 1, true));
                        player.Shoot();
                    }
                }
            }

            // 적 이동 (일정 프레임마다)
            enemyMoveTick++;
            if (enemyMoveTick >= enemyMoveInterval)
            {
                enemyManager.MoveEnemies(2, width - 3);
                enemyMoveTick = 0;
                // 적 총알 발사 (적 이동 주기와 동일하게)
                var enemyBullets = enemyManager.ShootEnemies();
                bullets.AddRange(enemyBullets);
            }

            // 총알 이동 및 삭제
            for (int i = bullets.Count - 1; i >= 0; i--)
            {
                bullets[i].Move();
                if (bullets[i].Y < 2 || bullets[i].Y > height - 2)
                {
                    if (bullets[i].IsPlayer) player.BulletDestroyed();
                    bullets.RemoveAt(i);
                    continue;
                }

                // 총알-적 충돌 판정 (플레이어 총알만)
                if (bullets[i].IsPlayer)
                {
                    foreach (var enemy in enemyManager.Enemies)
                    {
                        if (!enemy.IsAlive) continue;
                        // 적 심볼 길이만큼 충돌 체크
                        for (int k = 0; k < enemy.Symbol.Length; k++)
                        {
                            if (bullets[i].X == enemy.X + k && bullets[i].Y == enemy.Y)
                            {
                                enemy.IsAlive = false;
                                bullets.RemoveAt(i);
                                player.BulletDestroyed();
                                score += 100;
                                goto BulletLoopBreak;
                            }
                        }
                    }
                }
                // 적 총알-플레이어 충돌 판정
                else
                {
                    if (bullets[i].X == player.X && bullets[i].Y == player.Y)
                    {
                        // 플레이어 피격 처리 (여기서는 단순히 게임 종료)
                        running = false;
                        bullets.RemoveAt(i);
                        goto BulletLoopBreak;
                    }
                }
                BulletLoopBreak: ;
            }

            // UI 정보 갱신
            timeSec = (int)(DateTime.Now - startTime).TotalSeconds;

            // 버퍼에 박스/엔티티/UI 그리기
            DrawBox(boxLeft, boxTop, boxWidth, boxHeight);
            DrawUI(boxLeft, boxTop);
            DrawPlayer();
            DrawEnemies();
            DrawBullets();

            // 더블 버퍼 렌더링
            RenderDiff(width, height);

            // 프레임 속도 조절
            System.Threading.Thread.Sleep(GetFrameDelay(speed));
        }
}

    private static void DrawPlayer()
    {
        if (currentBuffer == null || colorBuffer == null || player == null) return;
        currentBuffer[player.X, player.Y] = player.Symbol;
        colorBuffer[player.X, player.Y] = player.Color;
    }

    private static void DrawEnemies()
    {
        if (currentBuffer == null || colorBuffer == null || enemyManager == null) return;
        foreach (var enemy in enemyManager.Enemies)
        {
            if (!enemy.IsAlive) continue;
            for (int i = 0; i < enemy.Symbol.Length; i++)
            {
                int ex = enemy.X + i;
                int ey = enemy.Y;
                if (ex >= 2 && ex < currentBuffer.GetLength(0) - 2 && ey >= 2 && ey < currentBuffer.GetLength(1) - 2)
                {
                    currentBuffer[ex, ey] = enemy.Symbol[i];
                    colorBuffer[ex, ey] = enemy.Color;
                }
            }
        }
    }

    private static void DrawBullets()
    {
        if (currentBuffer == null || colorBuffer == null) return;
        foreach (var bullet in bullets)
        {
            if (!bullet.IsActive) continue;
            int bx = bullet.X;
            int by = bullet.Y;
            if (bx >= 2 && bx < currentBuffer.GetLength(0) - 2 && by >= 2 && by < currentBuffer.GetLength(1) - 2)
            {
                currentBuffer[bx, by] = bullet.Symbol;
                colorBuffer[bx, by] = bullet.Color;
            }
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
        if (currentBuffer == null) return;
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
        if (currentBuffer == null || colorBuffer == null) return;
        for (int i = 0; i < ui.Length && left + 2 + i < currentBuffer.GetLength(0); i++)
        {
            currentBuffer[left + 2 + i, top + 1] = ui[i];
            colorBuffer[left + 2 + i, top + 1] = ConsoleColor.White;
        }
    }

    private static void RenderDiff(int width, int height)
    {
        if (currentBuffer == null || colorBuffer == null || prevBuffer == null || prevColorBuffer == null) return;
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
        return (prevBuffer ?? new char[1,1], prevColorBuffer ?? new ConsoleColor[1,1]);
    }
}
