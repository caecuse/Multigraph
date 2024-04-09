namespace MultigraphEditor.src.graph
{
    public interface INode
    {
        public string? Label { get; set; }
        public List<IEdge> Edges { get; set; }
        public void AddEdge(IEdge e);
        public void RemoveEdge(IEdge e);
    }
}
