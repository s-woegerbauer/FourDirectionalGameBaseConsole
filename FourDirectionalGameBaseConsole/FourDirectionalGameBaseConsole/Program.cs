using FourDirectionalGameBaseConsole.Interfaces;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Objects;
using FourDirectionalGameBaseConsole.Helper;
using FourDirectionalGameBaseConsole.Enums;

namespace FourDirectionalGameBaseConsole;

public static class Program
{
    public static void Main(string[] args)
    {
        MaximizeWindow.Execute();
        var map = Map.Create(10, 10, "test", "p1");
        map.Render();
    }
}