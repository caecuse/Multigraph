using MultigraphEditor.Src.layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultigraphEditor.Forms
{
    public partial class MatrixForm : Form
    {
        private string matrixType;
        private IMGraphLayer? selectedLayer = null;
        private ComboBox layerComboBox;
        TableLayoutPanel orginized = new TableLayoutPanel();

        public MatrixForm(List<IMGraphLayer> layerList, string type)
        {
            InitializeComponent();
            orginized.RowCount = 1;
            orginized.ColumnCount = 1;
            orginized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            orginized.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            orginized.Dock = DockStyle.Fill;
            orginized.AutoSize = true;

            AutoSize = true;
            matrixType = type;
            layerComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "layerComboBox",
                Width = 200,
                Location = new Point(10, 10)
            };

            foreach (IMGraphLayer l in layerList)
            {
                layerComboBox.Items.Add(l);
            }

            layerComboBox.DisplayMember = "Name";
            if (layerComboBox.Items.Count > 0)
            {
                layerComboBox.SelectedIndex = 0;
            }

            layerComboBox.SelectedIndexChanged += CreateMatrix;
            layerComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            orginized.Controls.Add(layerComboBox);
            Controls.Add(orginized);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            CreateMatrix(layerComboBox, EventArgs.Empty);

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void CreateMatrix(object? sender, EventArgs e)
        {
            if (layerComboBox.SelectedItem is IMGraphLayer layer)
            {
                selectedLayer = layer;
                switch (matrixType)
                {
                    case "adj":
                        GenerateAdjacencyMatrix(layer);
                        break;
                    case "inc":
                        GenerateIncidenceMatrix(layer);
                        break;
                    case "dist":
                        GenerateDistanceMatrix(layer);
                        break;
                }
            }
        }

        private void GenerateAdjacencyMatrix(IMGraphLayer layer)
        {
            ClearControlsExceptComboBox();

            var nodes = layer.nodes;
            var edges = layer.edges;

            int[,] adjacencyMatrix = new int[nodes.Count, nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    adjacencyMatrix[i, j] = 0;
                }
            }

            foreach (var edge in edges)
            {
                int startIndex = nodes.IndexOf((Src.graph.IMGraphEditorNode)edge.Source);
                int endIndex = nodes.IndexOf((Src.graph.IMGraphEditorNode)edge.Target);
                adjacencyMatrix[startIndex, endIndex] = 1;
                if (edge.Bidirectional)
                {
                    adjacencyMatrix[endIndex, startIndex] = 1;
                }
            }

            TextBox matrixDisplay = new TextBox
            {
                Multiline = true,
                Width = 400,
                Height = 300,
                ScrollBars = ScrollBars.Both,
                ReadOnly = true
            };

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    matrixDisplay.Text += adjacencyMatrix[i, j] + " ";
                }
                matrixDisplay.Text += Environment.NewLine;
            }

            orginized.RowCount = 2;
            orginized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            orginized.Controls.Add(matrixDisplay, 1, 1);
        }

        public void GenerateIncidenceMatrix(IMGraphLayer l)
        {
            ClearControlsExceptComboBox();
        }

        public void GenerateDistanceMatrix(IMGraphLayer l)
        {
            ClearControlsExceptComboBox();
        }

        private void ClearControlsExceptComboBox()
        {
            var controlsToRemove = orginized.Controls.Cast<Control>()
                                         .Where(ctrl => ctrl != layerComboBox)
                                         .ToList();

            foreach (var ctrl in controlsToRemove)
            {
                orginized.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }
    }
}
