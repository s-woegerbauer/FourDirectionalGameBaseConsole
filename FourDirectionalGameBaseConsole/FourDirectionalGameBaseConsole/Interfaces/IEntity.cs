namespace FourDirectionalGameBaseConsole.Interfaces;

using FourDirectionalGameBaseConsole.Interfaces;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Objects;
using FourDirectionalGameBaseConsole.Helper;
using FourDirectionalGameBaseConsole.Enums;

public interface IEntity
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public decimal Health { get; set; }
    public decimal MaxHealth { get; set; }
    public decimal Resistance { get; set; }
    public char Symbol { get; set; }
    public ConsoleColor Color { get; set; }
    
    public IWeapon Weapon { get; set; }

    public bool Move(Direction direction, Map map)
    {
        bool returner = true;
        map[X, Y].Draw();
        
        switch (direction)
        {
            case Direction.Up:
                if (Y > 0 && map[X, Y - 1] is IWalkable)
                {
                    Y--;
                }
                else
                {
                    returner = false;
                }
                break;
            case Direction.Right:
                if (X < map.Width - 1  && map[X + 1, Y] is IWalkable)
                {
                    X++;
                }
                else
                {
                    returner = false;
                }
                break;
            case Direction.Down:
                if (Y < map.Height - 1 && map[X, Y + 1] is IWalkable)
                {
                    Y++;
                }
                else
                {
                    returner = false;
                }
                break;
            case Direction.Left:
                if (X > 0 && map[X - 1, Y] is IWalkable)
                {
                    X--;
                }
                else
                {
                    returner = false;
                }
                break;
        }
        
        Draw();

        return returner;
    }

    public void TakeDamage(decimal damage)
    {
        decimal effectiveDamage = damage / Resistance;
        Health -= effectiveDamage;
        if (Health <= 0)
        {
            Die();
        }
    }
    
    public void Attack(Direction direction, Map map)
    {
        this.Weapon.Attack(this, direction, map);
    }
    
    public void Die();

    public void Draw()
    {
        Console.SetCursorPosition(X, Y);
        Console.ForegroundColor = Color;
        Console.Write(Symbol);
        Console.ForegroundColor = ConsoleColor.White;
    }
}