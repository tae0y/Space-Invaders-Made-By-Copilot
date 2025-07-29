using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public static class ScreenshotService
{
    private static readonly string Folder = "screenshoots";
    private static int shotCount = 0;

    public static void InitFolder()
    {
        if (Directory.Exists(Folder))
            Directory.Delete(Folder, true);
        Directory.CreateDirectory(Folder);
    }

    public static void Capture((char[,], ConsoleColor[,]) renderState)
    {
        int width = renderState.Item1.GetLength(0);
        int height = renderState.Item1.GetLength(1);
        using (Bitmap bmp = new Bitmap(width * 16, height * 16))
        using (Graphics g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.Black);
            using (Font font = new Font("Consolas", 14, FontStyle.Regular, GraphicsUnit.Pixel))
            {
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        char ch = renderState.Item1[x, y];
                        if (ch == '\0' || ch == ' ') continue;
                        Color color = ToColor(renderState.Item2[x, y]);
                        g.DrawString(ch.ToString(), font, new SolidBrush(color), x * 16, y * 16);
                    }
                }
            }
            string fileName = Path.Combine(Folder, $"screenshot_{++shotCount}.png");
            bmp.Save(fileName, ImageFormat.Png);
        }
    }

    private static Color ToColor(ConsoleColor cc)
    {
        return cc switch
        {
            ConsoleColor.Black => Color.Black,
            ConsoleColor.DarkBlue => Color.DarkBlue,
            ConsoleColor.DarkGreen => Color.DarkGreen,
            ConsoleColor.DarkCyan => Color.DarkCyan,
            ConsoleColor.DarkRed => Color.DarkRed,
            ConsoleColor.DarkMagenta => Color.DarkMagenta,
            ConsoleColor.DarkYellow => Color.FromArgb(128, 128, 0),
            ConsoleColor.Gray => Color.Gray,
            ConsoleColor.DarkGray => Color.DarkGray,
            ConsoleColor.Blue => Color.Blue,
            ConsoleColor.Green => Color.Green,
            ConsoleColor.Cyan => Color.Cyan,
            ConsoleColor.Red => Color.Red,
            ConsoleColor.Magenta => Color.Magenta,
            ConsoleColor.Yellow => Color.Yellow,
            ConsoleColor.White => Color.White,
            _ => Color.White
        };
    }
}
