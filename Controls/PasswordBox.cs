using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Masked text input control.
    /// </summary>
    public sealed class PasswordBox : View
    {
        private readonly char[] _buffer;
        private int _cursorIndex = 0;
        private int _length = 0;
        public string Password => new string(_buffer, 0, _length);

        public PasswordBox(int x, int y, int width) : base(x, y, width, 3)
        {
            _buffer = new char[width - 2];
        }

        public override void Draw(char[,] buffer)
        {
            string border = IsFocused ? "╔" + new string('═', Width - 2) + "╗" : "┌" + new string('─', Width - 2) + "┐";
            string bottom = IsFocused ? "╚" + new string('═', Width - 2) + "╝" : "└" + new string('─', Width - 2) + "┘";

            int innerWidth = Width - 2;
            string masked = new string('*', _length);
            if (masked.Length > innerWidth) masked = masked.Substring(masked.Length - innerWidth);

            string content = $"│{masked.PadRight(innerWidth)}│";

            WriteToBuffer(buffer, X, Y, border);
            WriteToBuffer(buffer, X, Y + 1, content);
            WriteToBuffer(buffer, X, Y + 2, bottom);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Backspace && _cursorIndex > 0)
            {
                Array.Copy(_buffer, _cursorIndex, _buffer, _cursorIndex - 1, _length - _cursorIndex);
                _cursorIndex--;
                _length--;
            }
            else if (!char.IsControl(key.KeyChar) && _length < _buffer.Length)
            {
                Array.Copy(_buffer, _cursorIndex, _buffer, _cursorIndex + 1, _length - _cursorIndex);
                _buffer[_cursorIndex] = key.KeyChar;
                _cursorIndex++;
                _length++;
            }
        }
    }
}