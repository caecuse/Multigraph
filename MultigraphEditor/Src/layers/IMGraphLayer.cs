using MultigraphEditor.src.layers;
using MultigraphEditor.Src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.Src.layers
{
    public interface IMGraphLayer : INodeLayer, IEdgeLayer
    {
        IMGraphLayer Clone();
        void UpdateNodeReferences(Dictionary<IMGraphEditorNode, IMGraphEditorNode> nodeReferences);
        void UpdateEdgeReferences(Dictionary<IMGraphEditorEdge, IMGraphEditorEdge> edgeReferences);
    }
}
