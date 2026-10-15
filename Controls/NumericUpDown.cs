using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Step-based integer counter control.
    /// </summary>
    public sealed class NumericUpDown : View
    {
        public int Value { get; private set; }
        public int MinValue { get; }
        public int MaxValue { get; }
        public int Step { get; }
        private readonly string _label;

        public NumericUpDown(int x, int y, int width, string label, int min, int max, int initial, int step = 1) : base(x, y, width, 3)
        {
            _label = label;
            MinValue = min;
            MaxValue = max;
            Step = step;
            Value = Math.Clamp(initial, min, max);
        }

        public override void Draw(char[,] buffer)
        {
            string border = IsFocused ? "╔" + new string('═', Width - 2) + "╗" : "┌" + new string('─', Width - 2) + "┐";
            string bottom = IsFocused ? "╚" + new string('═', Width - 2) + "╝" : "└" + new string('─', Width - 2) + "┘";
            
            string content = $"│ {_label}: < {Value,3} > │";
            if (content.Length < Width)
            {
                content = content.PadRight(Width - 1) + "│";
            }

            WriteToBuffer(buffer, X, Y, border);
            WriteToBuffer(buffer, X, Y + 1, content);
            WriteToBuffer(buffer, X, Y + 2, bottom);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.DownButton || key.Key == ConsoleKey.Subtract)
            {
                Value = Math.Max(MinValue, Value - Step);
            }
            else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.UpButton || key.Key == ConsoleKey.Add)
            {
                Value = Math.Min(MaxValue, Value + Step);
            }
        }
    }
}