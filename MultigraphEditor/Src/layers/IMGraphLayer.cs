using MultigraphEditor.src.graph;

namespace MultigraphEditor.src.layers
{
    public interface IMGraphLayer : INodeLayer, IEdgeLayer
    {
        IMGraphLayer Clone();
        void UpdateNodeReferences(Dictionary<IMGraphEditorNode, IMGraphEditorNode> nodeReferences);
        void UpdateEdgeReferences(Dictionary<IMGraphEditorEdge, IMGraphEditorEdge> edgeReferences);
    }
}
