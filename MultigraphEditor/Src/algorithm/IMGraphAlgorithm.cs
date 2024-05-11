using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;

namespace MultigraphEditor.src.algorithm
{
    internal interface IMGraphAlgorithm
    {
        // Generic output method for all algorithms, elements of the list will be displayed in the listbox
        List<string> Output(INode startNode, INode endNode, IMGraphLayer targetLayer);
        string Name { get; }
        bool requiresStartNode { get; }
        bool requiresEndNode { get; }
    }
}
