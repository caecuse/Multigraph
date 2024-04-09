using MultigraphEditor.Forms;
using MultigraphEditor.src.graph;
using MultigraphEditor.Src.design;
using MultigraphEditor.Src.graph;
using MultigraphEditor.Src.layers;

namespace MultigraphEditor
{
    public partial class MainForm : Form
    {
        private ApplicationMode amode = ApplicationMode.None;
        public List<IMGraphEditorNode> nodeList = new List<IMGraphEditorNode>();
        public List<IMGraphEditorEdge> edgeList = new List<IMGraphEditorEdge>();
        public List<IMGraphLayer> Layers = new List<IMGraphLayer>();
        Type nodeType;
        Type edgeType;
        Type layerType;
        List<Type> algoList;

        bool isPanning = false;
        private Point lastMouseLocation = Point.Empty;
        private IMGraphEditorNode? selectedNode = null;
        private IMGraphEditorNode? selectedNodeForConnection = null;
        private List<IMGraphEditorEdge> selectedEdges = new List<IMGraphEditorEdge>();
        private List<LayoutPreviewControl> previewControls = new List<LayoutPreviewControl>();

        private Stack<ApplicationState> stateStack = new Stack<ApplicationState>();

        private enum ApplicationMode
        {
            None,
            AddVertex,
            Default,
            Connect,
            View,
            Delete,
        }
        public MainForm(Type nodet, Type edget, Type layert, List<Type> alist)
        {
            InitializeComponent();
            nodeType = nodet;
            edgeType = edget;
            layerType = layert;
            algoList = alist;

            canvas.MouseDown += HandleMouseDown;
            canvas.MouseMove += HandleMouseMove;
            canvas.MouseUp += HandleMouseUp;
            LayoutPanel.Scroll += LayoutPanel_Scroll;
            LayoutPanel.MouseWheel += LayoutPanel_Scroll;
        }



        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            stateStack.Push(new ApplicationState(edgeList, nodeList, Layers));
            if (e.Button == MouseButtons.Left)
            {
                LeftMouseDown(sender, e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                RightMouseDown(sender, e);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeLayers();
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(AddBtn, "Add vertex");
            toolTip.SetToolTip(ViewBtn, "Move canvas around");
            toolTip.SetToolTip(MoveBtn, "Move vertices");
            toolTip.SetToolTip(ConnectBtn, "Connect vertices");
            toolTip.SetToolTip(GraphBtn, "Graph options");
            toolTip.SetToolTip(AlgorithmsBtn, "Algorithms");
            toolTip.SetToolTip(RemoveBtn, "Remove vertices or edges");
            toolTip.SetToolTip(UndoBtn, "Undo last action");
        }

        private void InitializeLayers()
        {
            if (Layers.Count == 0)
            {
                IMGraphLayer layer = (IMGraphLayer)Activator.CreateInstance(layerType);
                layer.nodes = new List<IMGraphEditorNode>();
                layer.edges = new List<IMGraphEditorEdge>();
                Layers.Add(layer);
            }
            LayoutPanel.Controls.Clear();
            LayoutPanel.RowStyles.Clear();
            LayoutPanel.RowCount = 0;
            LayoutPanel.ColumnStyles.Clear();
            LayoutPanel.ColumnCount = 1;
            LayoutPanel.ColumnStyles.Add(new ColumnStyle() { Width = 100, SizeType = SizeType.Percent });

            foreach (IMGraphLayer l in Layers)
            {
                Bitmap canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
                canvas.DrawToBitmap(canvasBitmap, new Rectangle(0, 0, canvas.Width, canvas.Height));
                LayoutPreviewControl prev = new LayoutPreviewControl(l, canvasBitmap);
                prev.CanvasInvalidated += (sender, e) =>
                {
                    InitializeLayers();
                    canvas.Invalidate();
                };
                prev.LayerDeleted += (sender, e) =>
                {
                    LayerDeletedHandler(sender, e);
                };
                LayoutPanel.RowCount++;
                LayoutPanel.RowStyles.Add(new RowStyle() { Height = 90, SizeType = SizeType.Absolute });
                LayoutPanel.Controls.Add(prev, 0, LayoutPanel.RowCount - 1);
                previewControls.Add(prev);
                prev.MouseDown += HandleMouseDown;
            }

            Button newLayer = new Button();
            newLayer.Text = "New Layer";
            newLayer.AutoSize = true;
            newLayer.Anchor = AnchorStyles.Left;
            newLayer.Anchor = AnchorStyles.Right;
            newLayer.Anchor = AnchorStyles.Top;
            newLayer.Click += AddLayer;
            LayoutPanel.RowCount++;
            LayoutPanel.RowStyles.Add(new RowStyle() { Height = 90, SizeType = SizeType.Absolute });
            LayoutPanel.Controls.Add(newLayer, 0, LayoutPanel.RowCount - 1);
            UpdatePreviewPanels();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            selectedEdges.Clear();
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.AddVertex;
            UpdateLastClickedButton(sender);
        }

        private void UndoBtn_Click(object sender, EventArgs e)
        {
            selectedEdges.Clear();
            selectedNode = null;
            selectedNodeForConnection = null;
            if (stateStack.Count > 0)
            {
                ApplicationState state = stateStack.Pop();
                state.LoadState(out nodeList, out edgeList, out Layers);
                InitializeLayers();
                canvas.Invalidate();
            }
            UpdatePreviewPanels();
            UpdateLastClickedButton(sender);
        }

        private void ViewBtn_Click(object sender, EventArgs e)
        {
            selectedEdges.Clear();
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.View;
            UpdateLastClickedButton(sender);
        }

        private void MoveBtn_Click(object sender, EventArgs e)
        {
            selectedEdges.Clear();
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.Default;
            UpdateLastClickedButton(sender);
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            selectedEdges.Clear();
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.Connect;
            UpdateLastClickedButton(sender);
        }

        private void GraphBtn_Click(object sender, EventArgs e)
        {
            UpdateLastClickedButton(sender);
            selectedEdges.Clear();
            selectedNode = null;
            selectedNodeForConnection = null;

            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem save = new ToolStripMenuItem("Save");
            ToolStripMenuItem import = new ToolStripMenuItem("Open");

            ToolStripMenuItem export = new ToolStripMenuItem("Export");
            ToolStripMenuItem adjacency = new ToolStripMenuItem("Adjacency matrix");
            ToolStripMenuItem incidence = new ToolStripMenuItem("Incidence matrix");
            ToolStripMenuItem distance = new ToolStripMenuItem("Distance matrix");

            contextMenuStrip.Items.Add(save);
            contextMenuStrip.Items.Add(export);
            contextMenuStrip.Items.Add(import);
            contextMenuStrip.Items.Add(adjacency);
            contextMenuStrip.Items.Add(incidence);
            contextMenuStrip.Items.Add(distance);

            adjacency.Click += (sender, e) => CreateMatrixForm(Layers, "adj");
            incidence.Click += (sender, e) => CreateMatrixForm(Layers, "inc");
            distance.Click += (sender, e) => CreateMatrixForm(Layers, "dis");

            save.Click += (sender, e) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Multigraph files (*.mg)|*.mg";
                saveFileDialog.Title = "Save a Multigraph File";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    ApplicationState state = new ApplicationState(edgeList, nodeList, Layers);
                    ApplicationState.SerializeData(state, saveFileDialog.FileName);
                }
            };

