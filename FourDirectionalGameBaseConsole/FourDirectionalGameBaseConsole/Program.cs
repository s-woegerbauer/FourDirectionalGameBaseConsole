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
        
        
        Console.ReadKey();
        List<IEntity> entities = new List<IEntity>();
        entities.Add(new Player("Player", 1, 1, 100, 1, new Sword(20, 500)));
        Map map = new Map(entities, 10, 10);
        
        map.Render();
    }
}