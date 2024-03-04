using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph
{
    public interface IEdge
    {
        public string? Label { get; set; }
        public INode Source { get; set; }
        public INode Target { get; set; }
        public bool Directed { get; set; }
        public bool Bidirectional { get; set; }
        public int Weight { get; set; }
    }
}
