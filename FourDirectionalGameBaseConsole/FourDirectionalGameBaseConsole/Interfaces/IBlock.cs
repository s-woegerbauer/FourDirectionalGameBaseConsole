using System.Drawing;
using System.Net.Mime;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Helper;
using Spectre.Console;

namespace FourDirectionalGameBaseConsole.Interfaces;

public interface IBlock
{
    public string TexturePath { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }

    public void Draw()
    {
        Console.SetCursorPosition(X, Y);
        Console.ForegroundColor = Color;
        Console.Write(Symbol);
        Console.ForegroundColor = ConsoleColor.White;

        // TODO: Implement Image Drawing
        //WinAPI.DrawImage(TexturePath,2, 2, X * 2 + 1, Y * 2 + 1);
    }
}