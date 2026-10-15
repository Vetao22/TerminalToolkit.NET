using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Single-choice option list control.
    /// </summary>
    public sealed class RadioGroup : View
    {
        private readonly string[] _options;
        public int SelectedIndex { get; private set; } = 0;

        public RadioGroup(int x, int y, string[] options) : base(x, y, 30, options.Length)
        {
            _options = options;
        }

        public override void Draw(char[,] buffer)
        {
            for (int i = 0; i < _options.Length; i++)
            {
                string marker = (i == SelectedIndex) ? "(•)" : "( )";
                string focusTag = (IsFocused && i == SelectedIndex) ? "> " : "  ";
                WriteToBuffer(buffer, X, Y + i, $"{focusTag}{marker} {_options[i]}");
            }
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (_options.Length == 0) return;

            if (key.Key == ConsoleKey.UpArrow)
            {
                SelectedIndex = Math.Max(0, SelectedIndex - 1);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                SelectedIndex = Math.Min(_options.Length - 1, SelectedIndex + 1);
            }
            else if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter)
            {
                // Confirmation action if needed
            }
        }
    }
}