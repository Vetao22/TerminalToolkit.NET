using System;

namespace TerminalToolkit.NET.Core
{
    /// <summary>
    /// Base class for all Terminal UI components (controls/widgets).
    /// </summary>
    public abstract class View
    {
        // Geometry
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        // State
        public bool IsFocused { get; set; }
        public bool IsVisible { get; set; } = true;

        protected View(int x, int y, int width, int height)
        {
            X = x; Y = y; Width = width; Height = height;
        }

        /// <summary>
        /// Draws the component into the synchronized terminal buffer.
        /// </summary>
        public abstract void Draw(char[,] buffer);

        /// <summary>
        /// Handles keyboard input when the component is focused.
        /// </summary>
        public virtual void HandleInput(ConsoleKeyInfo key) { }

        // Utility: Write text directly into the buffer safely
        protected void WriteToBuffer(char[,] buffer, int bx, int by, string text, bool inverse = false)
        {
            int h = buffer.GetLength(0);
            int w = buffer.GetLength(1);

            if (by < 0 || by >= h) return;

            for (int i = 0; i < text.Length; i++)
            {
                int currX = bx + i;
                if (currX >= 0 && currX < w)
                {
                    buffer[by, currX] = text[i];
                }
            }
        }
    }
}