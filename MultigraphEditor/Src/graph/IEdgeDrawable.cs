using MultigraphEditor.src.layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph
{
    public interface IEdgeDrawable : IDrawable
    {
        internal PointF SourcePoint { get; set; }
        internal PointF TargetPoint { get; set; }
        internal INodeDrawable SourceDrawable { get; set; }
        internal INodeDrawable TargetDrawable { get; set; }

        public void Draw(object sender, PaintEventArgs e, MGraphEditorEdgeLayer l);
        public void DrawLabel(object sender, PaintEventArgs e, MGraphEditorEdgeLayer l);
        public void DrawArrow(object sender, PaintEventArgs e, MGraphEditorEdgeLayer l);
    }
}
