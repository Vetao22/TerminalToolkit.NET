using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Grid view and data listing control.
    /// </summary>
    public sealed class DataTable : View
    {
        private readonly string[] _columns;
        private readonly string[,] _rows;
        private int _selectedRow = 0;

        public DataTable(int x, int y, int width, string[] columns, string[,] rows) : base(x, y, width, rows.GetLength(0) + 2)
        {
            _columns = columns;
            _rows = rows;
        }

        public override void Draw(char[,] buffer)
        {
            // Render header
            string header = "│ " + string.Join(" │ ", _columns) + " │";
            WriteToBuffer(buffer, X, Y, header);
            WriteToBuffer(buffer, X, Y + 1, "├" + new string('─', header.Length - 2) + "┤");

            // Render rows
            for (int r = 0; r < _rows.GetLength(0); r++)
            {
                string rowIndicator = (IsFocused && r == _selectedRow) ? ">" : " ";
                string rowStr = $"{rowIndicator} ";
                
                for (int c = 0; c < _rows.GetLength(1); c++)
                {
                    rowStr += _rows[r, c].PadRight(10) + (c < _rows.GetLength(1) - 1 ? "│ " : "");
                }

                WriteToBuffer(buffer, X, Y + 2 + r, rowStr);
            }
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (_rows.GetLength(0) == 0) return;

            if (key.Key == ConsoleKey.UpArrow)
            {
                _selectedRow = Math.Max(0, _selectedRow - 1);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                _selectedRow = Math.Min(_rows.GetLength(0) - 1, _selectedRow + 1);
            }
        }
    }
}