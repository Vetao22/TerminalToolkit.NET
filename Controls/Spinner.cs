using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Async loading indicator control.
    /// </summary>
    public sealed class Spinner : View
    {
        private readonly string[] _frames = { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };
        private int _frameIndex = 0;
        private readonly string _label;

        public Spinner(int x, int y, string label) : base(x, y, label.Length + 4, 1)
        {
            _label = label;
        }

        public void UpdateFrame()
        {
            _frameIndex = (_frameIndex + 1) % _frames.Length;
        }

        public override void Draw(char[,] buffer)
        {
            string frameChar = _frames[_frameIndex];
            string text = $"{frameChar} {_label}";
            WriteToBuffer(buffer, X, Y, text);
        }
    }
}