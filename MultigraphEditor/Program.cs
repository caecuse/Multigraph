using MultigraphEditor.src.graph;
using MultigraphEditor.Src.algorithm;
using MultigraphEditor.Src.layers;

namespace MultigraphEditor
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Type NodeType = typeof(MGraphEditorNode);
            Type EdgeType = typeof(MGraphEditorEdge);
            Type LayerType = typeof(MGraphLayer);
            List<Type> mGraphAlgorithms = new List<Type>();
            mGraphAlgorithms.Add(typeof(DijkstraAlgorithm));
            mGraphAlgorithms.Add(typeof(CheckHamiltonCycle));

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(NodeType, EdgeType, LayerType, mGraphAlgorithms));
        }
    }
}
