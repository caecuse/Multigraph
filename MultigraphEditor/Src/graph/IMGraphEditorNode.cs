namespace MultigraphEditor.src.graph
{
    public interface IMGraphEditorNode : INode, INodeDrawable
    {
        IMGraphEditorNode Clone();
    }
}
