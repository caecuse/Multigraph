using MultigraphEditor.src.layers;
using MultigraphEditor.Src.graph;

namespace MultigraphEditor.Src.layers
{
    public interface IMGraphLayer : INodeLayer, IEdgeLayer
    {
        IMGraphLayer Clone();
        void UpdateNodeReferences(Dictionary<IMGraphEditorNode, IMGraphEditorNode> nodeReferences);
        void UpdateEdgeReferences(Dictionary<IMGraphEditorEdge, IMGraphEditorEdge> edgeReferences);
    }
}
