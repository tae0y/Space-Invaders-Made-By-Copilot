using System;

public class RenderState
{
    public char[,] CharBuffer { get; set; }
    public ConsoleColor[,] ColorBuffer { get; set; }

    public RenderState(int width, int height)
    {
        CharBuffer = new char[width, height];
        ColorBuffer = new ConsoleColor[width, height];
    }
}
