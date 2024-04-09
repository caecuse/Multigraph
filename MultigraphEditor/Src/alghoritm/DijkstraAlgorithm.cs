using MultigraphEditor.src.graph;
using MultigraphEditor.Src.alghoritm;
using MultigraphEditor.Src.layers;

namespace MultigraphEditor.Src.algorithm
{
    internal class DijkstraAlgorithm : IMGraphAlgorithm
    {
        public string Name { get; } = "Dijkstra's Algorithm";

        public List<INode> FindPath(INode start, INode target, IMGraphLayer targetLayer)
        {
            var openSet = new PriorityQueue<INode, double>();
            var cameFrom = new Dictionary<INode, INode>();
            var gScore = new Dictionary<INode, double>();
            foreach (var node in targetLayer.nodes)
            {
                gScore[node] = double.PositiveInfinity;
            }
            gScore[start] = 0;

            openSet.Enqueue(start, 0);

            while (openSet.Count > 0)
            {
                var current = openSet.Dequeue();

                if (current.Equals(target))
                {
                    var res = ReconstructPath(cameFrom, target);
                    res.Insert(0, start);
                    res.Reverse();
                    return res;
                }

                foreach (var edge in current.Edges)
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