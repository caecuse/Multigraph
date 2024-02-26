using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph
{
    public class Edge
    {
        public string? Label;
        public required Node Source;
        public required Node Target;
        public required bool Directed;
        public required bool Bidirectional;
        public required int Weight;
    }
}
