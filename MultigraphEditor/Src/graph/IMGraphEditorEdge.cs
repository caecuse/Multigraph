using MultigraphEditor.src.graph;

namespace MultigraphEditor.Src.graph
{
    public interface IMGraphEditorEdge : IEdge, IEdgeDrawable
    {
        IMGraphEditorEdge Clone();
    }
}
