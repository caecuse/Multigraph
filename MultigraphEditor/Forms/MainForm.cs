using MultigraphEditor.Forms;
using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using MultigraphEditor.Src.design;
using MultigraphEditor.Src.graph;
using MultigraphEditor.Src.layers;
using System.Drawing.Printing;
using System.Net.NetworkInformation;
using static System.ComponentModel.Design.ObjectSelectorEditor;

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

        bool isPanning = false;
        private Point lastMouseLocation = Point.Empty;
        private IMGraphEditorNode? selectedNode = null;
        private IMGraphEditorNode? selectedNodeForConnection = null;
        private List<IMGraphEditorEdge> selectedEdges = new List<IMGraphEditorEdge>();
        private List<LayoutPreviewControl> previewControls = new List<LayoutPreviewControl>();
        private enum ApplicationMode
        {
            None,
            AddVertex,
            Default, // Serves for moving objects
            Connect,
            View, // Serves for moving canvas
            Delete,
        }
        public MainForm(Type nodet, Type edget, Type layert)
        {
            InitializeComponent();
            nodeType = nodet;
            edgeType = edget;
            layerType = layert;

            canvas.MouseDown += HandleMouseDown;
            canvas.MouseMove += HandleMouseMove;
            canvas.MouseUp += HandleMouseUp;
            LayoutPanel.Scroll += LayoutPanel_Scroll;
            LayoutPanel.MouseWheel += LayoutPanel_Scroll;
        }



        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
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
            if (Layers.Count == 0)
            {
                IMGraphLayer layer = (IMGraphLayer)Activator.CreateInstance(layerType);
                layer.nodes = new List<IMGraphEditorNode>();
                layer.edges = new List<IMGraphEditorEdge>();
                Layers.Add(layer);
            }
            Bitmap canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
            canvas.DrawToBitmap(canvasBitmap, new Rectangle(0, 0, canvas.Width, canvas.Height));
            LayoutPreviewControl prev = new LayoutPreviewControl(Layers[0], canvasBitmap);
            prev.CanvasInvalidated += (sender, e) =>
            {
                canvas.Invalidate(); // Invalidate the canvas on the main form
            };
            prev.LayerDeleted += (sender, e) =>
            {
                LayerDeletedHandler(sender, e);
            };
            LayoutPanel.RowStyles.Clear();
            LayoutPanel.Controls.Clear();

            LayoutPanel.RowStyles.Add(new RowStyle() { Height = 90, SizeType = SizeType.Absolute });
            LayoutPanel.RowCount++;
            LayoutPanel.Controls.Add(prev, 0, 0);
            previewControls.Add(prev);
            prev.MouseDown += HandleMouseDown;

            Button newLayer = new Button();
            newLayer.Text = "New Layer";
            newLayer.AutoSize = true;
            newLayer.Anchor = AnchorStyles.Left;
            newLayer.Anchor = AnchorStyles.Right;
            newLayer.Anchor = AnchorStyles.Top;
            newLayer.Click += AddLayer;
            LayoutPanel.Controls.Add(newLayer, 0, LayoutPanel.RowCount - 1);

            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(AddBtn, "Add vertex");
            toolTip.SetToolTip(ViewBtn, "Move canvas around");
            toolTip.SetToolTip(MoveBtn, "Move vertices");
            toolTip.SetToolTip(ConnectBtn, "Connect vertices");
            toolTip.SetToolTip(SettingsBtn, "Settings");
            toolTip.SetToolTip(GraphBtn, "Graph options");
        }



        private void AddBtn_Click(object sender, EventArgs e)
        {
            selectedEdges.Clear();
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.AddVertex;
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

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            UpdateLastClickedButton(sender);
            Console.WriteLine("Settings button clicked");
        }

        private void GraphBtn_Click(object sender, EventArgs e)
        {
            ContextMenuStrip contextMenuStrip = new ContextMenuStrip();

            ToolStripMenuItem save = new ToolStripMenuItem("Save");
            ToolStripMenuItem export = new ToolStripMenuItem("Export");
            ToolStripMenuItem import = new ToolStripMenuItem("Import");
            ToolStripMenuItem adjacency = new ToolStripMenuItem("Adjacency matrix");
            ToolStripMenuItem incidence = new ToolStripMenuItem("Incidence matrix");
            ToolStripMenuItem distance = new ToolStripMenuItem("Distance matrix");

            contextMenuStrip.Items.Add(save);
            contextMenuStrip.Items.Add(export);
            contextMenuStrip.Items.Add(import);
            contextMenuStrip.Items.Add(adjacency);
            contextMenuStrip.Items.Add(incidence);
            contextMenuStrip.Items.Add(distance);

            // Add event handlers for menu items
            adjacency.Click += (sender, e) => CreateMatrix(Layers, "adj");

            // Show the context menu strip at the location of the button
            contextMenuStrip.Show(GraphBtn, new System.Drawing.Point(0, GraphBtn.Height));
        }

        private void CreateMatrix(List<IMGraphLayer> layerList, string type)
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
                    // Draw the edge if source and target are in the same layer
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

            if (amode == ApplicationMode.Connect)
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
                                if (selectedNodeForConnection == null)
                                {
                                    selectedNodeForConnection = node;
                                    selectedNode = null;
                                    break;
                                }
                                else
                                {
                                    // Check if nodes are at least in one same layer
                                    if (selectedNodeForConnection != null && selectedNode != null)
                                    {
                                        bool areInSameLayer = false;
                                        foreach (IMGraphLayer elayer in Layers)
                                        {
                                            if (elayer.Active)
                                            {
                                                if (elayer.nodes.Contains(selectedNode) && elayer.nodes.Contains(selectedNodeForConnection))
                                                {
                                                    areInSameLayer = true;
                                                }
                                            }
                                        }
                                        if (!areInSameLayer)
                                        {
                                            MessageBox.Show("Nodes are not in the same layer");
                                            selectedNodeForConnection = null;
                                            selectedNode = null;
                                            break;
                                        }
                                    }
                                    IMGraphEditorEdge edge = (IMGraphEditorEdge)Activator.CreateInstance(edgeType);

                                    using (EditForm editform = new EditForm(edge))
                                    {
                                        editform.OnOk += (s, e) =>
                                        {
                                            editform.Close();
                                            edge.PopulateNode(selectedNodeForConnection, selectedNode, edge.Bidirectional, edge.Weight);
                                            edge.PopulateDrawing(selectedNodeForConnection, selectedNode);

                                            edge.SourceDrawable = selectedNodeForConnection;
                                            edge.TargetDrawable = selectedNode;
                                            edgeList.Add(edge);
                                            selectedNode.Neighbours.Add(selectedNodeForConnection);
                                            selectedNodeForConnection.Neighbours.Add(selectedNode);
                                            selectedNode.Edges.Add(edge);
                                            selectedNodeForConnection.Edges.Add(edge);

                                            foreach (IMGraphLayer layer in Layers)
                                            {
                                                // Check if the layer is active and contains both nodes
                                                if (layer.Active && layer.nodes.Contains(selectedNode) && layer.nodes.Contains(selectedNodeForConnection))
                                                {
                                                    // This layer is active and contains both nodes, so we add the edge to it
                                                    layer.edges.Add(edge);
                                                }
                                            }
                                        };
                                        editform.ShowDialog();
                                    }
                                    selectedNodeForConnection = null;
                                    selectedNode = null;
                                    canvas.Invalidate();
                                }
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

                    // Use a HashSet to track nodes that have already been moved, to avoid moving the same node multiple times
                    HashSet<IMGraphEditorNode> movedNodes = new HashSet<IMGraphEditorNode>();

                    foreach (var edge in selectedEdges)
                    {
                        foreach (IMGraphEditorNode node in nodeList)
                        {
                            if (edge.SourceDrawable == node || edge.TargetDrawable == node)
                            {
                                // Check if the node has not been moved already
                                if (!movedNodes.Contains(node))
                                {
                                    node.X += dx;
                                    node.Y += dy;
                                    movedNodes.Add(node); // Mark this node as moved
                                }
                            }
                        }
                    }

                    lastMouseLocation = e.Location; // Update the last mouse location
                    canvas.Invalidate(); // Invalidate the canvas to trigger a redraw
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
            foreach (IMGraphEditorEdge edge in edgeList)
            {
                foreach (IMGraphLayer layer in Layers)
                {
                    if (layer.Active)
                    {
                        if (edge.IsInside(e.X, e.Y))
                        {
                            using (EditForm editform = new EditForm(edge))
                            {
                                editform.OnOk += (s, e) =>
                                {
                                    editform.Close();
                                    canvas.Invalidate();
                                };
                                editform.ShowDialog();
                            }
                        }
                    }
                }
            }

            foreach (IMGraphEditorNode node in nodeList)
            {
                foreach (IMGraphLayer layer in Layers)
                {
                    if (layer.Active)
                    {
                        if (node.IsInside(e.X, e.Y))
                        {
                            using (EditForm editform = new EditForm(node))
                            {
                                editform.OnOk += (s, e) =>
                                {
                                    editform.Close();
                                    canvas.Invalidate();
                                };
                                editform.ShowDialog();
                            }
                        }
                    }
                }
            }
        }

        private void AddLayer(object? sender, EventArgs e)
        {
            IMGraphLayer layer = (IMGraphLayer)Activator.CreateInstance(layerType);
            layer.nodes = new List<IMGraphEditorNode>();
            layer.edges = new List<IMGraphEditorEdge>();
            Layers.Add(layer);

            Bitmap canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
            canvas.DrawToBitmap(canvasBitmap, new Rectangle(0, 0, canvas.Width, canvas.Height));
            LayoutPreviewControl prev = new LayoutPreviewControl(Layers[Layers.IndexOf(layer)], canvasBitmap);
            prev.LayerDeleted += (sender, e) =>
            {
                LayerDeletedHandler(sender, e);
            };
            prev.CanvasInvalidated += (sender, e) =>
            {
                canvas.Invalidate(); // Invalidate the canvas on the main form
            };

            LayoutPanel.RowCount--;
            LayoutPanel.RowCount++;
            LayoutPanel.RowStyles.Add(new RowStyle() { Height = 90, SizeType = SizeType.Absolute });
            LayoutPanel.Controls.Add(prev, LayoutPanel.RowCount - 1, 0);
            previewControls.Add(prev);
            prev.MouseDown += HandleMouseDown;
            LayoutPanel.RowCount++;
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
                MessageBox.Show("Cannot delete the last layer");
                return;
            }
            Layers.Remove(Layers[Layers.IndexOf(e)]);
            LayoutPanel.Controls.Remove((Control)sender);
            previewControls.Remove((LayoutPreviewControl)sender);
            LayoutPanel.RowCount--;
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
