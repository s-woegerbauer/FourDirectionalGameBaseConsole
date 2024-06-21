using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace FourDirectionalGameBaseConsole.Helper;

public class KeyPressHandler
{
    private readonly List<Action> _keyPressActions;
    private readonly List<VirtualKeyCode> _keyPresses;
    private bool _running;
    private readonly InputSimulator _inputSimulator;
    private readonly int _delay;

    public KeyPressHandler(List<Action> keyPressActions, List<VirtualKeyCode> keyPresses, int delay = 100)
    {
        _keyPressActions = keyPressActions;
        _keyPresses = keyPresses;
        _inputSimulator = new InputSimulator();
        _running = true;
        _delay = delay;

        Task.Run(MonitorKeyPresses);
    }

    private async Task MonitorKeyPresses()
    {
        while (true)
        {
            if (!_running) { continue; }

            List<VirtualKeyCode> pressedKeys = new List<VirtualKeyCode>();

            foreach (VirtualKeyCode key in _keyPresses)
            {
                if (_inputSimulator.InputDeviceState.IsKeyDown(key))
                {
                    pressedKeys.Add(key);
                }
            }

            foreach (var key in _keyPresses)
            {
                if (pressedKeys.Contains(key))
                {
                    HandleKeyPress(key);
                }
            }

            await Task.Delay(_delay);
        }
    }

    private void HandleKeyPress(VirtualKeyCode key)
    {
        int index = _keyPresses.IndexOf(key);
        if (index >= 0 && index < _keyPressActions.Count)
        {
            _keyPressActions[index]?.Invoke();
        }
    }
    
    public void AddKeyPressAction(VirtualKeyCode key, Action action)
    {
        _keyPresses.Add(key);
        _keyPressActions.Add(action);
    }

    public void Start()
    {
        _running = true;
    }

    public void Stop()
    {
        _running = false;
    }
}