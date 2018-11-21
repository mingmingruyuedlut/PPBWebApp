using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Models
{
    public class TreeView
    {
        public List<TreeViewNode> Nodes { get; set; }
    }

    public class TreeViewNode
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public bool IsSelected { get; set; }
        public int Tag { get; set; }

        public TreeViewNode Parent { get; set; }
        public List<TreeViewNode> Nodes { get; set; }
    }
}
