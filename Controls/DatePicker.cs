using System;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Interactive date adjuster control.
    /// </summary>
    public sealed class DatePicker : View
    {
        public DateTime SelectedDate { get; private set; }
        private int _fieldIndex = 0; // 0 = Day, 1 = Month, 2 = Year

        public DatePicker(int x, int y, DateTime initialDate) : base(x, y, 26, 3)
        {
            SelectedDate = initialDate;
        }

        public override void Draw(char[,] buffer)
        {
            string border = IsFocused ? "╔════════════════════════╗" : "┌────────────────────────┐";
            string bottom = IsFocused ? "╚════════════════════════╝" : "└────────────────────────┘";

            string dStr = SelectedDate.Day.ToString("D2");
            string mStr = SelectedDate.Month.ToString("D2");
            string yStr = SelectedDate.Year.ToString("D4");

            string dDisp = _fieldIndex == 0 ? $"<{dStr}>" : $" {dStr} ";
            string mDisp = _fieldIndex == 1 ? $"<{mStr}>" : $" {mStr} ";
            string yDisp = _fieldIndex == 2 ? $"<{yStr}>" : $" {yStr} ";

            string content = $"│ Date: {dDisp}/{mDisp}/{yDisp} │";

            WriteToBuffer(buffer, X, Y, border);
            WriteToBuffer(buffer, X, Y + 1, content);
            WriteToBuffer(buffer, X, Y + 2, bottom);
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            if (key.Key == ConsoleKey.Tab || key.Key == ConsoleKey.LeftArrow || key.Key == ConsoleKey.RightArrow)
            {
                if (key.Key == ConsoleKey.LeftArrow) _fieldIndex = (_fieldIndex - 1 + 3) % 3;
                else _fieldIndex = (_fieldIndex + 1) % 3;
            }
            else if (key.Key == ConsoleKey.UpArrow || key.Key == ConsoleKey.Add)
            {
                AdjustDate(1);
            }
            else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.Subtract)
            {
                AdjustDate(-1);
            }
        }

        private void AdjustDate(int amount)
        {
            try
            {
                if (_fieldIndex == 0) SelectedDate = SelectedDate.AddDays(amount);
                else if (_fieldIndex == 1) SelectedDate = SelectedDate.AddMonths(amount);
                else if (_fieldIndex == 2) SelectedDate = SelectedDate.AddYears(amount);
            }
            catch
            {
                // Prevent invalid date rollovers (e.g., Feb 30)
            }
        }
    }
}