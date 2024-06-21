namespace FourDirectionalGameBaseConsole.Interfaces;

using FourDirectionalGameBaseConsole.Interfaces;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Objects;
using FourDirectionalGameBaseConsole.Helper;
using FourDirectionalGameBaseConsole.Enums;

public interface IWeapon
{
    public void Attack(IEntity source, Direction direction, Map map);
}