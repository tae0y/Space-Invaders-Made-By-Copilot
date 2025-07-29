using System;

public class Bullet
{
    public int X { get; set; }
    public int Y { get; set; }
    public bool IsPlayer { get; set; }
    public char Symbol => IsPlayer ? '^' : 'v';
    public ConsoleColor Color { get; } = ConsoleColor.White;
    public bool IsActive { get; set; } = true;

    public Bullet(int x, int y, bool isPlayer)
    {
        X = x;
        Y = y;
        IsPlayer = isPlayer;
    }

    public void Move()
    {
        if (IsPlayer) Y--;
        else Y++;
    }
}
