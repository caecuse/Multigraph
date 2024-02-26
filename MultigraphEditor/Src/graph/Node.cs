using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph
{
    public class Node
    {
        public string? Label;
        public List<Edge>? Edges;
        public List<Node>? Neighbours;
    }
}
