namespace FourDirectionalGameBaseConsole.Objects;

using FourDirectionalGameBaseConsole.Interfaces;
using FourDirectionalGameBaseConsole.Enums;
using FourDirectionalGameBaseConsole.Objects;
using FourDirectionalGameBaseConsole.Helper;
using FourDirectionalGameBaseConsole.Enums;

public class Map
{
    private IBlock[,] Blocks { get; set; }
    public int Width => Blocks.GetLength(0);
    public int Height => Blocks.GetLength(1);
    public List<IEntity> Entities { get; set; }

    public Map(List<IEntity> entities, int width, int height)
    {
        Entities = entities;
        Blocks = new IBlock[width, height];
        Init();
    }

    private void FromCsv(string filePath)
    {
        List<string> input = File.ReadAllLines(filePath).ToList();
        int width = input[0].Split(';').Length;
        int height = input.Count;
        
        Blocks = new IBlock[width, height];
        
        for (int y = 0; y < Height; y++)
        {
            string[] line = input[y].Split(';');
            for (int x = 0; x < Width; x++)
            {
                switch (line[x])
                {
                    case "Floor":
                        Blocks[x, y] = new Floor(x, y, this);
                        break;
                    case "Wall":
                        Blocks[x, y] = new Wall(x, y, this);
                        break;
                    default:
                        Blocks[x, y] = new Floor(x, y, this);
                        break;
                }
            }
        }
    }

    public static Map Read(string name)
    {
        // Read map file
        Map map = new Map(new List<IEntity>(), 0, 0);
        map.FromCsv(Directory.GetCurrentDirectory() + "/Saves/" + name + "/map.csv");
        
        // Read entities file
        map.ReadEntities(Directory.GetCurrentDirectory() + "/Saves/" + name + "/entities.csv");

        return map;
    }
    
    public static Map Create(int width, int height, string mapName, string playerName)
    {
        Map map = new Map(new List<IEntity>(), width, height);
        map.Entities.Add(new Player(playerName, 1, 1, 100, 0, new Sword(10, 1000), 100));

        // Create base directory for saves if not available
        if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Saves"))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Saves");
        }
        
        // Create map directory if not available
        if (!Directory.Exists(Directory.GetCurrentDirectory() + "/Saves/" + mapName))
        {
            Directory.CreateDirectory(Directory.GetCurrentDirectory() + "/Saves/" + mapName);
        }
        
        // Create map file if not available
        if (!File.Exists(Directory.GetCurrentDirectory() + "/Saves/" + mapName + "/map.csv"))
        {
            using FileStream fs = File.Create(Directory.GetCurrentDirectory() + "/Saves/" + mapName + "/map.csv");
            map.ToCsv(fs);
            fs.Close();
        }
        
        // Create entities file if not available
        if (!File.Exists(Directory.GetCurrentDirectory() + "/Saves/" + mapName + "/entities.csv"))
        {
            using FileStream fs = File.Create(Directory.GetCurrentDirectory() + "/Saves/" + mapName + "/entities.csv");
            map.WriteEntities(fs);
            fs.Close();
        }

        return map;
    }
    
    private void ToCsv(FileStream fs)
    {
        List<string> output = new List<string>();
        for (int y = 0; y < Height; y++)
        {
            string line = "";
            for (int x = 0; x < Width; x++)
            {
                line += Blocks[x, y].GetType().Name + ";";
            }
            output.Add(line);
        }

        using StreamWriter sw = new StreamWriter(fs);
        foreach (string line in output)
        {
            sw.WriteLine(line);
        }
    }

    private void ReadEntities(string filePath)
    {
        List<string> input = File.ReadAllLines(filePath).ToList();
        
        foreach (var line in input)
        {
            string[] entity = line.Split(';');
            switch (entity[0])
            {
                case "Player":
                    Entities.Add(new Player(entity[0], int.Parse(entity[1]), int.Parse(entity[2]), 
                        decimal.Parse(entity[3]), decimal.Parse(entity[4]), 
                        IWeapon.FromString(entity[5]), decimal.Parse(entity[6])));
                    break;
            }
        }
    }
    
    private void WriteEntities(FileStream fs)
    {
        List<string> output = new List<string>();
        foreach (var entity in Entities)
        {
            string line = entity.GetType().Name + ";" + entity.X + ";" + entity.Y + ";" + entity.Health + ";" + entity.MaxHealth + ";" + entity.Weapon.ToString();
            output.Add(line);
        }
        
        using StreamWriter sw = new StreamWriter(fs);
        foreach (string line in output)
        {
            sw.WriteLine(line);
        }
    }

    private void Init()
    {
        for(int i = 0; i < Width; i++)
        {
            for(int j = 0; j < Height; j++)
            {
                if(i < 1 || j < 1 || i >= Width - 1 || j >= Height - 1)
                {
                    Blocks[i, j] = new Wall(i, j, this);
                    continue;
                }
                
                Blocks[i, j] = new Floor(i, j, this);
            }
        }
    }
    
    public IBlock this[int x, int y]
    {
        get => Blocks[x, y];
        set => Blocks[x, y] = value;
    }

    public void Render()
    {
        foreach (var block in Blocks)
        {
            block.Draw();
        }

        foreach (var entity in Entities)
        {
            entity.Draw();
        }
    }

    public void ResetPixel(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        this[x,y].Draw();
        foreach(var entity in Entities)
        {
            if(entity.X == x && entity.Y == y)
            {
                entity.Draw();
            }
        }
    }
}