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
        Panel previewPanel = new Panel();
        public event EventHandler CanvasInvalidated;

        public LayoutPreviewControl(IMGraphLayer layer, Bitmap bmp)
        {
            InitializeComponent();
            bmp = new Bitmap(bmp);

            //PaintPreviewPanel(bmp);
            // Create a Button for preview
            Button previewButton = new Button();
            previewButton.Image = Resources.view;
            previewButton.AutoSize = true;
            previewButton.Dock = DockStyle.Fill;
            ToolTip tipBtnLayer = new ToolTip();
            tipBtnLayer.SetToolTip(previewButton, "Make layer inactive");
            //previewButton.Height = 55;
            previewButton.Click += (sender, e) =>
            {
                layer.changeActive();
                previewButton.Image = layer.Active ? Resources.view : Resources.invisible;
                if (layer.Active)
                {
                    tipBtnLayer.SetToolTip(previewButton, "Make layer active");
                }
                else
                {
                    tipBtnLayer.SetToolTip(previewButton, "Make layer inactive");
                }
                CanvasInvalidated?.Invoke(this, EventArgs.Empty);
            };

            // Create a Label to display the name of the MGraphLayer
            Label layName = new Label();
            layName.Text = layer.Name;
            layName.AutoSize = true;

            // Create a TableLayoutPanel for layout
            TableLayoutPanel previewTable = new TableLayoutPanel();
            //previewTable.AutoSize = true;
            previewTable.AutoSize = true;
            previewTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            previewTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            previewTable.ColumnCount = 2;
            previewTable.ColumnStyles.Add(new ColumnStyle() { Width = 70, SizeType = SizeType.Percent });
            previewTable.ColumnStyles.Add(new ColumnStyle() { Width = 20, SizeType = SizeType.Percent });
            previewTable.RowCount = 1;
            previewTable.RowStyles.Add(new RowStyle() { Height = 70, SizeType = SizeType.Percent });

            // Add controls to the TableLayoutPanel
            previewTable.Controls.Add(previewPanel, 0, 0);
            previewTable.Controls.Add(previewButton, 1, 0);
            previewPanel.Controls.Add(layName);
            previewTable.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            previewTable.Dock = DockStyle.Fill;

            // Add the TableLayoutPanel to the LayoutPreviewControl
            this.Controls.Add(previewTable);
            this.Dock = DockStyle.Fill;
            this.Tag = layer.Identifier;
        }

        public void PaintPreviewPanel(Bitmap bmp)
        {
            Bitmap scaledBitmap = new Bitmap(bmp, new Size(previewPanel.Width, previewPanel.Height));
            using (Graphics g = previewPanel.CreateGraphics())
            {
                g.Clear(Color.White);
                g.DrawImage(scaledBitmap, 0, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }
    }
}
