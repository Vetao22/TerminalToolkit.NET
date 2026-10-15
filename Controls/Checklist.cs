using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Multi-selection checklist control.
    /// </summary>
    public sealed class Checklist : View
    {
        private readonly string[] _items;
        private readonly bool[] _checked;
        private int _selectedIndex = 0;

        public Checklist(int x, int y, string[] items) : base(x, y, 30, items.Length + 2)
        {
            _items = items; 
            _checked = new bool[items.Length];
        }

        public override void Draw(char[,] buffer)
        {
            WriteToBuffer(buffer, X, Y, "--- Checklist (Tab to focus) ---");
            for (int i = 0; i < _items.Length; i++)
            {
                string prefix = _checked[i] ? "[X]" : "[ ]";
                string focusMarker = (IsFocused && i == _selectedIndex) ? "> " : "  ";
                WriteToBuffer(buffer, X, Y + 1 + i, $"{focusMarker}{prefix} {_items[i]}");
            }
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.UpArrow)
            {
                _selectedIndex = Math.Max(0, _selectedIndex - 1);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                _selectedIndex = Math.Min(_items.Length - 1, _selectedIndex + 1);
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                _checked[_selectedIndex] = !_checked[_selectedIndex];
            }
        }
    }
}