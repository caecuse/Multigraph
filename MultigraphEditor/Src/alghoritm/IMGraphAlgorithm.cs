using MultigraphEditor.src.graph;
using MultigraphEditor.Src.graph;
using MultigraphEditor.Src.layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.Src.alghoritm
{
    internal interface IMGraphAlgorithm
    {
        List<INode> FindPath(INode startNode, INode endNode, IMGraphLayer targetLayer);
        String Name { get; }
    }
}
