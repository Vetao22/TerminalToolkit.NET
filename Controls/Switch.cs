using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Boolean toggle button control.
    /// </summary>
    public sealed class Switch : View
    {
        public bool IsChecked { get; private set; }
        private readonly string _label;

        public Switch(int x, int y, string label, bool initialState = false) : base(x, y, label.Length + 8, 1)
        {
            _label = label;
            IsChecked = initialState;
        }

        public override void Draw(char[,] buffer)
        {
            string stateStr = IsChecked ? "[ON ]" : "[OFF]";
            if (IsFocused) stateStr = $"<{stateStr.Trim('[', ']')}>";

            string text = $"{_label}: {stateStr}";
            WriteToBuffer(buffer, X, Y, text);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Spacebar || key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
            {
                IsChecked = !IsChecked;
            }
        }
    }
}