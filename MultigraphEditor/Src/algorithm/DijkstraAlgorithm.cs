using MultigraphEditor.src.graph;
using MultigraphEditor.Src.algorithm;
using MultigraphEditor.Src.layers;

namespace MultigraphEditor.Src.algorithm
{
    internal class DijkstraAlgorithm : IMGraphAlgorithm
    {
        public string Name { get; } = "Dijkstra's Algorithm";

        public List<INode> FindPath(INode start, INode target, IMGraphLayer targetLayer)
        {
            PriorityQueue<INode, double> openSet = new PriorityQueue<INode, double>();
            Dictionary<INode, INode> cameFrom = new Dictionary<INode, INode>();
            Dictionary<INode, double> gScore = new Dictionary<INode, double>();
            foreach (graph.IMGraphEditorNode node in targetLayer.nodes)
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