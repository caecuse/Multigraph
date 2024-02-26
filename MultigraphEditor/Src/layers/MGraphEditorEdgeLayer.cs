using MultigraphEditor.src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.layers
{
    public class MGraphEditorEdgeLayer : IEdgeLayer
    {
        public Font Font { get; set; } = new Font("Arial", 12);
        public Color Color { get; set; } = Color.Black;
        public int arrowSize { get; set; } = 12;
        public int Width { get; set; } = 3;
        public bool Active { get; set; } = true;
        public List<IEdgeDrawable> edges { get; set; }
    }
}
