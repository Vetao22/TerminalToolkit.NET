using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Interactive text input box control.
    /// </summary>
    public sealed class TextBox : View
    {
        private readonly char[] _buffer;
        private int _cursorIndex = 0;
        private int _scrollOffset = 0;
        public string Text => new string(_buffer, 0, _length);
        private int _length = 0;

        public TextBox(int x, int y, int width, string initialText = "") : base(x, y, width, 3)
        {
            _buffer = new char[width - 2];
            if (!string.IsNullOrEmpty(initialText))
            {
                int copyLen = Math.Min(initialText.Length, _buffer.Length);
                initialText.CopyTo(0, _buffer, 0, copyLen);
                _length = copyLen;
                _cursorIndex = copyLen;
            }
        }

        public override void Draw(char[,] buffer)
        {
            string border = IsFocused ? "╔" + new string('═', Width - 2) + "╗" : "┌" + new string('─', Width - 2) + "┐";
            string bottom = IsFocused ? "╚" + new string('═', Width - 2) + "╝" : "└" + new string('─', Width - 2) + "┘";

            // Render visible slice of text for horizontal scrolling inside fixed width
            int innerWidth = Width - 2;
            string currentText = new string(_buffer, 0, _length);
            if (currentText.Length > innerWidth)
            {
                if (_cursorIndex - _scrollOffset >= innerWidth) _scrollOffset = _cursorIndex - innerWidth + 1;
                if (_cursorIndex < _scrollOffset) _scrollOffset = _cursorIndex;
                currentText = currentText.Substring(_scrollOffset, Math.Min(innerWidth, currentText.Length - _scrollOffset));
            }

            string content = $"│{currentText.PadRight(innerWidth)}│";

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
            else if (key.Key == ConsoleKey.Delete && _cursorIndex < _length)
            {
                Array.Copy(_buffer, _cursorIndex + 1, _buffer, _cursorIndex, _length - _cursorIndex - 1);
                _length--;
            }
            else if (key.Key == ConsoleKey.LeftArrow && _cursorIndex > 0)
            {
                _cursorIndex--;
            }
            else if (key.Key == ConsoleKey.RightArrow && _cursorIndex < _length)
            {
                _cursorIndex++;
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