namespace MultigraphEditor.Forms
{
    partial class EdgeForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DynamicFlowLayout = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            CancelBtn = new Button();
            OkBtn = new Button();
            BtnLayoutPanel = new TableLayoutPanel();
            DynamicFlowLayout.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // DynamicFlowLayout
            // 
            DynamicFlowLayout.AutoSize = true;
            DynamicFlowLayout.ColumnCount = 1;
            DynamicFlowLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            DynamicFlowLayout.Controls.Add(flowLayoutPanel1, 0, 1);
            DynamicFlowLayout.Controls.Add(BtnLayoutPanel, 0, 0);
            DynamicFlowLayout.Dock = DockStyle.Fill;
            DynamicFlowLayout.Location = new Point(0, 0);
            DynamicFlowLayout.Name = "DynamicFlowLayout";
            DynamicFlowLayout.RowCount = 2;
            DynamicFlowLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            DynamicFlowLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            DynamicFlowLayout.Size = new Size(800, 450);
            DynamicFlowLayout.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom;
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(CancelBtn);
            flowLayoutPanel1.Controls.Add(OkBtn);
            flowLayoutPanel1.Location = new Point(319, 413);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(162, 34);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // CancelBtn
            // 
            CancelBtn.Location = new Point(3, 3);
            CancelBtn.Name = "CancelBtn";
            CancelBtn.Size = new Size(75, 23);
            CancelBtn.TabIndex = 0;
            CancelBtn.Text = "Cancel";
            CancelBtn.UseVisualStyleBackColor = true;
            CancelBtn.Click += CancelBtn_Click;
            // 
            // OkBtn
            // 
            OkBtn.Location = new Point(84, 3);
            OkBtn.Name = "OkBtn";
            OkBtn.Size = new Size(75, 23);
            OkBtn.TabIndex = 1;
            OkBtn.Text = "Ok";
            OkBtn.UseVisualStyleBackColor = true;
            OkBtn.Click += OkBtn_Click;
            // 
            // BtnLayoutPanel
            // 
            BtnLayoutPanel.AutoSize = true;
            BtnLayoutPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            BtnLayoutPanel.ColumnCount = 2;
            BtnLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BtnLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            BtnLayoutPanel.Dock = DockStyle.Fill;
            BtnLayoutPanel.Location = new Point(3, 3);
            BtnLayoutPanel.Name = "BtnLayoutPanel";
            BtnLayoutPanel.RowCount = 1;
            BtnLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            BtnLayoutPanel.Size = new Size(794, 404);
            BtnLayoutPanel.TabIndex = 1;
            // 
            // EdgeForm
            // 
            AcceptButton = OkBtn;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(800, 450);
            Controls.Add(DynamicFlowLayout);
            Name = "EdgeForm";
            Text = "EdgeForm";
            Load += EdgeForm_Load;
            DynamicFlowLayout.ResumeLayout(false);
            DynamicFlowLayout.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TableLayoutPanel DynamicFlowLayout;
        private FlowLayoutPanel flowLayoutPanel1;
        private Button CancelBtn;
        private Button OkBtn;
        private TableLayoutPanel BtnLayoutPanel;
    }
}