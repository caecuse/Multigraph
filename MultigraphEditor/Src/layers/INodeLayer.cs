using MultigraphEditor.src.graph;

namespace MultigraphEditor.src.layers
{
    public interface INodeLayer : ILayer
    {
        public List<IMGraphEditorNode> nodes { get; set; }
        int nodeWidth { get; set; }
    }
}
