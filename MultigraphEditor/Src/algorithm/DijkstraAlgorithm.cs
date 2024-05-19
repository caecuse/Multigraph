using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultigraphEditor.src.algorithm
{
    public partial class DijkstraAlgorithm : AlghoritmForm
    {
        TableLayoutPanel organized = new TableLayoutPanel();
        private ComboBox layerComboBox;
        private ComboBox startNode;
        private ComboBox endNode;
        ListBox pathListBox = new ListBox();
        private IMGraphLayer? selectedLayer = null;
        private INode? start = null;
        private INode? end = null;

        public DijkstraAlgorithm(List<IMGraphLayer> layerList)
        {
            InitializeComponent();

            // Set up form
            Controls.Add(organized);
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            // Set up the organized TableLayoutPanel
            organized.RowCount = 3;
            organized.ColumnCount = 1;
            organized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            organized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            organized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            organized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            organized.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            organized.Dock = DockStyle.Fill;
            organized.AutoSize = true;

            // Set up the comboBoxes
            layerComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "layerComboBox",
                Width = 200,
                Location = new Point(10, 10)
            };
            layerComboBox.DisplayMember = "Name";
            layerComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            organized.Controls.Add(layerComboBox, 0, 0);

            if (layerComboBox.Items.Count > 0)
            {
                layerComboBox.SelectedIndex = 0;
            }

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
            startNode.SelectedValueChanged += (sender, e) =>
            {
                start = selectedLayer.nodes.Find(n => n.Label == startNode.SelectedItem.ToString());
            };
            endNode.SelectedValueChanged += (sender, e) =>
            {
                end = selectedLayer.nodes.Find(n => n.Label == endNode.SelectedItem.ToString());
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
            organized.Controls.Add(nodePanel, 0, 1);

            foreach (IMGraphLayer l in layerList)
            {
                layerComboBox.Items.Add(l);
            }
            layerComboBox.SelectedValueChanged += SetNodes;

            Button runButton = new Button
            {
                Text = "Find path",
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            organized.Controls.Add(runButton, 0, 2);
            layerComboBox.SelectedIndexChanged += SetNodes;
            runButton.Click += RunAlgorithm;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
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

        private void RunAlgorithm(object? sender, EventArgs e)
        {
            ClearControlsExceptComboBox();

            if (layerComboBox.SelectedItem is IMGraphLayer layer)
            {
                List<string> output = Output(start, end, selectedLayer);
                pathListBox = new ListBox
                {
                    Width = 200,
                    Height = 100,
                    Name = "pathListBox",
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
                organized.RowCount = 4;
                organized.Controls.Add(pathListBox, 0, 3);

            }
        }

        private void ClearControlsExceptComboBox()
        {
            if (pathListBox != null)
            {
                organized.Controls.Remove(pathListBox);
            }
        }

        public List<string> Output(INode start, INode target, IMGraphLayer targetLayer)
        {
            List<INode> path = FindPath(start, target, targetLayer);
            List<string> output = new List<string>();
            foreach (INode node in path)
            {
                if (node.Label != null)
                {
                    output.Add(node.Label);
                }
                else
                {
                    output.Add("Unnamed node");
                }
            }
            if (path.Count == 0)
            {
                List<string> error = new List<string>();
                error.Add("No path found");
                return error;
            }
            output.Reverse();
            return output;
        }

        public List<INode> FindPath(INode start, INode target, IMGraphLayer targetLayer)
        {
            PriorityQueue<INode, double> openSet = new PriorityQueue<INode, double>();
            Dictionary<INode, INode> cameFrom = new Dictionary<INode, INode>();
            Dictionary<INode, double> gScore = new Dictionary<INode, double>();
            foreach (IMGraphEditorNode node in targetLayer.nodes)
            {
                gScore[node] = double.PositiveInfinity;
            }
            gScore[start] = 0;

            openSet.Enqueue(start, 0);

            while (openSet.Count > 0)
            {
                INode current = openSet.Dequeue();

                if (current.Equals(target))
                {
                    List<INode> res = ReconstructPath(cameFrom, target);
                    res.Insert(0, start);
                    res.Reverse();
                    return res;
                }

                foreach (IEdge edge in current.Edges)
                {
                    if (targetLayer.edges.Contains(edge) && targetLayer.nodes.Contains(edge.Target))
                    {
                        INode neighbor = edge.Target;
                        if (edge.Weight < 0)
                        {
                            MessageBox.Show("Dijkstra's algorithm does not work with edges that have negative weight", "Bad edge weight", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return new List<INode>();
                        }
                        double tentativeGScore = gScore[current] + edge.Weight;

                        if (tentativeGScore < gScore[neighbor])
                        {
                            cameFrom[neighbor] = current;
                            gScore[neighbor] = tentativeGScore;

                            if (!openSet.UnorderedItems.Any(item => item.Element.Equals(neighbor)))
                            {
                                openSet.Enqueue(neighbor, gScore[neighbor]);
                            }
                        }
                    }
                }
            }

            return new List<INode>();
        }

        private List<INode> ReconstructPath(Dictionary<INode, INode> cameFrom, INode current)
        {
            List<INode> path = new List<INode>();
            if (!cameFrom.ContainsKey(current))
            {
                return path;
            }

            while (current != null && cameFrom.ContainsKey(current))
            {
                path.Insert(0, current);
                current = cameFrom[current];
            }
            return path;
        }
    }
}
