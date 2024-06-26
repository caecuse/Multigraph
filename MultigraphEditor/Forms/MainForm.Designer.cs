﻿using MultigraphEditor.src.design;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            MainLayout = new TableLayoutPanel();
            ButtonPanel = new FlowLayoutPanel();
            button1 = new Button();
            ViewBtn = new Button();
            MoveBtn = new Button();
            AddBtn = new Button();
            ConnectBtn = new Button();
            AlgorithmsBtn = new Button();
            RemoveBtn = new Button();
            UndoBtn = new Button();
            GraphBtn = new Button();
            canvas = new DoubleBufferedPanel();
            LayoutPanel = new TableLayoutPanel();
            MainLayout.SuspendLayout();
            ButtonPanel.SuspendLayout();
            SuspendLayout();
            // 
            // MainLayout
            // 
            MainLayout.CellBorderStyle = TableLayoutPanelCellBorderStyle.InsetDouble;
            MainLayout.ColumnCount = 2;
            MainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            MainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            MainLayout.Controls.Add(ButtonPanel, 0, 1);
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
            // ButtonPanel
            // 
            ButtonPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            MainLayout.SetColumnSpan(ButtonPanel, 2);
            ButtonPanel.Controls.Add(button1);
            ButtonPanel.Controls.Add(ViewBtn);
            ButtonPanel.Controls.Add(MoveBtn);
            ButtonPanel.Controls.Add(AddBtn);
            ButtonPanel.Controls.Add(ConnectBtn);
            ButtonPanel.Controls.Add(AlgorithmsBtn);
            ButtonPanel.Controls.Add(RemoveBtn);
            ButtonPanel.Controls.Add(UndoBtn);
            ButtonPanel.Controls.Add(GraphBtn);
            ButtonPanel.Location = new Point(70, 407);
            ButtonPanel.Margin = new Padding(0);
            ButtonPanel.Name = "ButtonPanel";
            ButtonPanel.Size = new Size(660, 40);
            ButtonPanel.TabIndex = 0;
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
            ViewBtn.Size = new Size(62, 23);
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
            MoveBtn.Location = new Point(77, 3);
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
            AddBtn.Location = new Point(148, 3);
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
            ConnectBtn.Location = new Point(209, 3);
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
            AlgorithmsBtn.Location = new Point(295, 3);
            AlgorithmsBtn.Name = "AlgorithmsBtn";
            AlgorithmsBtn.Size = new Size(99, 23);
            AlgorithmsBtn.TabIndex = 5;
            AlgorithmsBtn.Text = "Algorithms";
            AlgorithmsBtn.TextAlign = ContentAlignment.MiddleRight;
            AlgorithmsBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            AlgorithmsBtn.UseVisualStyleBackColor = true;
            AlgorithmsBtn.Click += AlgorithmsBtn_Click;
            // 
            // RemoveBtn
            // 
            RemoveBtn.Image = Properties.Resources.remove;
            RemoveBtn.Location = new Point(400, 3);
            RemoveBtn.Name = "RemoveBtn";
            RemoveBtn.Size = new Size(116, 23);
            RemoveBtn.TabIndex = 7;
            RemoveBtn.Text = "Remove object";
            RemoveBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            RemoveBtn.UseVisualStyleBackColor = true;
            RemoveBtn.Click += RemoveBtn_Click;
            // 
            // UndoBtn
            // 
            UndoBtn.Image = Properties.Resources.undo;
            UndoBtn.ImageAlign = ContentAlignment.MiddleRight;
            UndoBtn.Location = new Point(522, 3);
            UndoBtn.Name = "UndoBtn";
            UndoBtn.Size = new Size(61, 23);
            UndoBtn.TabIndex = 8;
            UndoBtn.Text = "Undo";
            UndoBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            UndoBtn.UseVisualStyleBackColor = true;
            UndoBtn.Click += UndoBtn_Click;
            // 
            // GraphBtn
            // 
            GraphBtn.Image = Properties.Resources.graph;
            GraphBtn.ImageAlign = ContentAlignment.MiddleLeft;
            GraphBtn.Location = new Point(589, 3);
            GraphBtn.Name = "GraphBtn";
            GraphBtn.Size = new Size(66, 23);
            GraphBtn.TabIndex = 6;
            GraphBtn.Text = "Graph";
            GraphBtn.TextAlign = ContentAlignment.MiddleRight;
            GraphBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
            GraphBtn.UseVisualStyleBackColor = true;
            GraphBtn.Click += GraphBtn_Click;
            // 
            // canvas
            // 
            canvas.BackColor = Color.White;
            canvas.Dock = DockStyle.Fill;
            canvas.Location = new Point(6, 6);
            canvas.Name = "canvas";
            canvas.Size = new Size(585, 395);
            canvas.TabIndex = 1;
            canvas.Tag = "DoubleBufferedPanel";
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
            LayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 397F));
            LayoutPanel.Size = new Size(194, 395);
            LayoutPanel.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(MainLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(699, 298);
            Name = "MainForm";
            Text = "Multigraph Editor";
            Load += MainForm_Load;
            MainLayout.ResumeLayout(false);
            ButtonPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel MainLayout;
        private DoubleBufferedPanel canvas;
        private FlowLayoutPanel ButtonPanel;
        private Button button1;
        private Button ViewBtn;
        private Button MoveBtn;
        private Button AddBtn;
        private Button ConnectBtn;
        private Button AlgorithmsBtn;
        private Button GraphBtn;
        private TableLayoutPanel LayoutPanel;
        private Button RemoveBtn;
        private Button UndoBtn;
    }
}
