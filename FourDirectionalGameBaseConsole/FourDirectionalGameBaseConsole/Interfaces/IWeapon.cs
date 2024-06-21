namespace FourDirectionalGameBaseConsole.Interfaces;

using FourDirectionalGameBaseConsole.Interfaces;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Objects;
using FourDirectionalGameBaseConsole.Helper;
using FourDirectionalGameBaseConsole.Enums;

public interface IWeapon
{
    public void Attack(IEntity source, Direction direction, Map map);

    public static IWeapon FromString(string data)
    {
        string[] info = data.Split("|");
        
        if(info[0] == "Sword")
        {
            return new Sword(decimal.Parse(info[1]), int.Parse(info[2]));
        }
        
        return null!;
    }
}