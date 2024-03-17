using MultigraphEditor.Properties;
using MultigraphEditor.src.graph;
using MultigraphEditor.Src.graph;
using MultigraphEditor.src.layers;
using MultigraphEditor.Src.layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultigraphEditor.Src.design
{
    public partial class LayoutPreviewControl : Control
    {
        DoubleBufferedPanel previewPanel = new DoubleBufferedPanel();
        IMGraphLayer mGraphLayer;

        public LayoutPreviewControl(IMGraphLayer layer)
        {
            InitializeComponent();
            mGraphLayer = layer;

            Button previewButton = new Button();
            previewButton.Image = Resources.view;
            previewButton.Click += (sender, e) =>
            {
                layer.changeActive();
                previewButton.Image = layer.Active ? Resources.view : Resources.invisible;
            };
            Label layName = new Label();
            layName.Text = layer.Name;
            layName.AutoSize = true;
            previewPanel.Controls.Add(layName);

            PaintPreviewPanel();

            FlowLayoutPanel previewFlow = new FlowLayoutPanel();
            previewFlow.Dock = DockStyle.Fill;
            previewFlow.AutoSize = true;
            previewFlow.Controls.Add(previewButton);
            this.Controls.Add(previewPanel);
        }

        protected void PaintPreviewPanel()
        {
            foreach (INodeDrawable node in mGraphLayer.nodes)
            {
                node.Draw(previewPanel, null, mGraphLayer);
            }
            foreach (IEdgeDrawable edge in mGraphLayer.edges)
            {
                edge.Draw(previewPanel, null, mGraphLayer);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
