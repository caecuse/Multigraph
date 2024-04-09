using MultigraphEditor.Src.graph;

namespace MultigraphEditor.src.layers
{
    public interface INodeLayer : ILayer
    {
        public List<IMGraphEditorNode> nodes { get; set; }
    }
}
