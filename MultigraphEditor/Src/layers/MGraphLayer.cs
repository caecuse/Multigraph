using MultigraphEditor.Forms;
using MultigraphEditor.Src.graph;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultigraphEditor.Src.layers
{
    [Serializable]
    public class MGraphLayer : IMGraphLayer
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
        [ExcludeFromForm]
        private Guid _guid { get; set; }
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
            _guid = Guid.NewGuid();
        }

#pragma warning disable SYSLIB0011
        public IMGraphLayer Clone()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
            return (IMGraphLayer)formatter.Deserialize(stream);
        }
#pragma warning restore SYSLIB0011

        public void UpdateNodeReferences(Dictionary<IMGraphEditorNode, IMGraphEditorNode> nodeMap)
        {
            nodes = nodes.Select(n => nodeMap[n]).ToList();
        }

        public void UpdateEdgeReferences(Dictionary<IMGraphEditorEdge, IMGraphEditorEdge> edgeMap)
        {
            edges = edges.Select(e => edgeMap[e]).ToList();
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                MGraphLayer other = (MGraphLayer)obj;
                return _guid == other._guid;
            }
        }

        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }
    }
}