            export.Click += (sender, e) =>
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "PNG files (*.png)|*.png";
                saveFileDialog.Title = "Export to PNG";
                saveFileDialog.ShowDialog();

                if (saveFileDialog.FileName != "")
                {
                    Bitmap bitmap = new Bitmap(canvas.Width, canvas.Height);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        foreach (IMGraphLayer layer in Layers)
                        {
                            if (layer.Active)
                            {
                                foreach (IMGraphEditorNode node in layer.nodes)
                                {
                                    node.Draw(g, layer);
                                }
                                foreach (IMGraphEditorEdge edge in layer.edges)
                                {
                                    edge.Draw(g, layer);
                                }
                            }
                        }
                    }
                    bitmap.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                }
            };

            import.Click += (sender, e) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Multigraph files (*.mg)|*.mg";
                openFileDialog.Title = "Open a Multigraph File";
                openFileDialog.ShowDialog();

                if (openFileDialog.FileName != "")
                {
                    ApplicationState state = ApplicationState.DeserializeData(openFileDialog.FileName);
                    state.LoadState(out nodeList, out edgeList, out Layers);
                    InitializeLayers();
                    canvas.Invalidate();
                }
            };

            contextMenuStrip.Show(GraphBtn, new System.Drawing.Point(0, GraphBtn.Height));
        }

        private void AlgorithmsBtn_Click(object sender, EventArgs e)
        {
            selectedEdges.Clear();
            selectedNode = null;
            selectedNodeForConnection = null;
            UpdateLastClickedButton(sender);
            AlghoritmForm alghorimtForm = new AlghoritmForm(Layers, algoList);
            alghorimtForm.Show();
        }

        private void RemoveBtn_Click(object sender, EventArgs e)
        {
            selectedEdges.Clear();
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.Delete;
            UpdateLastClickedButton(sender);
        }

        private void CreateMatrixForm(List<IMGraphLayer> layerList, string type)
        {
            MatrixForm matrixForm = new MatrixForm(layerList, type);
            matrixForm.Show();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            foreach (IMGraphLayer layer in Layers)
            {
                if (layer.Active)
                {
                    foreach (IMGraphEditorNode node in layer.nodes)
                    {
                        node.Draw(g, layer);
                    }
                    foreach (IMGraphEditorEdge edge in layer.edges)
                    {
                        if (layer.nodes.Contains(edge.SourceDrawable) && layer.nodes.Contains(edge.TargetDrawable))
                        {
                            edge.Draw(g, layer);
                        }
                    }
                }
            }
        }

        private void LeftMouseDown(object sender, MouseEventArgs e)
        {
            if (amode == ApplicationMode.AddVertex)
            {
                IMGraphEditorNode node = (IMGraphEditorNode)Activator.CreateInstance(nodeType);

                using (EditForm editform = new EditForm(node))
                {
                    editform.OnOk += (s, n) =>
                    {
                        editform.Close();
                        node.X = e.X;
                        node.Y = e.Y;
                        nodeList.Add(node);

                        foreach (IMGraphLayer layer in Layers)
                        {
                            if (layer.Active)
                            {
                                layer.nodes.Add(node);
                            }
                        }

                        canvas.Invalidate();
                    };
                    editform.ShowDialog();
                }
            }

            if (amode == ApplicationMode.View)
            {
                lastMouseLocation = e.Location;
                isPanning = true;
            }

            if (amode == ApplicationMode.Default)
            {
                foreach (IMGraphEditorNode node in nodeList)
                {
                    foreach (IMGraphLayer layer in Layers)
                    {
                        if (layer.Active)
                        {
                            if (node.IsInside(e.X, e.Y))
                            {
                                selectedNode = node;
                                lastMouseLocation = e.Location;
                                break;
                            }
                        }
                    }
                }

                foreach (IMGraphEditorEdge edge in edgeList)
                {
                    foreach (IMGraphLayer layer in Layers)
                    {
                        if (layer.Active)
                        {
                            if (edge.IsInsideControlPoint(e.X, e.Y))
                            {
                                selectedEdges.Add(edge);
                                lastMouseLocation = e.Location;
                                break;
                            }
                        }
                    }
                }
            }

            if (amode == ApplicationMode.Delete)
            {
                bool itemDeleted = false;

                foreach (IMGraphEditorNode node in nodeList.ToList())
                {
                    if (node.IsInside(e.X, e.Y))
                    {
                        var connectedEdges = edgeList.Where(edge => edge.SourceDrawable == node || edge.TargetDrawable == node).ToList();

                        foreach (IMGraphLayer layer in Layers)
                        {
                            if (layer.Active)
                            {
                                layer.nodes.Remove(node);
                                foreach (var edge in connectedEdges)
                                {
                                    layer.edges.Remove(edge);
                                }
                            }
                        }

                        nodeList.Remove(node);
                        edgeList = edgeList.Except(connectedEdges).ToList();

                        itemDeleted = true;
                        canvas.Invalidate();
                        break;
                    }
                }

                if (!itemDeleted)
                {
                    foreach (IMGraphEditorEdge edge in edgeList.ToList())
                    {
                        if (edge.IsInside(e.X, e.Y))
                        {
                            foreach (IMGraphLayer layer in Layers)
                            {
                                if (layer.Active)
                                {
                                    layer.edges.Remove(edge);
                                }
                            }

                            edgeList.Remove(edge);

                            canvas.Invalidate();
                            break;
                        }
                    }
                }
            }

            if (amode == ApplicationMode.Connect)
            {
                foreach (IMGraphEditorNode node in nodeList)
                {
                    if (node.IsInside(e.X, e.Y) && Layers.Any(layer => layer.Active && layer.nodes.Contains(node)))
                    {
                        if (selectedNodeForConnection == null)
                        {
                            selectedNodeForConnection = node;
                            return;
                        }
                        else
                        {
                            var commonActiveLayers = Layers.Where(layer => layer.Active && layer.nodes.Contains(node) && layer.nodes.Contains(selectedNodeForConnection)).ToList();

                            if (commonActiveLayers.Any())
                            {
                                IMGraphEditorEdge edge = (IMGraphEditorEdge)Activator.CreateInstance(edgeType);

                                using (EditForm editform = new EditForm(edge))
                                {
                                    editform.OnOk += (s, ea) =>
                                    {
                                        edge.PopulateNode(selectedNodeForConnection, node, edge.Bidirectional, edge.Weight);
                                        edge.PopulateDrawing(selectedNodeForConnection, node);
                                        edge.SourceDrawable = selectedNodeForConnection;
                                        edge.TargetDrawable = node;

                                        foreach (IMGraphLayer layer in commonActiveLayers)
                                        {
                                            layer.edges.Add(edge);
                                        }
                                        edgeList.Add(edge);

                                        selectedNodeForConnection.Edges.Add(edge);
                                        node.Edges.Add(edge);

                                        canvas.Invalidate();
                                    };
                                    editform.ShowDialog();
                                }

                                selectedNodeForConnection = null;
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Nodes are not in the same layer");
                                selectedNodeForConnection = null;
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void HandleMouseMove(object sender, MouseEventArgs e)
        {
            if (amode == ApplicationMode.Default)
            {
                if (selectedNode != null)
                {
                    float dx = e.X - lastMouseLocation.X;
                    float dy = e.Y - lastMouseLocation.Y;
                    selectedNode.X += dx;
                    selectedNode.Y += dy;
                    lastMouseLocation = e.Location;
                    canvas.Invalidate();
                }

                if (selectedEdges != null && selectedEdges.Count > 0)
                {
                    float dx = e.X - lastMouseLocation.X;
                    float dy = e.Y - lastMouseLocation.Y;

                    HashSet<IMGraphEditorNode> movedNodes = new HashSet<IMGraphEditorNode>();

                    foreach (var edge in selectedEdges)
                    {
                        foreach (IMGraphEditorNode node in nodeList)
                        {
                            if (edge.SourceDrawable == node || edge.TargetDrawable == node)
                            {
                                if (!movedNodes.Contains(node))
                                {
                                    node.X += dx;
                                    node.Y += dy;
                                    movedNodes.Add(node);
                                }
                            }
                        }
                    }

                    lastMouseLocation = e.Location;
                    canvas.Invalidate();
                }
            }

            if (amode == ApplicationMode.View)
            {
                if (isPanning)
                {
                    int dx = e.X - lastMouseLocation.X;
                    int dy = e.Y - lastMouseLocation.Y;

                    foreach (IMGraphEditorNode node in nodeList)
                    {
                        node.X += dx;
                        node.Y += dy;
                    }

                    lastMouseLocation = e.Location;
                    canvas.Invalidate();
                }
            }
        }

        private void HandleMouseUp(object sender, MouseEventArgs e)
        {
            selectedNode = null;
            selectedEdges.Clear();
            if (amode == ApplicationMode.View)
            {
                isPanning = false;
            }
            UpdatePreviewPanels();
        }

        private void RightMouseDown(object sender, MouseEventArgs e)
        {
            bool formShown = false;

            foreach (IMGraphEditorEdge edge in edgeList)
            {
                if (formShown) break;

                foreach (IMGraphLayer layer in Layers)
                {
                    if (layer.Active && edge.IsInside(e.X, e.Y))
                    {
                        using (EditForm editform = new EditForm(edge))
                        {
                            editform.OnOk += (s, ea) =>
                            {
                                editform.Close();
                                canvas.Invalidate();
                            };
                            editform.ShowDialog();
                            formShown = true;
                            break;
                        }
                    }
                }
            }

            if (formShown) return;

            foreach (IMGraphEditorNode node in nodeList)
            {
                if (formShown) break;

                foreach (IMGraphLayer layer in Layers)
                {
                    if (layer.Active && node.IsInside(e.X, e.Y))
                    {
                        using (EditForm editform = new EditForm(node))
                        {
                            editform.OnOk += (s, ea) =>
                            {
                                editform.Close();
                                canvas.Invalidate();
                            };
                            editform.ShowDialog();
                            formShown = true;
                            break;
                        }
                    }
                }
            }
        }

        private void AddLayer(object? sender, EventArgs e)
        {
            stateStack.Push(new ApplicationState(edgeList, nodeList, Layers));

            IMGraphLayer layer = (IMGraphLayer)Activator.CreateInstance(layerType);
            layer.nodes = new List<IMGraphEditorNode>();
            layer.edges = new List<IMGraphEditorEdge>();
            Layers.Add(layer);

            InitializeLayers();
        }

        private Control? GetControlByTag(string tag)
        {
            foreach (Control control in Controls)
            {
                if (control.Tag != null && control.Tag.ToString() == tag)
                {
                    return control;
                }
            }
            return null;
        }

        private void LayoutPanel_Scroll(object sender, ScrollEventArgs e)
        {
            UpdatePreviewPanels();
        }

        private void LayoutPanel_Scroll(object? sender, MouseEventArgs e)
        {
            UpdatePreviewPanels();
        }

        private void UpdatePreviewPanels()
        {
            foreach (IMGraphLayer l in Layers)
            {
                foreach (Control c in LayoutPanel.Controls)
                {
                    if (c is LayoutPreviewControl)
                    {
                        if (c.Tag.ToString() == l.Identifier.ToString())
                        {
                            LayoutPreviewControl lp = (LayoutPreviewControl)c;
                            Bitmap bitmap = new Bitmap(canvas.Width, canvas.Height);
                            using (Graphics g = Graphics.FromImage(bitmap))
                            {
                                foreach (IMGraphEditorNode node in l.nodes)
                                {
                                    node.Draw(g, l);
                                }
                                foreach (IEdgeDrawable edge in l.edges)
                                {
                                    edge.Draw(g, l);
                                }
                            }
                            lp.PaintPreviewPanel(bitmap);
                        }
                    }
                }
            }
        }

        private void LayerDeletedHandler(object sender, IMGraphLayer e)
        {
            if (Layers.Count == 1)
            {
                MessageBox.Show("There must be at least one layer");
                return;
            }
            Layers.Remove(Layers[Layers.IndexOf(e)]);
            InitializeLayers();
        }

        private void UpdateLastClickedButton(object sender)
        {
            foreach (Control b in ButtonPanel.Controls)
            {
                if (b is Button)
                {
                    Button button = (Button)b;
                    if (button == sender)
                    {
                        button.BackColor = Color.FromArgb(204, 218, 229);
                    }
                    else
                    {
                        button.BackColor = Color.White;
                    }
                }
            }
        }
    }
}
