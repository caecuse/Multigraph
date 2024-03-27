using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph
{
    public interface INode
    {
        public string? Label { get; set; }
        public List<IEdge> Edges { get; set; }
        public void AddEdge(IEdge e);
        public void RemoveEdge(IEdge e);
    }
}
