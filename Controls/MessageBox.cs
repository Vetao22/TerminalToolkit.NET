using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Modal alert pop-up control.
    /// </summary>
    public sealed class MessageBox : View
    {
        private readonly string _title;
        private readonly string _message;
        public event Action Confirmed;
        public event Action Cancelled;

        public MessageBox(int x, int y, int width, string title, string message) : base(x, y, width, 6)
        {
            _title = title;
            _message = message;
        }

        public override void Draw(char[,] buffer)
        {
            if (!IsVisible) return;

            string borderTop = "╔" + new string('═', Width - 2) + "╗";
            string borderBot = "╚" + new string('═', Width - 2) + "╝";

            WriteToBuffer(buffer, X, Y, borderTop);
            WriteToBuffer(buffer, X, Y + 1, $"│ {_title.PadRight(Width - 4)} │");
            WriteToBuffer(buffer, X, Y + 2, "├" + new string('─', Width - 2) + "┤");
            WriteToBuffer(buffer, X, Y + 3, $"│ {_message.PadRight(Width - 4)} │");
            WriteToBuffer(buffer, X, Y + 4, IsFocused ? "│  [OK] (Enter)    [Cancel] (ESC)  │" : "│  OK              Cancel          │");
            WriteToBuffer(buffer, X, Y + 5, borderBot);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Enter || key.Key == ConsoleKey.Spacebar)
            {
                Confirmed?.Invoke();
            }
            else if (key.Key == ConsoleKey.Escape)
            {
                Cancelled?.Invoke();
            }
        }
    }
}