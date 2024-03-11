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
        private IMGraphEditorEdge? selectedEdge = null;

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
                layer.nodes = [(IMGraphEditorNode)Activator.CreateInstance(nodeType)];
                layer.edges = [(IMGraphEditorEdge)Activator.CreateInstance(edgeType)];
                Layers.Add(layer);
            }

            Label layName = new Label();
            layName.Text = "Layer 1";
            layName.AutoSize = true;

            Layout0Panel.Controls.Add(layName);
            LayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 70));
            Button newLayer = new Button();
            newLayer.Text = "New Layer";
            newLayer.AutoSize = true;
            newLayer.Anchor = AnchorStyles.Left;
            newLayer.Anchor = AnchorStyles.Right;
            newLayer.Anchor = AnchorStyles.Top;


            //newLayer.Click += NewLayer_Click;
            //FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            //flowLayoutPanel.Controls.Add(newLayer);
            LayoutPanel.Controls.Add(newLayer, 0, LayoutPanel.RowCount - 1);
            //Layout0Panel.BackgroundImage = 
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            selectedEdge = null;
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.AddVertex;
        }

        private void ViewBtn_Click(object sender, EventArgs e)
        {
            selectedEdge = null;
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.View;
        }

        private void MoveBtn_Click(object sender, EventArgs e)
        {
            selectedEdge = null;
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.Default;
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            selectedEdge = null;
            selectedNode = null;
            selectedNodeForConnection = null;
            amode = ApplicationMode.Connect;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (IMGraphEditorNode node in nodeList)
            {
                foreach (IMGraphLayer layer in Layers)
                {
                    if (layer.Active)
                    {
                        node.Draw(sender, e, layer);
                    }
                }
            }
            foreach (IEdgeDrawable edge in edgeList)
            {
                foreach (IMGraphLayer layer in Layers)
                {
                    if (layer.Active)
                    {
                        edge.Draw(sender, e, layer);
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

                foreach (IMGraphLayer layer in Layers)
                {
                    if (layer.Active)
                    {
                        layer.nodes.Add(node);
                    }
                }
                canvas.Invalidate();
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
                                selectedEdge = edge;
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
                                }
                                else
                                {
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
                                            foreach (IMGraphLayer elayer in Layers)
                                            {
                                                if (elayer.Active)
                                                {
                                                    elayer.edges.Add(edge);
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

                if (selectedEdge != null)
                {
                    float dx = e.X - lastMouseLocation.X;
                    float dy = e.Y - lastMouseLocation.Y;
                    foreach (IMGraphEditorNode node in nodeList)
                    {
                        foreach (IMGraphLayer layer in Layers)
                        {
                            if (layer.Active)
                            {
                                if (selectedEdge.SourceDrawable == node || selectedEdge.TargetDrawable == node)
                                {
                                    node.X += dx;
                                    node.Y += dy;
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
            selectedEdge = null;
            if (amode == ApplicationMode.View)
            {
                isPanning = false;
            }
            // Create a bitmap to capture the content of the canvas control
            Bitmap canvasBitmap = new Bitmap(canvas.Width, canvas.Height);
            canvas.DrawToBitmap(canvasBitmap, new Rectangle(0, 0, canvas.Width, canvas.Height));
            Bitmap scaledBitmap = new Bitmap(canvasBitmap, new Size(Layout0Panel.Width, Layout0Panel.Height));
            // Draw the captured bitmap onto the Layout0Panel
            using (Graphics g = Layout0Panel.CreateGraphics())
            {
                g.DrawImage(scaledBitmap, 0, 0);
            }
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

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Settings button clicked");
        }
    }
}
