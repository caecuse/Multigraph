using MultigraphEditor.src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.layers
{
    public class MGraphEditorNodeLayer : INodeLayer
    {
        public Font Font { get; set; } = new Font("Arial", 12);
        public Color Color { get; set; } = Color.Black;
        public int Width { get; set; } = 4;
        public List<INodeDrawable> nodes { get; set; }
        public bool Active { get; set; } = true;
    }
}
