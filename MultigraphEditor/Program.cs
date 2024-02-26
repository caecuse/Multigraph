using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;

namespace MultigraphEditor
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Type NodeType = typeof(MGraphEditorNode);
            Type EdgeType = typeof(MGraphEditorEdge);
            Type NodeLayerType = typeof(MGraphEditorNodeLayer);
            Type EdgeLayerType = typeof(MGraphEditorEdgeLayer);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm(NodeType, EdgeType, NodeLayerType, EdgeLayerType));
        }
    }
}