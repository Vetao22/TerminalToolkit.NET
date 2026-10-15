using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Interactive hour and minute adjuster control.
    /// </summary>
    public sealed class TimePicker : View
    {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }
        private int _fieldIndex = 0; // 0 = Hours, 1 = Minutes

        public TimePicker(int x, int y, int initialHours = 12, int initialMinutes = 0) : base(x, y, 20, 3)
        {
            Hours = Math.Clamp(initialHours, 0, 23);
            Minutes = Math.Clamp(initialMinutes, 0, 59);
        }

        public override void Draw(char[,] buffer)
        {
            string border = IsFocused ? "╔══════════════════╗" : "┌──────────────────┐";
            string bottom = IsFocused ? "╚══════════════════╝" : "└──────────────────┘";

            string hStr = Hours.ToString("D2");
            string mStr = Minutes.ToString("D2");

            string hDisplay = _fieldIndex == 0 ? $"<{hStr}>" : $" {hStr} ";
            string mDisplay = _fieldIndex == 1 ? $"<{mStr}>" : $" {mStr} ";

            string content = $"│ Time: {hDisplay}:{mDisplay}   │";

            WriteToBuffer(buffer, X, Y, border);
            WriteToBuffer(buffer, X, Y + 1, content);
            WriteToBuffer(buffer, X, Y + 2, bottom);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Tab || key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
            {
                _fieldIndex = (_fieldIndex + 1) % 2;
            }
            else if (key.Key == ConsoleKey.UpButton || key.Key == ConsoleKey.Add)
            {
                if (_fieldIndex == 0) Hours = (Hours + 1) % 24;
                else Minutes = (Minutes + 1) % 60;
            }
            else if (key.Key == ConsoleKey.DownButton || key.Key == ConsoleKey.Subtract)
            {
                if (_fieldIndex == 0) Hours = (Hours - 1 + 24) % 24;
                else Minutes = (Minutes - 1 + 60) % 60;
            }
        }
    }
}