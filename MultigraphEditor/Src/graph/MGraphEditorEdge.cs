using MultigraphEditor.src.layers;
using MultigraphEditor.Src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph
{
    public class MGraphEditorEdge: IMGraphEditorEdge
    {
        public int Identifier { get; set; }
        public string Label { get; set; }
        public INode Source { get; set; }
        public INode Target { get; set; }
        public bool Directed { get; set; }
        public bool Bidirectional { get; set; }
        public int Weight { get; set; }
        public PointF SourcePoint { get; set; }
        public PointF TargetPoint { get; set; }
        public required INodeDrawable SourceDrawable { get; set; }
        public required INodeDrawable TargetDrawable { get; set; }

        public static int EdgeCounter = 0;

        public MGraphEditorEdge()
        {
            Identifier = GetIdentifier();
        }
        public int GetIdentifier()
        {
            return EdgeCounter++;
        }

        public void Draw(object sender, PaintEventArgs e, MGraphEditorEdgeLayer l)
        {
            Pen pen = new Pen(l.Color, l.Width);

            if (SourceDrawable != TargetDrawable)
            {
                float dx = TargetDrawable.X - SourceDrawable.X;
                float dy = TargetDrawable.Y - SourceDrawable.Y;
                float length = (float)Math.Sqrt(dx * dx + dy * dy);
                float unitDx = dx / length;
                float unitDy = dy / length;
                float sourceRadius = SourceDrawable.Diameter / 2;
                float targetRadius = TargetDrawable.Diameter / 2;
                float sourceX = SourceDrawable.X + sourceRadius * unitDx;
                float sourceY = SourceDrawable.Y + sourceRadius * unitDy;
                float targetX = TargetDrawable.X - targetRadius * unitDx;
                float targetY = TargetDrawable.Y - targetRadius * unitDy;

                // Draw the edge
                e.Graphics.DrawLine(pen, sourceX, sourceY, targetX, targetY);
                if (!Directed)
                {
                    DrawArrow(sender, e, l);
                }
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void DrawArrow(object sender, PaintEventArgs e, MGraphEditorEdgeLayer l)
        {
            float dx = TargetDrawable.X - SourceDrawable.X;
            float dy = TargetDrawable.Y - SourceDrawable.Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);
            float unitDx = dx / length;
            float unitDy = dy / length;
            float sourceRadius = SourceDrawable.Diameter / 2;
            float targetRadius = TargetDrawable.Diameter / 2;

            // Calculate the coords of arrow tip
            int arrowSize = l.arrowSize;

            float arrowHeadX = TargetDrawable.X - (unitDx * targetRadius);
            float arrowHeadY = TargetDrawable.Y - (unitDy * targetRadius);
            float arrowHeadAngle = (float)Math.Atan2(dy, dx);

            using (SolidBrush brush = new SolidBrush(l.Color))
            {
                PointF[] points = new PointF[3];
                points[0] = new PointF(arrowHeadX, arrowHeadY);
                points[1] = new PointF(arrowHeadX - (arrowSize * (float)Math.Cos(arrowHeadAngle - (Math.PI / 6))), arrowHeadY - (arrowSize * (float)Math.Sin(arrowHeadAngle - (Math.PI / 6))));
                points[2] = new PointF(arrowHeadX - (arrowSize * (float)Math.Cos(arrowHeadAngle + (Math.PI / 6))), arrowHeadY - (arrowSize * (float)Math.Sin(arrowHeadAngle + (Math.PI / 6))));
                e.Graphics.FillPolygon(brush, points);
            }

            if (Bidirectional)
            {
                float arrowHeadX2 = SourceDrawable.X + (unitDx * sourceRadius);
                float arrowHeadY2 = SourceDrawable.Y + (unitDy * sourceRadius);
                float arrowHeadAngle2 = (float)Math.Atan2(-dy, -dx);

                using (SolidBrush brush = new SolidBrush(l.Color))
                {
                    PointF[] points = new PointF[3];
                    points[0] = new PointF(arrowHeadX2, arrowHeadY2);
                    points[1] = new PointF(arrowHeadX2 - (arrowSize * (float)Math.Cos(arrowHeadAngle2 - (Math.PI / 6))), arrowHeadY2 - (arrowSize * (float)Math.Sin(arrowHeadAngle2 - (Math.PI / 6))));
                    points[2] = new PointF(arrowHeadX2 - (arrowSize * (float)Math.Cos(arrowHeadAngle2 + (Math.PI / 6))), arrowHeadY2 - (arrowSize * (float)Math.Sin(arrowHeadAngle2 + (Math.PI / 6))));
                    e.Graphics.FillPolygon(brush, points);
                }
            }
        }

        public void DrawLabel(object sender, PaintEventArgs e, MGraphEditorEdgeLayer l)
        {
            float dx = TargetDrawable.X - SourceDrawable.X;
            float dy = TargetDrawable.Y - SourceDrawable.Y;
            float length = (float)Math.Sqrt(dx * dx + dy * dy);
            float unitDx = dx / length;
            float unitDy = dy / length;
            float sourceRadius = SourceDrawable.Diameter / 2;
            float targetRadius = TargetDrawable.Diameter / 2;
            float sourceX = SourceDrawable.X + sourceRadius * unitDx;
            float sourceY = SourceDrawable.Y + sourceRadius * unitDy;
            float targetX = TargetDrawable.X - targetRadius * unitDx;
            float targetY = TargetDrawable.Y - targetRadius * unitDy;

            float labelX = (sourceX + targetX) / 2;
            float labelY = (sourceY + targetY) / 2 - 6;

            using (SolidBrush brush = new SolidBrush(l.Color))
            {
                SizeF textSize = e.Graphics.MeasureString(Weight.ToString() + "\n" + Label, l.Font);
                PointF labelPosition = new PointF(labelX - textSize.Width / 2, labelY - textSize.Height / 2);

                e.Graphics.DrawString(Weight.ToString() + "\n" + Label, l.Font, brush, labelPosition);
            }
        }

        public bool IsInside(float x, float y)
        {
            float tolerance = 5f;
            float lineDistance = PointToLineDistance(x, y, SourcePoint.X, SourcePoint.Y, TargetPoint.X, TargetPoint.Y);
            bool isInsideEllipse = IsPointInsideEllipse(x, y, SourceDrawable.X + (SourceDrawable.Diameter / 4) + SourceDrawable.Diameter, SourceDrawable.Y + SourceDrawable.Diameter, SourceDrawable.Diameter, SourceDrawable.Diameter);

            if (lineDistance <= tolerance || isInsideEllipse)
            {
                return true; // Point is inside the line connecting two nodes
            }

            return false; // Point is not inside any line
        }

        public bool IsPointInsideEllipse(float x, float y, float centerX, float centerY, float width, float height)
        {
            // Calculate normalized coordinates
            float normalizedX = (x - centerX) / (width / 2);
            float normalizedY = (y - centerY) / (height / 2);

            // Check if the point is inside the ellipse equation (x^2 / a^2) + (y^2 / b^2) <= 1
            return (normalizedX * normalizedX) + (normalizedY * normalizedY) <= 1;
        }

        private float PointToLineDistance(float x, float y, float lineX1, float lineY1, float lineX2, float lineY2)
        {
            float A = x - lineX1;
            float B = y - lineY1;
            float C = lineX2 - lineX1;
            float D = lineY2 - lineY1;

            float dot = (A * C) + (B * D);
            float len_sq = (C * C) + (D * D);
            float param = dot / len_sq;

            float nearestX, nearestY;

            if (param < 0)
            {
                nearestX = lineX1;
                nearestY = lineY1;
            }
            else if (param > 1)
            {
                nearestX = lineX2;
                nearestY = lineY2;
            }
            else
            {
                nearestX = lineX1 + (param * C);
                nearestY = lineY1 + (param * D);
            }

            float dx = x - nearestX;
            float dy = y - nearestY;

            return (float)Math.Sqrt((dx * dx) + (dy * dy));
        }
    }
}
