using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Interactive button control.
    /// </summary>
    public sealed class Button : View
    {
        public string Text { get; set; }
        public event Action Clicked;

        public Button(int x, int y, string text) : base(x, y, text.Length + 4, 3)
        {
            Text = text;
        }

        public override void Draw(char[,] buffer)
        {
            if (!IsVisible) return;
            string border = IsFocused ? "╔" + new string('═', Width - 2) + "╗" : "┌" + new string('─', Width - 2) + "┐";
            string content = IsFocused ? $"║ {Text} ║" : $"│ {Text} │";
            string bottom = IsFocused ? "╚" + new string('═', Width - 2) + "╝" : "└" + new string('─', Width - 2) + "┘";

            WriteToBuffer(buffer, X, Y, border);
            WriteToBuffer(buffer, X, Y + 1, content);
            WriteToBuffer(buffer, X, Y + 2, bottom);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
            {
                Clicked?.Invoke();
            }
        }
    }
}