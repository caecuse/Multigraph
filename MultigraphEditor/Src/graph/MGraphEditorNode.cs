using MultigraphEditor.Forms;
using MultigraphEditor.src.layers;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization.Formatters.Binary;

namespace MultigraphEditor.src.graph
{
    [Serializable]
    public class MGraphEditorNode : IMGraphEditorNode
    {
        [ExcludeFromForm]
        public int Identifier { get; set; }
        [ExcludeFromForm]
        private Guid _guid { get; set; }
        public string? Label { get; set; }
        [ExcludeFromForm]
        public List<IEdge> Edges { get; set; } = new List<IEdge>();
        [ExcludeFromForm]
        public float X { get; set; }
        [ExcludeFromForm]
        public float Y { get; set; }
        public float Diameter { get; set; } = 20;
        [ExcludeFromForm]
        public static int NodeCounter = 0;

        public MGraphEditorNode()
        {
            Identifier = GetIdentifier();
            Label = Identifier.ToString();
            _guid = Guid.NewGuid();
        }

        public virtual int GetIdentifier()
        {
            return NodeCounter++;
        }

        public virtual void AddEdge(IEdge e)
        {
            if (!Edges.Contains(e))
            {
                Edges.Add(e);
            }
        }

        public virtual void RemoveEdge(IEdge e)
        {
            Edges.Remove(e);
        }

        public virtual (float, float) GetCoordinates()
        {
            return (X, Y);
        }

        public virtual (float, float) GetDrawingCoordinates()
        {
            return (X - (Diameter / 2), Y - (Diameter / 2));
        }

        public virtual void Draw(Graphics g, INodeLayer l)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen p = new Pen(l.Color, l.nodeWidth);
            using (p)
            {
                float x = GetDrawingCoordinates().Item1;
                float y = GetDrawingCoordinates().Item2;

                g.DrawEllipse(p, x, y, Diameter, Diameter);
                if (Label != null)
                {
                    DrawLabel(g, l);
                }
            }
        }

        public virtual void DrawLabel(Graphics g, INodeLayer l)
        {
            // Calculate label position and size
            SizeF textSize = g.MeasureString(Label, l.Font);
            float labelX = GetDrawingCoordinates().Item1 + ((Diameter - textSize.Width) / 2);     // Center horizontally
            float labelY = GetDrawingCoordinates().Item2 + ((Diameter - textSize.Height) / 2);    // Center vertically

            // Draw label
            Color textColor = l.Color;
            g.DrawString(Label, l.Font, new SolidBrush(textColor), labelX, labelY);
        }

        public bool IsInside(float x, float y)
        {
            bool a = x >= GetDrawingCoordinates().Item1 && x <= GetDrawingCoordinates().Item1 + Diameter &&
                               y >= GetDrawingCoordinates().Item2 && y <= GetDrawingCoordinates().Item2 + Diameter;
            return a;
        }

#pragma warning disable SYSLIB0011
        public IMGraphEditorNode Clone()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using MemoryStream stream = new MemoryStream();
            formatter.Serialize(stream, this);
            stream.Seek(0, SeekOrigin.Begin);
            return (IMGraphEditorNode)formatter.Deserialize(stream);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                MGraphEditorNode other = (MGraphEditorNode)obj;
                return _guid == other._guid;
            }
        }

        public override int GetHashCode()
        {
            return _guid.GetHashCode();
        }
    }
}
