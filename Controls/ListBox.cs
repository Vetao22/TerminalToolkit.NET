using System;
using System.Collections.Generic;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Scrollable item list control.
    /// </summary>
    public sealed class ListBox : View
    {
        private readonly List<string> _items;
        private int _selectedIndex = 0;
        private int _scrollOffset = 0;
        public string SelectedItem => _items.Count > 0 ? _items[_selectedIndex] : string.Empty;

        public ListBox(int x, int y, int width, int height, List<string> items) : base(x, y, width, height)
        {
            _items = items ?? new List<string>();
        }

        public override void Draw(char[,] buffer)
        {
            string borderTop = IsFocused ? "╔" + new string('═', Width - 2) + "╗" : "┌" + new string('─', Width - 2) + "┐";
            string borderBot = IsFocused ? "╚" + new string('═', Width - 2) + "╝" : "└" + new string('─', Width - 2) + "┘";

            WriteToBuffer(buffer, X, Y, borderTop);

            int viewHeight = Height - 2;
            if (_selectedIndex < _scrollOffset) _scrollOffset = _selectedIndex;
            if (_selectedIndex >= _scrollOffset + viewHeight) _scrollOffset = _selectedIndex - viewHeight + 1;

            for (int i = 0; i < viewHeight; i++)
            {
                int itemIndex = _scrollOffset + i;
                string rowContent;

                if (itemIndex < _items.Count)
                {
                    string marker = (IsFocused && itemIndex == _selectedIndex) ? "► " : "  ";
                    string text = $"{marker}{_items[itemIndex]}";
                    int maxLen = Width - 4;
                    if (text.Length > maxLen) text = text.Substring(0, maxLen);
                    rowContent = $"│ {text.PadRight(maxLen)} │";
                }
                else
                {
                    rowContent = $"│ {new string(' ', Width - 4)} │";
                }

                WriteToBuffer(buffer, X, Y + 1 + i, rowContent);
            }

            WriteToBuffer(buffer, X, Y + Height - 1, borderBot);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (_items.Count == 0) return;

            if (key.Key == ConsoleKey.UpArrow)
            {
                _selectedIndex = Math.Max(0, _selectedIndex - 1);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                _selectedIndex = Math.Min(_items.Count - 1, _selectedIndex + 1);
            }
        }
    }
}