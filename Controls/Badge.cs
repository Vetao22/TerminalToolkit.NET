using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Visual status tag control.
    /// </summary>
    public sealed class Badge : View
    {
        private readonly string _text;
        private readonly string _style; // "info", "success", "warning", "danger"

        public Badge(int x, int y, string text, string style = "info") : base(x, y, text.Length + 4, 1)
        {
            _text = text;
            _style = style.ToLower();
        }

        public override void Draw(char[,] buffer)
        {
            string formatted = _style switch
            {
                "success" => $"[ ✓ {_text} ]",
                "warning" => $"[ ⚠ {_text} ]",
                "danger"  => $"[ ✕ {_text} ]",
                _         => $"[ ℹ {_text} ]"
            };

            WriteToBuffer(buffer, X, Y, formatted);
        }
    }
}