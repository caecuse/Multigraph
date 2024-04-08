using MultigraphEditor.src.graph;
using MultigraphEditor.Src.graph;
using MultigraphEditor.Src.layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace MultigraphEditor.Src.design
{
    internal class ApplicationState
    {
        private List<IMGraphEditorNode> _nodes = new List<IMGraphEditorNode>();
        private List<IMGraphEditorEdge> _edges = new List<IMGraphEditorEdge>();
        private List<IMGraphLayer> _layers = new List<IMGraphLayer>();

        internal ApplicationState(List<IMGraphEditorEdge> edge, List<IMGraphEditorNode> node, List<IMGraphLayer> layers)
        {
            var edgeMap = edge.ToDictionary(e => e, e => e.Clone());
            var nodeMap = node.ToDictionary(n => n, n => n.Clone());

            _edges.AddRange(edgeMap.Values);
            _nodes.AddRange(nodeMap.Values);

            foreach (var kvp in edgeMap)
            {
                var originalEdge = kvp.Key;
                var clonedEdge = kvp.Value;

                IMGraphEditorNode sourceNode = (IMGraphEditorNode)originalEdge.SourceDrawable;
                IMGraphEditorNode targetNode = (IMGraphEditorNode)originalEdge.TargetDrawable;

                clonedEdge.SourceDrawable = nodeMap[sourceNode];
                clonedEdge.TargetDrawable = nodeMap[targetNode];
            }

            foreach (var layer in layers)
            {
                var clonedLayer = layer.Clone();
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

        internal void SerializeState()
        {
            // Serialize the state
        }

    }
}
