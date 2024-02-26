using MultigraphEditor.src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.layers
{
    public interface IEdgeLayer : ILayer
    {
        public int arrowSize { get; set; }
        public List<IEdgeDrawable> edges { get; set; }
    }
}
