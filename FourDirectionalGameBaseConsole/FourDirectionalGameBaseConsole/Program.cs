using System.Text;
using FourDirectionalGameBaseConsole.Interfaces;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Objects;
using FourDirectionalGameBaseConsole.Helper;
using FourDirectionalGameBaseConsole.Enums;
using WindowsInput.Native;

namespace FourDirectionalGameBaseConsole;

public static class Programdssddwa
{
    public static void Main(string[] args)
    {
        DisableConsoleQuickEdit.Go();
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        Thread.Sleep(100);
        
        MaximizeWindow.Execute();
        var map = Map.Create(10, 10, "test", "p1");
        map.Render();
        
        KeyPressHandler keyPressHandler = new KeyPressHandler(new List<Action>(), new List<VirtualKeyCode>());
        
        keyPressHandler.AddKeyPressAction(VirtualKeyCode.VK_W, () => map.Entities[0].Move(Direction.Up, map));
        keyPressHandler.AddKeyPressAction(VirtualKeyCode.VK_A, () => map.Entities[0].Move(Direction.Left, map));
        keyPressHandler.AddKeyPressAction(VirtualKeyCode.VK_S, () => map.Entities[0].Move(Direction.Down, map));
        keyPressHandler.AddKeyPressAction(VirtualKeyCode.VK_D, () => map.Entities[0].Move(Direction.Right, map));
        keyPressHandler.AddKeyPressAction(VirtualKeyCode.LBUTTON, () => map.Entities[0].Weapon.Attack(map.Entities[0], map.Entities[0].Direction, map));

        while (true){}
    }
}