namespace MultigraphEditor.src.graph
{
    public interface IMGraphEditorEdge : IEdge, IEdgeDrawable
    {
        IMGraphEditorEdge Clone();
    }
}
