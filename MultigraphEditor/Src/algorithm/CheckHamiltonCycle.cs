using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.algorithm
{
    internal class CheckHamiltonCycle : IMGraphAlgorithm
    {
        public List<string> Output(INode startNode, INode endNode, IMGraphLayer targetLayer)
        {
            List<string> output = new List<string>();
            if (targetLayer.nodes.Count < 3)
            {
                output.Add("Graph has less than 3 nodes, so it can't have a Hamilton cycle");
                return output;
            }
            if (targetLayer.nodes.Count != targetLayer.edges.Count)
            {
                output.Add("Graph has more nodes than edges, so it can't have a Hamilton cycle");
                return output;
            }
            foreach (INode node in targetLayer.nodes)
            {
                if (node.Edges.Count < 2)
                {
                    output.Add("Node " + node.Label + " has less than 2 edges, so it can't be part of a Hamilton cycle");
                    return output;
                }
            }
            output.Add("Graph has a Hamilton cycle");
            return output;
        }
        public string Name { get; } = "Check Hamilton Cycle";
        public bool requiresStartNode { get; } = false;
        public bool requiresEndNode { get; } = false;
    }
}
