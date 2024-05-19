using MultigraphEditor.src.algorithm;
using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;

namespace MultigraphEditor.Forms
{
    public partial class AlgorithmForm : Form
    {
        private AlghoritmForm? selectedAlgorithm = null;
        private List<IMGraphLayer> layerList = new List<IMGraphLayer>();
        private Dictionary<string, Type> algorithmNameToTypeMap = new Dictionary<string, Type>();
        TableLayoutPanel organized = new TableLayoutPanel();
        private ComboBox algorithmComboBox;

        internal AlgorithmForm(List<IMGraphLayer> layerList, List<Type> algorithmList)
        {
            InitializeComponent();

            this.layerList = layerList;

            organized.RowCount = 3;
            organized.ColumnCount = 1;
            organized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            organized.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            organized.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            organized.Dock = DockStyle.Fill;
            organized.AutoSize = true;

            AutoSize = true;

            algorithmComboBox = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Name = "algorithmComboBox",
                Width = 200,
                Location = new Point(10, 10)
            };

            foreach (Type t in algorithmList)
            {
                string? algoName = t.Name;
                if (algoName != null)
                {
                    string displayName = algoName ?? "";
                    algorithmComboBox.Items.Add(displayName);
                    algorithmNameToTypeMap[displayName] = t;
                }
            }

            algorithmComboBox.DisplayMember = "Name";
            if (algorithmComboBox.Items.Count > 0)
            {
                algorithmComboBox.SelectedIndex = 0;
            }

            algorithmComboBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            
            organized.Controls.Add(algorithmComboBox, 0, 0);

            Button runButton = new Button
            {
                Text = "Run selected alghoritm",
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink
            };
            organized.Controls.Add(runButton, 0, 1);

            Controls.Add(organized);

            runButton.Click += RunAlgorithm;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            MaximizeBox = false;
            MinimizeBox = false;
        }

        private void RunAlgorithm(object? sender, EventArgs e)
        {
            if (algorithmComboBox.SelectedItem is string selectedDisplayName)
            {
                if (algorithmNameToTypeMap.TryGetValue(selectedDisplayName, out Type selectedAlgorithmType))
                {
                    selectedAlgorithm = Activator.CreateInstance(selectedAlgorithmType, layerList) as AlghoritmForm;
                }
                else
                {
                    selectedAlgorithm = null;
                }
            }

            if (selectedAlgorithm != null)
            {
                selectedAlgorithm.ShowDialog();
            }
        }
    }
}
