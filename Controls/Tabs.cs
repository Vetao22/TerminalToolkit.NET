using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Horizontal tabbed navigation control.
    /// </summary>
    public sealed class Tabs : View
    {
        private readonly string[] _tabTitles;
        public int SelectedIndex { get; private set; } = 0;
        public string SelectedTab => _tabTitles[SelectedIndex];

        public Tabs(int x, int y, string[] tabTitles) : base(x, y, 50, 1)
        {
            _tabTitles = tabTitles;
        }

        public override void Draw(char[,] buffer)
        {
            string line = "";
            for (int i = 0; i < _tabTitles.Length; i++)
            {
                bool isSelected = i == SelectedIndex;
                string tabStr = isSelected ? $" [{_tabTitles[i]}] " : $"  {_tabTitles[i]}  ";
                line += tabStr;
            }
            WriteToBuffer(buffer, X, Y, line);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (_tabTitles.Length == 0) return;

            if (key.Key == ConsoleKey.LeftArrow)
            {
                SelectedIndex = (SelectedIndex - 1 + _tabTitles.Length) % _tabTitles.Length;
            }
            else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.Tab)
            {
                SelectedIndex = (SelectedIndex + 1) % _tabTitles.Length;
            }
        }
    }
}