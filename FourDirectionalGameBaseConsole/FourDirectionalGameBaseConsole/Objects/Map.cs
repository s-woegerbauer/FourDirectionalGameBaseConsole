namespace FourDirectionalGameBaseConsole.Objects;

using FourDirectionalGameBaseConsole.Interfaces;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Objects;
using FourDirectionalGameBaseConsole.Helper;
using FourDirectionalGameBaseConsole.Enums;

public class Map
{
    private IBlock[,] Blocks { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<IEntity> Entities { get; set; }

    public Map(List<IEntity> entities, int width, int height)
    {
        Width = width;
        Height = height;
        Entities = entities;
        Blocks = new IBlock[width, height];
        FillWithWall();
    }
    
    private void FillWithFloor()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Blocks[x, y] = new Floor(x, y, this);
            }
        }
    }
    
    private void FillWithWall()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                Blocks[x, y] = new Wall(x, y, this);
            }
        }
    }
    
    public IBlock this[int x, int y]
    {
        get => Blocks[x, y];
        set => Blocks[x, y] = value;
    }
    
    public void SpawnEntity(IEntity entity)
    {
        Entities.Add(entity);
    }
    
    public void RemoveEntity(IEntity entity)
    {
        Entities.Remove(entity);
    }

    public void Render()
    {
        foreach (var block in Blocks)
        {
            block.Draw();
        }
    }
}