namespace MultigraphEditor.src.graph
{
    public interface IEdge
    {
        public string? Label { get; set; }
        public INode Source { get; set; }
        public INode Target { get; set; }
        public bool Bidirectional { get; set; }
        public int Weight { get; set; }

        public void PopulateNode(INode src, INode tgt, bool bidir, int w);
    }
}
