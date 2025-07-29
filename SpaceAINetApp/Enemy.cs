using System;
using System.Collections.Generic;

public class Enemy
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Type { get; set; } // "2char" or "3char"
    public string Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    public bool IsAlive { get; set; } = true;
    public bool CanShoot { get; set; } = false;

    public Enemy(int x, int y, string type, string symbol, ConsoleColor color)
    {
        X = x;
        Y = y;
        Type = type;
        Symbol = symbol;
        Color = color;
    }
}

public class EnemyManager
{
    public List<Enemy> Enemies { get; private set; } = new List<Enemy>();
    private int direction = 1; // 1: 오른쪽, -1: 왼쪽
    private int moveDownStep = 1;

    public EnemyManager()
    {
        // 상단 5개: ><, oo, ><, oo, >< (Red)
        Enemies.Add(new Enemy(5, 2, "2char", "><", ConsoleColor.Red));
        Enemies.Add(new Enemy(9, 2, "2char", "oo", ConsoleColor.Red));
        Enemies.Add(new Enemy(13, 2, "2char", "><", ConsoleColor.Red));
        Enemies.Add(new Enemy(17, 2, "2char", "oo", ConsoleColor.Red));
        Enemies.Add(new Enemy(21, 2, "2char", "><", ConsoleColor.Red));
        // 하단 3개: /O\ (DarkYellow)
        Enemies.Add(new Enemy(7, 4, "3char", "/O\\", ConsoleColor.DarkYellow));
        Enemies.Add(new Enemy(15, 4, "3char", "/O\\", ConsoleColor.DarkYellow));
        Enemies.Add(new Enemy(23, 4, "3char", "/O\\", ConsoleColor.DarkYellow));
    }

    // 적 전체 좌우 이동 및 벽에 닿으면 한 줄 아래로 이동
    public void MoveEnemies(int leftBound, int rightBound)
    {
        bool needToMoveDown = false;
        foreach (var enemy in Enemies)
        {
            if (!enemy.IsAlive) continue;
            int nextX = enemy.X + direction;
            if (nextX < leftBound || nextX + enemy.Symbol.Length - 1 > rightBound)
            {
                needToMoveDown = true;
                break;
            }
        }
        if (needToMoveDown)
        {
            foreach (var enemy in Enemies)
            {
                if (!enemy.IsAlive) continue;
                enemy.Y += moveDownStep;
            }
            direction *= -1;
        }
        else
        {
            foreach (var enemy in Enemies)
            {
                if (!enemy.IsAlive) continue;
                enemy.X += direction;
            }
        }
    }

    // 적이 총알을 발사 (아래 방향)
    public List<Bullet> ShootEnemies()
    {
        var newBullets = new List<Bullet>();
        var rand = new Random();
        foreach (var enemy in Enemies)
        {
            if (!enemy.IsAlive) continue;
            // 일정 확률로 발사 (예: 10% 확률)
            if (rand.NextDouble() < 0.1)
            {
                int bulletX = enemy.X + enemy.Symbol.Length / 2;
                int bulletY = enemy.Y + 1;
                newBullets.Add(new Bullet(bulletX, bulletY, false));
            }
        }
        return newBullets;
    }
}
