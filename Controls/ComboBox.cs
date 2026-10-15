using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Dropdown selection control.
    /// </summary>
    public sealed class ComboBox : View
    {
        private readonly string[] _items;
        public int SelectedIndex { get; private set; } = 0;
        public string SelectedItem => _items[SelectedIndex];

        public ComboBox(int x, int y, int width, string[] items) : base(x, y, width, 3)
        {
            _items = items;
        }

        public override void Draw(char[,] buffer)
        {
            string border = IsFocused ? "╔" + new string('═', Width - 2) + "╗" : "┌" + new string('─', Width - 2) + "┐";
            string bottom = IsFocused ? "╚" + new string('═', Width - 2) + "╝" : "└" + new string('─', Width - 2) + "┘";
            
            string currentItem = _items.Length > 0 ? _items[SelectedIndex] : "";
            string content = $"│ {currentItem.PadRight(Width - 4)} ▼│";

            WriteToBuffer(buffer, X, Y, border);
            WriteToBuffer(buffer, X, Y + 1, content);
            WriteToBuffer(buffer, X, Y + 2, bottom);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (_items.Length == 0) return;

            if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.LeftArrow)
            {
                SelectedIndex = (SelectedIndex - 1 + _items.Length) % _items.Length;
            }
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
            {
                SelectedIndex = (SelectedIndex + 1) % _items.Length;
            }
        }
    }
}