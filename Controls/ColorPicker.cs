using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Terminal theme and color selector control.
    /// </summary>
    public sealed class ColorPicker : View
    {
        private readonly string[] _themes = { "Matrix Green", "Cyberpunk Cyan", "Amber Retro", "Classic Mono" };
        public int SelectedIndex { get; private set; } = 0;
        public string SelectedTheme => _themes[SelectedIndex];

        public ColorPicker(int x, int y, int width) : base(x, y, width, 3) { }

        public override void Draw(char[,] buffer)
        {
            string border = IsFocused ? "╔" + new string('═', Width - 2) + "╗" : "┌" + new string('─', Width - 2) + "┐";
            string bottom = IsFocused ? "╚" + new string('═', Width - 2) + "╝" : "└" + new string('─', Width - 2) + "┘";
            string themeName = _themes[SelectedIndex];
            string content = $"│ Theme: {themeName.PadRight(Width - 10)} 🎨│";

            WriteToBuffer(buffer, X, Y, border);
            WriteToBuffer(buffer, X, Y + 1, content);
            WriteToBuffer(buffer, X, Y + 2, bottom);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.UpArrow)
            {
                SelectedIndex = (SelectedIndex - 1 + _themes.Length) % _themes.Length;
            }
            else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
            {
                SelectedIndex = (SelectedIndex + 1) % _themes.Length;
            }
        }
    }
}