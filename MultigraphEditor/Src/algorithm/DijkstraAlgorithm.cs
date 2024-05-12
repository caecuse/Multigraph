using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;

namespace MultigraphEditor.src.algorithm
{
    public class DijkstraAlgorithm : IMGraphAlgorithm
    {
        public string Name { get; } = "Dijkstra's Algorithm";
        public bool requiresStartNode { get; } = true;
        public bool requiresEndNode { get; } = true;
        public List<string> Output(INode start, INode target, IMGraphLayer targetLayer)
        {
            List<INode> path = FindPath(start, target, targetLayer);
            List<string> output = new List<string>();
            foreach (INode node in path)
            {
                if (node.Label != null)
                {
                    output.Add(node.Label);
                }
                else
                {
                    output.Add("Unnamed node");
                }
            }
            if (path.Count == 0)
            {
                List<string> error = new List<string>();
                error.Add("No path found");
                return error;
            }
            output.Reverse();
            return output;
        }

        public List<INode> FindPath(INode start, INode target, IMGraphLayer targetLayer)
        {
            PriorityQueue<INode, double> openSet = new PriorityQueue<INode, double>();
            Dictionary<INode, INode> cameFrom = new Dictionary<INode, INode>();
            Dictionary<INode, double> gScore = new Dictionary<INode, double>();
            foreach (IMGraphEditorNode node in targetLayer.nodes)
            {
                gScore[node] = double.PositiveInfinity;
            }
            gScore[start] = 0;

            openSet.Enqueue(start, 0);

            while (openSet.Count > 0)
            {
                INode current = openSet.Dequeue();

                if (current.Equals(target))
                {
                    List<INode> res = ReconstructPath(cameFrom, target);
                    res.Insert(0, start);
                    res.Reverse();
                    return res;
                }

                foreach (IEdge edge in current.Edges)
                {
                    if (targetLayer.edges.Contains(edge) && targetLayer.nodes.Contains(edge.Target))
                    {
                        INode neighbor = edge.Target;
                        if (edge.Weight < 0)
                        {
                            MessageBox.Show("Dijkstra's algorithm does not work with edges that have negative weight", "Bad edge weight", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return new List<INode>();
                        }
                        double tentativeGScore = gScore[current] + edge.Weight;

                        if (tentativeGScore < gScore[neighbor])
                        {
                            cameFrom[neighbor] = current;
                            gScore[neighbor] = tentativeGScore;

                            if (!openSet.UnorderedItems.Any(item => item.Element.Equals(neighbor)))
                            {
                                openSet.Enqueue(neighbor, gScore[neighbor]);
                            }
                        }
                    }
                }
            }

            return new List<INode>();
        }

        private List<INode> ReconstructPath(Dictionary<INode, INode> cameFrom, INode current)
        {
            List<INode> path = new List<INode>();
            if (!cameFrom.ContainsKey(current))
            {
                return path;
            }

            while (current != null && cameFrom.ContainsKey(current))
            {
                path.Insert(0, current);
                current = cameFrom[current];
            }
            return path;
        }
    }
}