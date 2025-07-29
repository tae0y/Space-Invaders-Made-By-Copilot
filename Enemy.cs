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
    // 이동, 스윕, 하강 등 메서드 추가 예정
}
