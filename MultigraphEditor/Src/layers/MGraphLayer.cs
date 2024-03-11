using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.Src.layers
{
    internal class MGraphLayer : IMGraphLayer
    { 
        public Font Font { get; set; } = new Font("Lato", 8);
        public Color Color { get; set; } = Color.Black;
        public int Width { get; set; } = 4;
        public List<INodeDrawable> nodes { get; set; }
        public List<IEdgeDrawable> edges { get; set; }
        public bool Active { get; set; } = true;
        public int arrowSize { get; set; } = 10;
    }
}
