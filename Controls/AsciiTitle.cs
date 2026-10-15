using System;
using TerminalToolkit.NET.Core;
using AsciiArt.NET;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Component for rendering ASCII banners using AsciiArt.NET.
    /// </summary>
    public sealed class AsciiTitle : View
    {
        public string Text { get; set; }

        public AsciiTitle(int x, int y, string text) : base(x, y, 0, 0)
        {
            Text = text;
        }

        public override void Draw(char[,] buffer)
        {
            if (!IsVisible) return;
            string rendered = AsciiEngine.Render(Text, scale: 1);
            string[] lines = rendered.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                WriteToBuffer(buffer, X, Y + i, lines[i].TrimEnd());
            }
        }
    }
}