using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Visual status indicator progress bar control.
    /// </summary>
    public sealed class ProgressBarView : View
    {
        private double _percentage;
        private readonly string _label;

        public ProgressBarView(int x, int y, int width, string label) : base(x, y, width, 2)
        {
            _label = label;
        }

        public void SetProgress(double percentage)
        {
            _percentage = Math.Clamp(percentage, 0, 100);
        }

        public override void Draw(char[,] buffer)
        {
            int innerWidth = Width - _label.Length - 8;
            innerWidth = Math.Max(10, innerWidth);

            int filled = (int)Math.Round((_percentage / 100.0) * innerWidth);
            filled = Math.Clamp(filled, 0, innerWidth);

            string bar = "[" + new string('█', filled) + new string('░', innerWidth - filled) + "]";
            string text = $"{_label}: {bar} {_percentage:F1}%";

            WriteToBuffer(buffer, X, Y, text);
        }
    }
}