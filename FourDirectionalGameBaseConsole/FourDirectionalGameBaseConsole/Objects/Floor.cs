using FourDirectionalGameBaseConsole.Interfaces;

namespace FourDirectionalGameBaseConsole.Objects;

public class Floor : IWalkable
{
    private Map Map { get; set; }
    public string TexturePath { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public char Symbol { get; set; } = ' ';
    public Floor(int x, int y, Map map)
    {
        X = x;
        Y = y;
        Map = map;
        TexturePath = "\\Textures\\Wall.jpg";
    }
}