using MultigraphEditor.Forms;
using MultigraphEditor.src.layers;
using MultigraphEditor.Src.graph;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph
{
    public class MGraphEditorNode : IMGraphEditorNode
    {
        [ExcludeFromForm]
        public int Identifier { get; set; }
        public string? Label { get; set; }
        [ExcludeFromForm]
        public List<IEdge> Edges { get; set; } = new List<IEdge>();
        [ExcludeFromForm]
        public List<INode> Neighbours { get; set; } = new List<INode>();
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
        }

        public int GetIdentifier()
        {
            return NodeCounter++;
        }

        public void AddEdge(IEdge e)
        {
            Edges.Add(e);
        }

        public void RemoveEdge(IEdge e)
        {
            Edges.Remove(e);
        }

        public void AddNeighbour(INode n)
        {
            Neighbours.Add(n);
        }

        public void RemoveNeighbour(INode n)
        {
            Neighbours.Remove(n);
        }

        public (float, float) GetCoordinates()
        {
            return (X, Y);
        }

        public (float, float) GetDrawingCoordinates()
        {
            return (X - Diameter / 2, Y - Diameter / 2);
        }

        public void Draw(object sender, PaintEventArgs e, INodeLayer l)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen p = new Pen(l.Color, l.Width);
            using (p)
            {
                float x = GetDrawingCoordinates().Item1;
                float y = GetDrawingCoordinates().Item2;

                e.Graphics.DrawEllipse(p, x, y, Diameter, Diameter);
                if (Label != null)
                {
                    DrawLabel(sender, e, l);
                }
            }
        }

        public void DrawLabel(object sender, PaintEventArgs e, INodeLayer l)
        {
            // Calculate label position and size
            SizeF textSize = e.Graphics.MeasureString(Label, l.Font);
            float labelX = GetDrawingCoordinates().Item1 + (Diameter - textSize.Width) / 2;     // Center horizontally
            float labelY = GetDrawingCoordinates().Item2 + (Diameter - textSize.Height) / 2;    // Center vertically

            // Draw label
            Color textColor = l.Color;
            e.Graphics.DrawString(Label, l.Font, new SolidBrush(textColor), labelX, labelY);
        }

        public bool IsInside(float x, float y)
        {
            bool a = x >= GetDrawingCoordinates().Item1 && x <= GetDrawingCoordinates().Item1 + Diameter &&
                               y >= GetDrawingCoordinates().Item2 && y <= GetDrawingCoordinates().Item2 + Diameter;
            return a;
        }
    }
}
