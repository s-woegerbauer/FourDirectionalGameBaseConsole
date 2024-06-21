namespace FourDirectionalGameBaseConsole.Objects;

using FourDirectionalGameBaseConsole.Interfaces;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Objects;
using FourDirectionalGameBaseConsole.Helper;
using FourDirectionalGameBaseConsole.Enums;

public class Player : IEntity
{
    public string Name { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public decimal Health { get; set; }
    public decimal Resistance { get; set; }
    public decimal MaxHealth { get; set; }
    
    public IWeapon Weapon { get; set; }
    
    public Player(string name, int x, int y, decimal health, decimal resistance, IWeapon weapon, decimal maxHealth)
    {
        Name = name;
        X = x;
        Y = y;
        Health = health;
        Resistance = resistance;
        Weapon = weapon;
        MaxHealth = maxHealth;
    }

    public void TakeDamage(decimal damage)
    {
        throw new NotImplementedException();
    }

    public void Attack(IEntity entity)
    {
        throw new NotImplementedException();
    }

    public void Die()
    {
        throw new NotImplementedException();
    }
}