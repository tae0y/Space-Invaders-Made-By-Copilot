using System;

public class Player
{
    public int X { get; set; }
    public int Y { get; set; }
    public int MaxBullets { get; } = 3;
    public int ActiveBullets { get; set; } = 0;
    public ConsoleColor Color { get; } = ConsoleColor.Cyan;
    public char Symbol { get; } = 'A';

    public Player(int startX, int startY)
    {
        X = startX;
        Y = startY;
    }

    public void MoveLeft(int minX)
    {
        if (X > minX) X--;
    }
    public void MoveRight(int maxX)
    {
        if (X < maxX) X++;
    }
    public bool CanShoot()
    {
        return ActiveBullets < MaxBullets;
    }
    public void Shoot()
    {
        if (CanShoot()) ActiveBullets++;
    }
    public void BulletDestroyed()
    {
        if (ActiveBullets > 0) ActiveBullets--;
    }
}
