using MultigraphEditor.src.graph;
using MultigraphEditor.Src.alghoritm;
using MultigraphEditor.Src.graph;
using MultigraphEditor.Src.layers;

namespace MultigraphEditor.Forms
{
    public partial class AlghoritmForm : Form
    {
        private string? selectedAlgorithm = null;
        private IMGraphLayer? selectedLayer = null;
        private Dictionary<string, Type> algorithmNameToTypeMap = new Dictionary<string, Type>();
        TableLayoutPanel orginized = new TableLayoutPanel();
        private ComboBox layerComboBox;
        private ComboBox algorithmComboBox;
        private ComboBox startNode;
        private ComboBox endNode;
        private INode? start = null;
        private INode? end = null;
        ListBox pathListBox = new ListBox();

        internal AlghoritmForm(List<IMGraphLayer> layerList, List<Type> algorithmList)
        {
            InitializeComponent();
            orginized.RowCount = 3;
            orginized.ColumnCount = 1;
            orginized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            orginized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            orginized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            orginized.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            orginized.Dock = DockStyle.Fill;
            orginized.AutoSize = true;

            AutoSize = true;
            layerComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "layerComboBox",
                Width = 200,
                Location = new Point(10, 10)
            };
            algorithmComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "algorithmComboBox",
                Width = 200,
                Location = new Point(10, 10)
            };
            startNode = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "startNode",
                Width = 100,
            };
            endNode = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "endNode",
                Width = 100,
            };
            TableLayoutPanel nodePanel = new TableLayoutPanel
            {
                RowCount = 1,
                ColumnCount = 2,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            nodePanel.Controls.Add(startNode, 0, 0);
            nodePanel.Controls.Add(endNode, 1, 0);
            orginized.Controls.Add(nodePanel, 0, 2);

            foreach (IMGraphLayer l in layerList)
            {
                layerComboBox.Items.Add(l);
            }
            layerComboBox.SelectedValueChanged += SetNodes;

            foreach (Type t in algorithmList)
            {
                IMGraphAlgorithm? algo = Activator.CreateInstance(t) as IMGraphAlgorithm;
                if (algo != null)
                {
                    string displayName = algo.Name ?? "";
                    algorithmComboBox.Items.Add(displayName);
                    algorithmNameToTypeMap[displayName] = t;
                }
            }

            selectedLayer = layerList[0];
            foreach (IMGraphEditorNode n in layerList[0].nodes)
            {
                startNode.Items.Add(n.Label);
                endNode.Items.Add(n.Label);
            }
            startNode.SelectedValueChanged += (sender, e) =>
            {
                start = selectedLayer.nodes.Find(n => n.Label == startNode.SelectedItem.ToString());
            };
            endNode.SelectedValueChanged += (sender, e) =>
            {
                end = selectedLayer.nodes.Find(n => n.Label == endNode.SelectedItem.ToString());
            };

            layerComboBox.DisplayMember = "Name";
            if (layerComboBox.Items.Count > 0)
            {
                layerComboBox.SelectedIndex = 0;
            }

            algorithmComboBox.DisplayMember = "Name";
            if (algorithmComboBox.Items.Count > 0)
            {
                algorithmComboBox.SelectedIndex = 0;
            }

            layerComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            algorithmComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            orginized.Controls.Add(layerComboBox, 0, 0);
            orginized.Controls.Add(algorithmComboBox, 0, 1);

            Controls.Add(orginized);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            layerComboBox.SelectedIndexChanged += SetNodes;
            startNode.SelectedIndexChanged += FindPath;
            endNode.SelectedIndexChanged += FindPath;

            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void FindPath(object? sender, EventArgs e)
        {
            if (start == null || end == null)
            {
                return;
            }
            ClearControlsExceptComboBox();

            if (layerComboBox.SelectedItem is IMGraphLayer layer && algorithmComboBox.SelectedItem is string algoName)
            {
                IMGraphAlgorithm? algo = null;
                if (algorithmComboBox.SelectedItem is string selectedDisplayName)
                {
                    if (algorithmNameToTypeMap.TryGetValue(selectedDisplayName, out Type selectedAlgorithmType))
                    {
                        algo = Activator.CreateInstance(selectedAlgorithmType) as IMGraphAlgorithm;
                    }
                }

                if (algo != null)
                {
                    List<INode> path = algo.FindPath(start, end, selectedLayer);
                    path.Reverse();
                    pathListBox = new ListBox
                    {
                        Width = 200,
                        Height = 100,
                        Location = new Point(10, 10)
                    };

                    pathListBox.Items.Clear();
                    if (path.Count() > 0)
                    {
                        foreach (INode node in path)
                        {
                            pathListBox.Items.Add(node.Label ?? "Unnamed Node");
                        }
                    }
                    else
                    {
                        pathListBox.Items.Add("No path found.");
                    }
                    orginized.RowCount = 4;
                    orginized.Controls.Add(pathListBox, 0, 3);
                }
            }
        }

        private void SetNodes(object? sender, EventArgs e)
        {
            if (layerComboBox.SelectedItem is IMGraphLayer layer)
            {
                selectedLayer = layer;
                startNode.Items.Clear();
                endNode.Items.Clear();
                foreach (IMGraphEditorNode n in layer.nodes)
                {
                    startNode.Items.Add(n.Label);
                    endNode.Items.Add(n.Label);
                }
            }
        }

        private void ClearControlsExceptComboBox()
        {
            if (pathListBox != null)
            {
                orginized.Controls.Remove(pathListBox);
            }
        }
    }
}
