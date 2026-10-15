using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Interactive vertical menu control with keyboard navigation and selection events.
    /// </summary>
    public sealed class Menu : View
    {
        private readonly string[] _items;
        private int _selectedIndex = 0;
        public event Action<int, string> ItemSelected;

        public Menu(int x, int y, int width, string[] items) : base(x, y, width, items.Length + 2)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        public override void Draw(char[,] buffer)
        {
            string borderTop = IsFocused ? "╔" + new string('═', Width - 2) + "╗" : "┌" + new string('─', Width - 2) + "┐";
            string borderBot = IsFocused ? "╚" + new string('═', Width - 2) + "╝" : "└" + new string('─', Width - 2) + "┘";

            WriteToBuffer(buffer, X, Y, borderTop);

            for (int i = 0; i < _items.Length; i++)
            {
                string marker = (IsFocused && i == _selectedIndex) ? "► " : "  ";
                string itemText = $"{marker}{_items[i]}";
                
                // Pad and fit into width
                int innerWidth = Width - 4;
                if (itemText.Length > innerWidth)
                {
                    itemText = itemText.Substring(0, innerWidth);
                }
                else
                {
                    itemText = itemText.PadRight(innerWidth);
                }

                string row = $"│ {itemText} │";
                WriteToBuffer(buffer, X, Y + 1 + i, row);
            }

            WriteToBuffer(buffer, X, Y + _items.Length + 1, borderBot);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (_items.Length == 0) return;

            if (key.Key == ConsoleKey.UpArrow)
            {
                _selectedIndex = (_selectedIndex - 1 + _items.Length) % _items.Length;
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                _selectedIndex = (_selectedIndex + 1) % _items.Length;
            }
            else if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
            {
                ItemSelected?.Invoke(_selectedIndex, _items[_selectedIndex]);
            }
        }
    }
}