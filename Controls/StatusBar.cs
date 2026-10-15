using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Bottom quick actions bar control.
    /// </summary>
    public sealed class StatusBar : View
    {
        private string _statusText;

        public StatusBar(string statusText = "Ready | [Tab] Next Control | [Esc] Exit") 
            : base(0, Console.WindowHeight - 2, Console.WindowWidth, 1)
        {
            _statusText = statusText;
        }

        public void SetStatus(string text)
        {
            _statusText = text;
        }

        public override void Draw(char[,] buffer)
        {
            int width = buffer.GetLength(1);
            string line = _statusText.PadRight(width);
            if (line.Length > width) line = line.Substring(0, width);
            
            WriteToBuffer(buffer, 0, Y, line);
        }
    }
}