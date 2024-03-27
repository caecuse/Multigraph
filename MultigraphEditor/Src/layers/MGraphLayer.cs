using MultigraphEditor.Forms;
using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using MultigraphEditor.Src.graph;
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
        [ExcludeFromForm]
        public List<IMGraphEditorNode> nodes { get; set; }
        [ExcludeFromForm]
        public List<IMGraphEditorEdge> edges { get; set; }
        [ExcludeFromForm]
        public bool Active { get; set; } = true;
        public int arrowSize { get; set; } = 10;
        [ExcludeFromForm]
        public int Identifier { get; set; }
        public String Name { get; set; }
        public void changeActive()
        {
            Active = !Active;
        }
        static int id = 0;
        public MGraphLayer()
        {
            Identifier = id++;
            Name = "Layer " + Identifier;
        }
    }
}
