using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultigraphEditor.src.design
{
    [Serializable]
    internal class ApplicationState
    {
        private List<IMGraphEditorNode> _nodes = new List<IMGraphEditorNode>();
        private List<IMGraphEditorEdge> _edges = new List<IMGraphEditorEdge>();
        private List<IMGraphLayer> _layers = new List<IMGraphLayer>();

        internal ApplicationState(List<IMGraphEditorEdge> edge, List<IMGraphEditorNode> node, List<IMGraphLayer> layers)
        {
            Dictionary<IMGraphEditorEdge, IMGraphEditorEdge> edgeMap = edge.ToDictionary(e => e, e => e.Clone());
            Dictionary<IMGraphEditorNode, IMGraphEditorNode> nodeMap = node.ToDictionary(n => n, n => n.Clone());

            _edges.AddRange(edgeMap.Values);
            _nodes.AddRange(nodeMap.Values);

            foreach (KeyValuePair<IMGraphEditorEdge, IMGraphEditorEdge> kvp in edgeMap)
            {
                IMGraphEditorEdge originalEdge = kvp.Key;
                IMGraphEditorEdge clonedEdge = kvp.Value;

                IMGraphEditorNode sourceNode = (IMGraphEditorNode)originalEdge.SourceDrawable;
                IMGraphEditorNode targetNode = (IMGraphEditorNode)originalEdge.TargetDrawable;

                clonedEdge.SourceDrawable = nodeMap[sourceNode];
                clonedEdge.TargetDrawable = nodeMap[targetNode];
            }

            foreach (IMGraphLayer layer in layers)
            {
                IMGraphLayer clonedLayer = layer.Clone();
                clonedLayer.UpdateNodeReferences(nodeMap);
                clonedLayer.UpdateEdgeReferences(edgeMap);
                _layers.Add(clonedLayer);
            }
        }

        internal void LoadState(out List<IMGraphEditorNode> nodes, out List<IMGraphEditorEdge> edges, out List<IMGraphLayer> layers)
        {
            nodes = _nodes;
            edges = _edges;
            layers = _layers;
        }

#pragma warning disable SYSLIB0011
        internal static void SerializeData(ApplicationState state, string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, state);
        }

        internal static ApplicationState DeserializeData(string path)
        {
            BinaryFormatter formatter = new BinaryFormatter();

            using FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return (ApplicationState)formatter.Deserialize(stream);
        }
#pragma warning restore SYSLIB0011
    }
}
