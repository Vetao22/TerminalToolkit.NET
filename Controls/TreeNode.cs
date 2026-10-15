using System.Collections.Generic;

namespace TerminalToolkit.NET.Controls
{
    /// <summary>
    /// Hierarchical node model for TreeView.
    /// </summary>
    public sealed class TreeNode
    {
        public string Title { get; set; }
        public List<TreeNode> Children { get; } = new List<TreeNode>();
        public bool IsExpanded { get; set; } = true;

        public TreeNode(string title) => Title = title;
    }
}