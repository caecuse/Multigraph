namespace MultigraphEditor.src.graph
{
    public interface IDrawable
    {
        public bool IsInside(float x, float y);
        public int Identifier { get; set; }
        public int GetIdentifier();
    }
}
