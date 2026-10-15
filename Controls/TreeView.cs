using System;
using System.Collections.Generic;
using TerminalToolkit.NET.Core;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Hierarchical node navigator control.
    /// </summary>
    public sealed class TreeView : View
    {
        private readonly TreeNode _rootNode;
        private int _selectedIndex = 0;

        public TreeView(int x, int y, int width, int height, TreeNode rootNode) : base(x, y, width, height)
        {
            _rootNode = rootNode;
        }

        public override void Draw(char[,] buffer)
        {
            var flatList = FlattenTree(_rootNode, 0);
            for (int i = 0; i < Math.Min(Height, flatList.Count); i++)
            {
                var (node, depth) = flatList[i];
                string indent = new string(' ', depth * 2);
                string icon = node.Children.Count > 0 ? (node.IsExpanded ? "📂 " : "📁 ") : "📄 ";
                string cursor = (IsFocused && i == _selectedIndex) ? "> " : "  ";
                
                string line = $"{cursor}{indent}{icon}{node.Title}";
                WriteToBuffer(buffer, X, Y + i, line);
            }
        }

        private List<(TreeNode Node, int Depth)> FlattenTree(TreeNode node, int depth)
        {
            var list = new List<(TreeNode, int)> { (node, depth) };
            if (node.IsExpanded)
            {
                foreach (var child in node.Children)
                {
                    list.AddRange(FlattenTree(child, depth + 1));
                }
            }
            return list;
        }

        public override void HandleInput(ConsoleKeyInfo key)
        {
            var flatList = FlattenTree(_rootNode, 0);
            if (flatList.Count == 0) return;

            if (key.Key == ConsoleKey.UpArrow)
            {
                _selectedIndex = Math.Max(0, _selectedIndex - 1);
            }
            else if (key.Key == ConsoleKey.DownArrow)
            {
                _selectedIndex = Math.Min(flatList.Count - 1, _selectedIndex + 1);
            }
            else if (key.Key == ConsoleKey.RightArrow || key.Key == ConsoleKey.Enter)
            {
                var target = flatList[_selectedIndex].Node;
                if (target.Children.Count > 0) target.IsExpanded = true;
            }
            else if (key.Key == ConsoleKey.LeftArrow)
            {
                var target = flatList[_selectedIndex].Node;
                if (target.Children.Count > 0 && target.IsExpanded) target.IsExpanded = false;
            }
        }
    }
}