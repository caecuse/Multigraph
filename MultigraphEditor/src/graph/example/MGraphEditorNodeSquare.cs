using MultigraphEditor.src.layers;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph.example
{
    [Serializable]
    public class MGraphEditorNodeSquare : MGraphEditorNode
    {
        public override void Draw(Graphics g, INodeLayer l)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;

            Pen p = new Pen(l.Color, l.nodeWidth);
            using (p)
            {
                float x = GetDrawingCoordinates().Item1;
                float y = GetDrawingCoordinates().Item2;

                g.DrawRectangle(p, x, y, Diameter, Diameter);
                if (Label != null)
                {
                    DrawLabel(g, l);
                }
            }
        }
    }
}
