using MultigraphEditor.Forms;
using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using MultigraphEditor.Src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.Src.layers
{
    [Serializable]
    internal class MGraphLayer : IMGraphLayer
    { 
        public Font Font { get; set; } = new Font("Lato", 8);
        public Color Color { get; set; } = Color.Black;
        public int nodeWidth { get; set; } = 4;
        public int edgeWidth { get; set; } = 4;
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

#pragma warning disable SYSLIB0011
        public IMGraphLayer Clone()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (IMGraphLayer)formatter.Deserialize(stream);
            }
        }

        public void UpdateNodeReferences(Dictionary<IMGraphEditorNode, IMGraphEditorNode> nodeMap)
        {
            nodes = nodes.Select(n => nodeMap[n]).ToList();
        }

        public void UpdateEdgeReferences(Dictionary<IMGraphEditorEdge, IMGraphEditorEdge> edgeMap)
        {
            edges = edges.Select(e => edgeMap[e]).ToList();
        }
    }
}
