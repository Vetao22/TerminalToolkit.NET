using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Contextual help banner control.
    /// </summary>
    public sealed class Tooltip : View
    {
        private string _tipMessage;

        public Tooltip(int x, int y, int width, string tipMessage) : base(x, y, width, 3)
        {
            _tipMessage = tipMessage;
        }

        public void SetTip(string message)
        {
            _tipMessage = message;
        }

        public override void Draw(char[,] buffer)
        {
            if (!IsVisible) return;

            string borderTop = "╭" + new string('─', Width - 2) + "╮";
            string borderBot = "╰" + new string('─', Width - 2) + "╯";
            string content = $"│ 💡 {_tipMessage.PadRight(Width - 6)} │";

            WriteToBuffer(buffer, X, Y, borderTop);
            WriteToBuffer(buffer, X, Y + 1, content);
            WriteToBuffer(buffer, X, Y + 2, borderBot);
        }
    }
}