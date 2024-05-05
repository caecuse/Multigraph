namespace MultigraphEditor.src.graph
{
    public interface IDrawable
    {
        public int Identifier { get; set; }
        public bool IsInside(float x, float y);
        public int GetIdentifier();
    }
}
