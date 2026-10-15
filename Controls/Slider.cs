using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Interactive numeric adjuster slider control.
    /// </summary>
    public sealed class Slider : View
    {
        public int MinValue { get; }
        public int MaxValue { get; }
        public int Value { get; private set; }
        private readonly string _label;

        public Slider(int x, int xPos, int width, string label, int min, int max, int initial) : base(x, xPos, width, 2)
        {
            _label = label;
            MinValue = min;
            MaxValue = max;
            Value = Math.Clamp(initial, min, max);
        }

        public override void Draw(char[,] buffer)
        {
            int innerWidth = Width - _label.Length - 8;
            innerWidth = Math.Max(10, innerWidth);

            double percentage = (double)(Value - MinValue) / (MaxValue - MinValue);
            int filled = (int)Math.Round(percentage * innerWidth);
            filled = Math.Clamp(filled, 0, innerWidth);

            string bar = "[" + new string('█', filled) + new string('░', innerWidth - filled) + "]";
            string text = $"{_label}: {bar} {Value}";

            WriteToBuffer(buffer, X, Y, text);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.DownArrow)
            {
                Value = Math.Max(MinValue, Value - 1);
            }
            else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.UpArrow)
            {
                Value = Math.Min(MaxValue, Value + 1);
            }
        }
    }
}