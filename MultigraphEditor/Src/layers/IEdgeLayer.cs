using MultigraphEditor.Src.graph;

namespace MultigraphEditor.src.layers
{
    public interface IEdgeLayer : ILayer
    {
        public int arrowSize { get; set; }
        public List<IMGraphEditorEdge> edges { get; set; }
        int edgeWidth { get; set; }
    }
}
