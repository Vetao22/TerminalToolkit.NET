using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Horizontal section divider control.
    /// </summary>
    public sealed class Separator : View
    {
        private readonly string _title;

        public Separator(int x, int y, int width, string title = "") : base(x, y, width, 1)
        {
            _title = title;
        }

        public override void Draw(char[,] buffer)
        {
            string line;
            if (string.IsNullOrEmpty(_title))
            {
                line = new string('─', Width);
            }
            else
            {
                string paddedTitle = $" {_title} ";
                int remaining = Math.Max(0, Width - paddedTitle.Length);
                int left = remaining / 2;
                int right = remaining - left;
                line = new string('─', left) + paddedTitle + new string('─', right);
            }

            WriteToBuffer(buffer, X, Y, line);
        }
    }
}