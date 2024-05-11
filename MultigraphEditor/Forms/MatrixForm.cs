using MultigraphEditor.src.layers;
using System.Data;
using System.Text;

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
                        Text = "Adjacency Matrix";
                        GenerateAdjacencyMatrix(layer);
                        break;
                    case "inc":
                        Text = "Incidence Matrix";
                        GenerateIncidenceMatrix(layer);
                        break;
                    case "dis":
                        Text = "Distance Matrix";
                        GenerateDistanceMatrix(layer);
                        break;
                }
            }
        }

        private void GenerateAdjacencyMatrix(IMGraphLayer layer)
        {
            ClearControlsExceptComboBox();

            List<IMGraphEditorNode> nodes = layer.nodes;
            List<IMGraphEditorEdge> edges = layer.edges;

            int[,] adjacencyMatrix = new int[nodes.Count, nodes.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    adjacencyMatrix[i, j] = 0;
                }
            }

            foreach (IMGraphEditorEdge edge in edges)
            {
                int startIndex = nodes.IndexOf((IMGraphEditorNode)edge.Source);
                int endIndex = nodes.IndexOf((IMGraphEditorNode)edge.Target);
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
                    matrixDisplay.Text += adjacencyMatrix[i, j] + ", ";
                }
                matrixDisplay.Text += Environment.NewLine;
            }

            orginized.RowCount = 2;
            orginized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            orginized.Controls.Add(matrixDisplay, 1, 1);
        }

        public void GenerateIncidenceMatrix(IMGraphLayer layer)
        {
            ClearControlsExceptComboBox();

            List<IMGraphEditorNode> nodes = layer.nodes;
            List<IMGraphEditorEdge> edges = layer.edges;

            int[,] incidenceMatrix = new int[nodes.Count, edges.Count];

            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < edges.Count; j++)
                {
                    incidenceMatrix[i, j] = 0;
                }
            }

            for (int j = 0; j < edges.Count; j++)
            {
                IMGraphEditorEdge edge = edges[j];
                int nodeUIndex = nodes.IndexOf((IMGraphEditorNode)edge.Source);
                int nodeVIndex = nodes.IndexOf((IMGraphEditorNode)edge.Target);
                int weight = edge.Weight;

                if (edge.Bidirectional)
                {
                    incidenceMatrix[nodeUIndex, j] = weight;
                    incidenceMatrix[nodeVIndex, j] = weight;
                }
                else
                {
                    incidenceMatrix[nodeUIndex, j] = weight;
                    incidenceMatrix[nodeVIndex, j] = -weight;
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

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < edges.Count; j++)
                {
                    sb.Append(" " + incidenceMatrix[i, j] + ", ");
                }
                sb.AppendLine();
            }
            matrixDisplay.Text = sb.ToString();

            orginized.RowCount = 2;
            orginized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            orginized.Controls.Add(matrixDisplay, 1, 1);
        }

        public void GenerateDistanceMatrix(IMGraphLayer layer)
        {
            ClearControlsExceptComboBox();

            List<IMGraphEditorNode> nodes = layer.nodes;
            List<IMGraphEditorEdge> edges = layer.edges;

            double[,] distanceMatrix = new double[nodes.Count, nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    if (i == j)
                        distanceMatrix[i, j] = 0;
                    else
                        distanceMatrix[i, j] = double.PositiveInfinity;
                }
            }

            foreach (IMGraphEditorEdge edge in edges)
            {
                int nodeUIndex = nodes.IndexOf((IMGraphEditorNode)edge.Source);
                int nodeVIndex = nodes.IndexOf((IMGraphEditorNode)edge.Target);
                double weight = edge.Weight;

                distanceMatrix[nodeUIndex, nodeVIndex] = weight;
                if (edge.Bidirectional)
                {
                    distanceMatrix[nodeVIndex, nodeUIndex] = weight;
                }
            }

            for (int k = 0; k < nodes.Count; k++)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    for (int j = 0; j < nodes.Count; j++)
                    {
                        if (distanceMatrix[i, k] + distanceMatrix[k, j] < distanceMatrix[i, j])
                        {
                            distanceMatrix[i, j] = distanceMatrix[i, k] + distanceMatrix[k, j];
                        }
                    }
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

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < nodes.Count; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    string distance = distanceMatrix[i, j] == double.PositiveInfinity ? "∞" : distanceMatrix[i, j].ToString();
                    sb.Append(distance.PadLeft(4) + ", ");
                }
                sb.AppendLine();
            }
            matrixDisplay.Text = sb.ToString();

            orginized.RowCount = 2;
            orginized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            orginized.Controls.Add(matrixDisplay, 0, 1);
        }

        private void ClearControlsExceptComboBox()
        {
            List<Control> controlsToRemove = orginized.Controls.Cast<Control>()
                                         .Where(ctrl => ctrl != layerComboBox)
                                         .ToList();

            foreach (Control? ctrl in controlsToRemove)
            {
                orginized.Controls.Remove(ctrl);
                ctrl.Dispose();
            }
        }
    }
}
