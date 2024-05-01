using MultigraphEditor.src.graph;
using MultigraphEditor.Src.algorithm;
using MultigraphEditor.Src.graph;
using MultigraphEditor.Src.layers;

namespace MultigraphEditor.Forms
{
    public partial class AlgorithmForm : Form
    {
        private IMGraphAlgorithm? selectedAlgorithm = null;
        private IMGraphLayer? selectedLayer = null;
        private Dictionary<string, Type> algorithmNameToTypeMap = new Dictionary<string, Type>();
        TableLayoutPanel organized = new TableLayoutPanel();
        private ComboBox layerComboBox;
        private ComboBox algorithmComboBox;
        private ComboBox startNode;
        private ComboBox endNode;
        private INode? start = null;
        private INode? end = null;
        ListBox pathListBox = new ListBox();

        internal AlgorithmForm(List<IMGraphLayer> layerList, List<Type> algorithmList)
        {
            InitializeComponent();
            organized.RowCount = 3;
            organized.ColumnCount = 1;
            organized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            organized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            organized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            organized.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            organized.Dock = DockStyle.Fill;
            organized.AutoSize = true;

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
            organized.Controls.Add(nodePanel, 0, 2);

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
                    algorithmComboBox.SelectedIndexChanged += SetNodes;
                    algorithmNameToTypeMap[displayName] = t;
                }
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
            organized.Controls.Add(algorithmComboBox, 0, 0);
            organized.Controls.Add(layerComboBox, 0, 1);

            Button runButton = new Button
            {
                Text = "Run selected alghoritm",
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
            };
            organized.RowCount = 4;
            organized.Controls.Add(runButton, 0, 3);

            Controls.Add(organized);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;

            layerComboBox.SelectedIndexChanged += SetNodes;
            runButton.Click += RunAlgorithm;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void RunAlgorithm(object? sender, EventArgs e)
        {
            if ((selectedAlgorithm.requiresEndNode == true && end == null) || (selectedAlgorithm.requiresStartNode == true && start == null))
            {
                return;
            }
            ClearControlsExceptComboBox();

            if (layerComboBox.SelectedItem is IMGraphLayer layer && algorithmComboBox.SelectedItem is string algoName)
            {

                if (selectedAlgorithm != null)
                {
                    List<String> output = selectedAlgorithm.Output(start, end, selectedLayer);
                    pathListBox = new ListBox
                    {
                        Width = 200,
                        Height = 100,
                        Location = new Point(10, 10),
                        HorizontalScrollbar = true,
                    };

                    pathListBox.Items.Clear();
                    if (output.Count() > 0)
                    {
                        foreach (string word in output)
                        {
                            pathListBox.Items.Add(word ?? "???");
                        }
                    }
                    else
                    {
                        pathListBox.Items.Add("No output.");
                    }
                    organized.RowCount = 5;
                    organized.Controls.Add(pathListBox, 0, 3);
                }
            }
        }

        private void SetNodes(object? sender, EventArgs e)
        {
            if (algorithmComboBox.SelectedItem is string selectedDisplayName)
            {
                if (algorithmNameToTypeMap.TryGetValue(selectedDisplayName, out Type selectedAlgorithmType))
                {
                    selectedAlgorithm = Activator.CreateInstance(selectedAlgorithmType) as IMGraphAlgorithm;
                }
                else
                {
                    selectedAlgorithm = null;
                }
            }
            if (selectedAlgorithm != null)
            {
                if (selectedAlgorithm.requiresStartNode)
                {
                    startNode.Enabled = true;
                }
                else
                {
                    startNode.Enabled = false;
                }
                if (selectedAlgorithm.requiresEndNode)
                {
                    endNode.Enabled = true;
                }
                else
                {
                    endNode.Enabled = false;
                }
            }

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
                organized.Controls.Remove(pathListBox);
            }
        }
    }
}
