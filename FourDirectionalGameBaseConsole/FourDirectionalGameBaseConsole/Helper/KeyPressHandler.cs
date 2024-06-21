namespace FourDirectionalGameBaseConsole.Helper;

public class KeyPressHandler
{
    private readonly List<Action> _keyPressActions;
    private readonly List<ConsoleKey> _keyPresses;
    
    public KeyPressHandler(List<Action> keyPressActions, List<ConsoleKey> keyPresses)
    {
        _keyPressActions = keyPressActions;
        _keyPresses = keyPresses;
        
        Task.Run(() =>
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    HandleKeyPress(Console.ReadKey(true).Key);
                }
                
                Task.Delay(100).Wait();
            }
        });
    }
    
    private void HandleKeyPress(ConsoleKey key)
    {
        for (int i = 0; i < _keyPresses.Count; i++)
        {
            if (_keyPresses[i] == key)
            {
                _keyPressActions[i]();
            }
        }
    }
    
    public void AddKeyPressAction(ConsoleKey key, Action action)
    {
        _keyPresses.Add(key);
        _keyPressActions.Add(action);
    }
    
    public void RemoveKeyPressAction(ConsoleKey key)
    {
        for (int i = 0; i < _keyPresses.Count; i++)
        {
            if (_keyPresses[i] == key)
            {
                _keyPresses.RemoveAt(i);
                _keyPressActions.RemoveAt(i);
            }
        }
    }
    
    public void ClearKeyPressActions()
    {
        _keyPresses.Clear();
        _keyPressActions.Clear();
    }
}