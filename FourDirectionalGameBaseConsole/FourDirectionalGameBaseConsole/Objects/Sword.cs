using FourDirectionalGameBaseConsole.Interfaces;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Objects;
using FourDirectionalGameBaseConsole.Helper;
using FourDirectionalGameBaseConsole.Enums;

namespace FourDirectionalGameBaseConsole.Objects;

public class Sword : IWeapon
{
    public const string Name = "Sword";
    public decimal Damage { get; set; }
    public int MsDelay { get; set; }
    public DateTime LastAttack { get; set; }

    public Sword(decimal damage, int msDelay)
    {
        Damage = damage;
        MsDelay = msDelay;
        LastAttack = DateTime.Now;
    }

    public void Attack(IEntity source, Direction direction, Map map)
    {
        if(DateTime.Now - LastAttack < TimeSpan.FromMilliseconds(MsDelay))
        {
            return;
        }
        
        LastAttack = DateTime.Now;
        
        int targetX = source.X;
        int targetY = source.Y;

        switch (direction)
        {
            case Direction.Up:
                targetY--;
                break;
            case Direction.Down:
                targetY++;
                break;
            case Direction.Left:
                targetX--;
                break;
            case Direction.Right:
                targetX++;
                break;
        }

        var targets = map.Entities.Where(e => e.X == targetX && e.Y == targetY).ToList();

        foreach (var target in targets)
        {
            target.TakeDamage(Damage);
        }

        Console.SetCursorPosition(targetX, targetY);
        Console.Write('\u2694');

        Task.Run(() =>
        {
            Thread.Sleep(500);
            map.ResetPixel(targetX, targetY);
        });
    }
}