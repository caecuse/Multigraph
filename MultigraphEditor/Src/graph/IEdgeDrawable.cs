using MultigraphEditor.src.layers;

namespace MultigraphEditor.src.graph
{
    public interface IEdgeDrawable : IDrawable
    {
        internal INodeDrawable SourceDrawable { get; set; }
        internal INodeDrawable TargetDrawable { get; set; }
        public static int EdgeCounter;
        public void PopulateDrawing(INodeDrawable srcDrw, INodeDrawable tgtDrw);
        public void Draw(Graphics g, IEdgeLayer l);
        public void DrawLabel(Graphics g, IEdgeLayer l);
        public void DrawArrow(Graphics g, IEdgeLayer l);
        public bool IsInsideControlPoint(float x, float y);
    }
}
