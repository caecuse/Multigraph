using MultigraphEditor.src.graph;
using MultigraphEditor.Src.layers;

namespace MultigraphEditor.Src.algorithm
{
    internal interface IMGraphAlgorithm
    {
        List<INode> FindPath(INode startNode, INode endNode, IMGraphLayer targetLayer);
        String Name { get; }
    }
}
