using MultigraphEditor.src.algorithm;
using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using MultigraphEditor.src.graph.example;


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
            mGraphAlgorithms.Add(typeof(HamiltonCycleAlgorithm));
            //mGraphAlgorithms.Add(typeof(DijkstraAlgorithm));
            //mGraphAlgorithms.Add(typeof(CheckHamiltonCycle));

            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(NodeType, EdgeType, LayerType, mGraphAlgorithms));
        }
    }
}
