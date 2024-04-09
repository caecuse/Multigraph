using MultigraphEditor.Forms;
using MultigraphEditor.Properties;
using MultigraphEditor.Src.layers;

namespace MultigraphEditor.Src.design
{
    public partial class LayoutPreviewControl : Control
    {
        Panel previewPanel = new Panel();
        public IMGraphLayer Layer { get; set; }
        public event EventHandler CanvasInvalidated;
        public event EventHandler<IMGraphLayer> LayerDeleted;

        public LayoutPreviewControl(IMGraphLayer layer, Bitmap bmp)
        {
            InitializeComponent();
            Layer = layer;
            bmp = new Bitmap(bmp);
            TableLayoutPanel optionsPanel = new TableLayoutPanel();
            optionsPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            optionsPanel.ColumnCount = 1;
            optionsPanel.ColumnStyles.Add(new ColumnStyle() { Width = 100, SizeType = SizeType.Percent });
            optionsPanel.RowCount = 3;
            optionsPanel.RowStyles.Add(new RowStyle() { Height = 33, SizeType = SizeType.Percent });
            optionsPanel.RowStyles.Add(new RowStyle() { Height = 33, SizeType = SizeType.Percent });
            optionsPanel.RowStyles.Add(new RowStyle() { Height = 33, SizeType = SizeType.Percent });

            Button editButton = new Button();
            editButton.Image = Resources.edit;
            editButton.AutoSize = true;
            editButton.Dock = DockStyle.Fill;
            editButton.Margin = new Padding(0, 0, 0, 0);
            ToolTip tipBtnEdit = new ToolTip();
            tipBtnEdit.SetToolTip(editButton, "Edit layer");
            editButton.Click += (sender, e) =>
            {
                EditForm layerEditor = new EditForm(layer);
                layerEditor.ShowDialog();
                CanvasInvalidated?.Invoke(this, EventArgs.Empty);
            };

            Button deleteButton = new Button();
            deleteButton.Image = Resources.trash;
            deleteButton.AutoSize = true;
            deleteButton.Dock = DockStyle.Fill;
            deleteButton.Margin = new Padding(0, 0, 0, 0);
            ToolTip tipBtnDelete = new ToolTip();
            tipBtnDelete.SetToolTip(deleteButton, "Delete layer");
            deleteButton.Click += (sender, e) =>
            {
                LayerDeleted?.Invoke(this, layer);
                CanvasInvalidated?.Invoke(this, EventArgs.Empty);
            };

            Button previewButton = new Button();
            previewButton.Image = Resources.view;
            previewButton.AutoSize = true;
            previewButton.Dock = DockStyle.Fill;
            previewButton.Margin = new Padding(0, 0, 0, 0);
            previewButton.Image = layer.Active ? Resources.view : Resources.invisible;

            ToolTip tipBtnLayer = new ToolTip();
            if (layer.Active)
            {
                tipBtnLayer.SetToolTip(previewButton, "Make layer active");
            }
            else
            {
                tipBtnLayer.SetToolTip(previewButton, "Make layer inactive");
            }
            previewButton.Click += (sender, e) =>
            {
                layer.changeActive();
                CanvasInvalidated?.Invoke(this, EventArgs.Empty);
            };

            Label layName = new Label();
            layName.Text = layer.Name;
            layName.AutoSize = true;
            layName.BackColor = Color.White;
            previewPanel.BackColor = Color.White;

            TableLayoutPanel previewTable = new TableLayoutPanel();
            previewTable.AutoSize = true;
            previewTable.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            previewTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            previewTable.ColumnCount = 2;
            previewTable.ColumnStyles.Add(new ColumnStyle() { Width = 70, SizeType = SizeType.Percent });
            previewTable.ColumnStyles.Add(new ColumnStyle() { Width = 20, SizeType = SizeType.Percent });
            previewTable.RowCount = 1;
            previewTable.RowStyles.Add(new RowStyle() { Height = 70, SizeType = SizeType.Percent });

            previewTable.Controls.Add(previewPanel, 0, 0);
            optionsPanel.Controls.Add(deleteButton, 0, 0);
            optionsPanel.Controls.Add(previewButton, 0, 1);
            optionsPanel.Controls.Add(editButton, 0, 2);
            previewTable.Controls.Add(optionsPanel, 1, 0);
            previewPanel.Controls.Add(layName);
            previewTable.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            previewTable.Dock = DockStyle.Fill;

            this.Controls.Add(previewTable);
            this.Dock = DockStyle.Fill;
            this.Tag = layer.Identifier;
            this.MouseDown += LayoutPreviewControl_MouseDown;
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

        public bool IsInside(float x, float y)
        {
            Point p = new Point((int)x, (int)y);
            return this.Bounds.Contains(p);
        }

        private void LayoutPreviewControl_MouseDown(object sender, MouseEventArgs e)
        {
            return;
        }
    }
}
