using MultigraphEditor.src.graph;

namespace MultigraphEditor.Src.graph
{
    public interface IMGraphEditorNode : INode, INodeDrawable
    {
        IMGraphEditorNode Clone();

    }
}
