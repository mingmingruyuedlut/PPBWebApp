using PPB.DBManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPB.DBManager.Managers
{
    public class TreeViewManager
    {
        public TreeView GenerateProjectFileTreeView()
        {
            var tv = new TreeView()
            {
                Nodes = new List<TreeViewNode>()
            };

            var rootNode = new TreeViewNode()
            {
                Label = "Root",
                Nodes = new List<TreeViewNode>()
            };

            tv.Nodes.Add(rootNode);

            foreach (var root in tv.Nodes)
            {
                GenerateSubTreeNodes(root, "Area");

                foreach (var areaNode in root.Nodes)
                {
                    GenerateSubTreeNodes(areaNode, "Section");

                    foreach (var sectionNode in areaNode.Nodes)
                    {
                        GenerateSubTreeNodes(sectionNode, "Station");

                        foreach (var stationNode in sectionNode.Nodes)
                        {
                            GenerateSubTreeNodes(stationNode, "PLC");
                        }
                    }
                }
            }
            return tv;
        }

        private void GenerateSubTreeNodes(TreeViewNode parent, string label)
        {
            var firstNode = new TreeViewNode()
            {
                Label = label + 1.ToString(),
                Nodes = new List<TreeViewNode>()
            };
            var secondNode = new TreeViewNode()
            {
                Label = label + 2.ToString(),
                Nodes = new List<TreeViewNode>()
            };
            parent.Nodes.Add(firstNode);
            parent.Nodes.Add(secondNode);
        }

    }
}
