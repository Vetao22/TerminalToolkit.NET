using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TerminalToolkit.NET.Core
{
    /// <summary>
    /// The core engine responsible for the main application loop, input handling, and rendering synchronization.
    /// </summary>
    public sealed class TerminalApp
    {
        private static readonly Lazy<TerminalApp> _instance = new Lazy<TerminalApp>(() => new TerminalApp());
        public static TerminalApp Instance => _instance.Value;

        private readonly List<View> _views = new List<View>();
        private int _focusedIndex = 0;
        private bool _isRunning = false;

        // Custom interaction events
        public event Action<ConsoleKeyInfo> KeyPressed;

        private TerminalApp() { }

        /// <summary>
        /// Starts the terminal application loop.
        /// </summary>
        public async Task RunAsync(CancellationToken token = default)
        {
            if (_isRunning) return;
            _isRunning = true;

            ConfigureTerminal();

            try
            {
                while (!token.IsCancellationRequested && _isRunning)
                {
                    // 1. Process Input Non-Blockingly
                    if (Console.KeyAvailable)
                    {
                        var key = Console.ReadKey(true);
                        HandleInput(key);
                        KeyPressed?.Invoke(key);
                    }

                    // 2. Render synchronization (double buffering)
                    Render();

                    // Throttle for performance (~60 FPS)
                    await Task.Delay(16, token);
                }
            }
            finally
            {
                RestoreTerminal();
            }
        }

        public void AddView(View view) => _views.Add(view);

        public void Stop() => _isRunning = false;

        private void HandleInput(ConsoleKeyInfo key)
        {
            // Focus navigation
            if (key.Key == ConsoleKey.Tab)
            {
                if (_views.Count == 0) return;
                _views[_focusedIndex].IsFocused = false;
                _focusedIndex = (_focusedIndex + 1) % _views.Count;
                _views[_focusedIndex].IsFocused = true;
            }

            // Pass input to focused view
            if (_views.Count > 0)
            {
                _views[_focusedIndex].HandleInput(key);
            }
        }

        private void Render()
        {
            int width = Console.WindowWidth;
            int height = Console.WindowHeight;
            if (width < 20 || height < 10) return; // Minimum size check

            var buffer = new char[height, width];
            ClearBuffer(buffer);

            // Ask all views to draw themselves into the buffer
            foreach (var view in _views)
            {
                view.Draw(buffer);
            }

            // Flush buffer atomically to screen
            var sb = new StringBuilder(height * (width + 1));
            for (int y = 0; y < height - 1; y++)
            {
                for (int x = 0; x < width; x++) sb.Append(buffer[y, x]);
                sb.AppendLine();
            }

            Console.SetCursorPosition(0, 0);
            Console.Write(sb.ToString());
        }

        private void ClearBuffer(char[,] buffer)
        {
            int h = buffer.GetLength(0);
            int w = buffer.GetLength(1);
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++) buffer[y, x] = ' ';
        }

        private void ConfigureTerminal()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            Console.Clear();
        }

        private void RestoreTerminal()
        {
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.Clear();
        }
    }
}