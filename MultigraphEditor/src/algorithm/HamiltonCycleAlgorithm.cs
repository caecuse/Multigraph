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
    public partial class HamiltonCycleAlgorithm : AlghoritmForm
    {
        TableLayoutPanel organized = new TableLayoutPanel();
        private ComboBox layerComboBox;
        RichTextBox label = new RichTextBox();
        private IMGraphLayer? selectedLayer = null;

        public HamiltonCycleAlgorithm(List<IMGraphLayer> layerList)
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

            foreach (IMGraphLayer l in layerList)
            {
                layerComboBox.Items.Add(l);
            }

            Button runButton = new Button
            {
                Text = "Find path",
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };

            organized.Controls.Add(runButton, 0, 1);
            runButton.Click += RunAlgorithm;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void RunAlgorithm(object? sender, EventArgs e)
        {
            ClearControlsExceptComboBox();

            if (layerComboBox.SelectedItem is IMGraphLayer layer)
            {
                selectedLayer = layer;
                List<string> output = Output(selectedLayer);
                label = new RichTextBox
                {
                    Text = string.Join("\n", output),
                    ReadOnly = true,
                    Name = "outputLabel",
                    Location = new Point(10, 10),
                    Width = Text.Length * 10,
                   
                };
                organized.RowCount = 3;
                organized.Controls.Add(label, 0, 2);

            }
        }

        private void ClearControlsExceptComboBox()
        {
            if (label != null)
            {
                organized.Controls.Remove(label);
            }
        }

        public List<string> Output(IMGraphLayer targetLayer)
        {
            List<string> output = new List<string>();
            if (targetLayer.nodes.Count < 3)
            {
                output.Add("Graph has less than 3 nodes, so it can't have a Hamilton cycle");
                return output;
            }
            if (targetLayer.nodes.Count != targetLayer.edges.Count)
            {
                output.Add("Graph has more nodes than edges, so it can't have a Hamilton cycle");
                return output;
            }
            foreach (INode node in targetLayer.nodes)
            {
                if (node.Edges.Count < 2)
                {
                    output.Add("Node " + node.Label + " has less than 2 edges, so it can't be part of a Hamilton cycle");
                    return output;
                }
            }
            output.Add("Graph has a Hamilton cycle");
            return output;
        }
    }
}

