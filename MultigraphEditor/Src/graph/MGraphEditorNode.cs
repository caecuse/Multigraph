using MultigraphEditor.src.layers;
using MultigraphEditor.Src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph
{
    public class MGraphEditorNode : IMGraphEditorNode
    {
        public int Identifier { get; set; }
        public string? Label { get; set; }
        public List<IEdge> Edges { get; set; } = new List<IEdge>();
        public List<INode> Neighbours { get; set; } = new List<INode>();
        public float X { get; set; }
        public float Y { get; set; }
        public float Diameter { get; set; } = 20;
        public static int NodeCounter = 0;

        public MGraphEditorNode()
        {
            Identifier = GetIdentifier();
        }
        public int GetIdentifier()
        {
            return NodeCounter++;
        }

        public (float, float) GetCoordinates()
        {
            return (X, Y);
        }

        public (float, float) GetDrawingCoordinates()
        {
            return (X - Diameter / 2, Y - Diameter / 2);
        }

        public void Draw(object sender, PaintEventArgs e, MGraphEditorNodeLayer l)
        {
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

        public void DrawLabel(object sender, PaintEventArgs e, MGraphEditorNodeLayer l)
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
