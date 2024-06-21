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
    public IWeapon Weapon { get; set; }
    public Direction Direction
    {
        get
        {
            switch (Symbol)
            {
                case '\u2191':
                    return Direction.Up;
                case '\u2192':
                    return Direction.Right;
                case '\u2193':
                    return Direction.Down;
                case '\u2190':
                    return Direction.Left;
                default:
                    return Direction.Up;
            }
        }
        
        set
        {
            switch (value)
            {
                case Direction.Up:
                    Symbol = '\u2191';
                    break;
                case Direction.Right:
                    Symbol = '\u2192';
                    break;
                case Direction.Down:
                    Symbol = '\u2193';
                    break;
                case Direction.Left:
                    Symbol = '\u2190';
                    break;
            }
        }
    }

    public bool Move(Direction direction, Map map)
    {
        bool returner = true;
        int prevX = X;
        int prevY = Y;
        
        switch (direction)
        {
            case Direction.Up:
                if (Y > 0 && map[X, Y - 1] is IWalkable)
                {
                    this.Symbol = '\u2191';
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
                    this.Symbol = '\u2192';
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
                    this.Symbol = '\u2193';
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
                    this.Symbol = '\u2190';
                    X--;
                }
                else
                {
                    returner = false;
                }
                break;
        }
        
        map.ResetPixel(prevX, prevY);
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
        Console.Write(Symbol);
    }
}