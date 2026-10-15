using TerminalToolkit.NET.Core;
using TerminalGrid.NET; // Reutilizando nossa grid engine para layouts e painéis

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// A panel container component that uses TerminalGrid.NET conventions to draw borders and structure layout.
    /// </summary>
    public sealed class Panel : View
    {
        public string Title { get; set; }
        public Panel(int x, int y, int width, int height, string title) : base(x, y, width, height) => Title = title;

        public override void Draw(char[,] buffer)
        {
            if (!IsVisible) return;
            // Draw border via TerminalGrid component
            GridRenderer.DrawPanelBox(buffer, X, Y, Width, Height, Title);
        }
    }
}