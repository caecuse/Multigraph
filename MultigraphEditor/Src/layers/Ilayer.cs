namespace MultigraphEditor.src.layers
{
    public interface ILayer
    {
        Font Font { get; set; }
        Color Color { get; set; }
        bool Active { get; set; }
        int Identifier { get; set; }
        string Name { get; set; }
        void changeActive();
    }
}
