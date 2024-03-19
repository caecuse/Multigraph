namespace MultigraphEditor
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            MainLayout = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            button1 = new Button();
            ViewBtn = new Button();
            MoveBtn = new Button();
            AddBtn = new Button();
            ConnectBtn = new Button();
            AlgorithmsBtn = new Button();
            SettingsBtn = new Button();
            GraphBtn = new Button();
            canvas = new Src.design.DoubleBufferedPanel();
            LayoutPanel = new TableLayoutPanel();
            MainLayout.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // MainLayout
            // 
            MainLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            MainLayout.ColumnCount = 2;
            MainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            MainLayout.Controls.Add(flowLayoutPanel1, 0, 1);
            MainLayout.Controls.Add(canvas, 0, 0);
            MainLayout.Controls.Add(LayoutPanel, 1, 0);
            MainLayout.Dock = DockStyle.Fill;
            MainLayout.Location = new Point(0, 0);
            MainLayout.Name = "MainLayout";
            MainLayout.RowCount = 2;
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            MainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            MainLayout.Size = new Size(800, 450);
            MainLayout.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            MainLayout.SetColumnSpan(flowLayoutPanel1, 2);
            flowLayoutPanel1.Controls.Add(button1);
            flowLayoutPanel1.Controls.Add(ViewBtn);
            flowLayoutPanel1.Controls.Add(MoveBtn);
            flowLayoutPanel1.Controls.Add(AddBtn);
            flowLayoutPanel1.Controls.Add(ConnectBtn);
            flowLayoutPanel1.Controls.Add(AlgorithmsBtn);
            flowLayoutPanel1.Controls.Add(SettingsBtn);
            flowLayoutPanel1.Controls.Add(GraphBtn);
            flowLayoutPanel1.Location = new Point(131, 407);
            flowLayoutPanel1.Margin = new Padding(0);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(537, 40);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(3, 3);
            button1.Name = "button1";
            button1.Size = new Size(0, 0);
            button1.TabIndex = 0;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // ViewBtn
            // 
            ViewBtn.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ViewBtn.Image = Properties.Resources.view;
            ViewBtn.ImageAlign = ContentAlignment.MiddleLeft;
            ViewBtn.Location = new Point(9, 3);
            ViewBtn.Name = "ViewBtn";
            ViewBtn.Size = new Size(55, 23);
            ViewBtn.TabIndex = 1;
            ViewBtn.Text = "View";
            ViewBtn.TextAlign = ContentAlignment.MiddleRight;
            ViewBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            ViewBtn.UseVisualStyleBackColor = true;
            ViewBtn.Click += ViewBtn_Click;
            // 
            // MoveBtn
            // 
            MoveBtn.Image = Properties.Resources.move;
            MoveBtn.ImageAlign = ContentAlignment.MiddleLeft;
            MoveBtn.Location = new Point(70, 3);
            MoveBtn.Name = "MoveBtn";
            MoveBtn.Size = new Size(65, 23);
            MoveBtn.TabIndex = 2;
            MoveBtn.Text = "Move";
            MoveBtn.TextAlign = ContentAlignment.MiddleRight;
            MoveBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            MoveBtn.UseVisualStyleBackColor = true;
            MoveBtn.Click += MoveBtn_Click;
            // 
            // AddBtn
            // 
            AddBtn.Image = Properties.Resources.add;
            AddBtn.ImageAlign = ContentAlignment.MiddleLeft;
            AddBtn.Location = new Point(141, 3);
            AddBtn.Name = "AddBtn";
            AddBtn.Size = new Size(55, 23);
            AddBtn.TabIndex = 3;
            AddBtn.Text = "Add";
            AddBtn.TextAlign = ContentAlignment.MiddleRight;
            AddBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            AddBtn.Click += AddBtn_Click;
            // 
            // ConnectBtn
            // 
            ConnectBtn.Image = Properties.Resources.connect;
            ConnectBtn.ImageAlign = ContentAlignment.MiddleLeft;
            ConnectBtn.Location = new Point(202, 3);
            ConnectBtn.Name = "ConnectBtn";
            ConnectBtn.Size = new Size(80, 23);
            ConnectBtn.TabIndex = 4;
            ConnectBtn.Text = "Connect";
            ConnectBtn.TextAlign = ContentAlignment.MiddleRight;
            ConnectBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            ConnectBtn.UseVisualStyleBackColor = true;
            ConnectBtn.Click += ConnectBtn_Click;
            // 
            // AlgorithmsBtn
            // 
            AlgorithmsBtn.Image = Properties.Resources.algo;
            AlgorithmsBtn.ImageAlign = ContentAlignment.MiddleLeft;
            AlgorithmsBtn.Location = new Point(288, 3);
            AlgorithmsBtn.Name = "AlgorithmsBtn";
            AlgorithmsBtn.Size = new Size(90, 23);
            AlgorithmsBtn.TabIndex = 5;
            AlgorithmsBtn.Text = "Algorithms";
            AlgorithmsBtn.TextAlign = ContentAlignment.MiddleRight;
            AlgorithmsBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            AlgorithmsBtn.UseVisualStyleBackColor = true;
            // 
            // SettingsBtn
            // 
            SettingsBtn.Image = Properties.Resources.setting;
            SettingsBtn.ImageAlign = ContentAlignment.MiddleLeft;
            SettingsBtn.Location = new Point(384, 3);
            SettingsBtn.Name = "SettingsBtn";
            SettingsBtn.Size = new Size(75, 23);
            SettingsBtn.TabIndex = 7;
            SettingsBtn.Text = "Settings";
            SettingsBtn.TextAlign = ContentAlignment.MiddleRight;
            SettingsBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            SettingsBtn.UseVisualStyleBackColor = true;
            SettingsBtn.Click += SettingsBtn_Click;
            // 
            // GraphBtn
            // 
            GraphBtn.Image = Properties.Resources.graph;
            GraphBtn.ImageAlign = ContentAlignment.MiddleLeft;
            GraphBtn.Location = new Point(465, 3);
            GraphBtn.Name = "GraphBtn";
            GraphBtn.Size = new Size(66, 23);
            GraphBtn.TabIndex = 6;
            GraphBtn.Text = "Graph";
            GraphBtn.TextAlign = ContentAlignment.MiddleRight;
            GraphBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            GraphBtn.UseVisualStyleBackColor = true;
            // 
            // canvas
            // 
            canvas.Dock = DockStyle.Fill;
            canvas.Location = new Point(6, 6);
            canvas.Name = "canvas";
            canvas.Size = new Size(585, 395);
            canvas.TabIndex = 1;
            canvas.Paint += canvas_Paint;
            // 
            // LayoutPanel
            // 
            LayoutPanel.AutoScroll = true;
            LayoutPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            LayoutPanel.ColumnCount = 1;
            LayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            LayoutPanel.Dock = DockStyle.Fill;
            LayoutPanel.Location = new Point(600, 6);
            LayoutPanel.Name = "LayoutPanel";
            LayoutPanel.RowCount = 1;
            LayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            LayoutPanel.Size = new Size(194, 395);
            LayoutPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(MainLayout);
            Name = "MainForm";
            Text = "Multigraph Editor";
            Load += MainForm_Load;
            MainLayout.ResumeLayout(false);
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainLayout;
        private Src.design.DoubleBufferedPanel canvas;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button button1;
        private Button ViewBtn;
        private Button MoveBtn;
        private Button AddBtn;
        private Button ConnectBtn;
        private Button AlgorithmsBtn;
        private Button SettingsBtn;
        private Button GraphBtn;
        private TableLayoutPanel LayoutPanel;
    }
}
