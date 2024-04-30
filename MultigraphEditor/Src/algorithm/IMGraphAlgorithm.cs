using MultigraphEditor.src.graph;
using MultigraphEditor.Src.layers;

namespace MultigraphEditor.Src.algorithm
{
    internal interface IMGraphAlgorithm
    {
        // Generic output method for all algorithms, elements of the list will be displayed in the listbox
        List<string> Output(INode startNode, INode endNode, IMGraphLayer targetLayer);
        string Name { get; }
    }
}
