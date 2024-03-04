using MultigraphEditor.Forms;
using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using MultigraphEditor.Src.graph;
using System.Net.NetworkInformation;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace MultigraphEditor
{
    public partial class MainForm : Form
    {
        private ApplicationMode amode = ApplicationMode.None;
        public List<IMGraphEditorNode> nodeList = new List<IMGraphEditorNode>();
        public List<IMGraphEditorEdge> edgeList = new List<IMGraphEditorEdge>();
        public List<INodeLayer> nodeLayers = new List<INodeLayer>();
        public List<IEdgeLayer> edgeLayers = new List<IEdgeLayer>();
        Type nodeType;
        Type edgeType;
        Type nodeLayerType;
        Type edgeLayerType;

        bool isPanning = false;
        private Point lastMouseLocation = Point.Empty;
        private IMGraphEditorNode? selectedNode = null;
        private IMGraphEditorNode? selectedNodeForConnection = null;

        private enum ApplicationMode
        {
            None,
            AddVertex,
            Default, // Serves for moving objects
            Connect,
            View, // Serves for moving canvas
            Delete,
        }
        public MainForm(Type nodet, Type edget, Type nlayert, Type elayert)
        {
            InitializeComponent();
            nodeType = nodet;
            edgeType = edget;
            nodeLayerType = nlayert;
            edgeLayerType = elayert;

            canvas.MouseDown += HandleMouseDown;
            canvas.MouseMove += HandleMouseMove;
            canvas.MouseUp += HandleMouseUp;
        }

        private void HandleMouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Handle left mouse button down
                LeftMouseDown(sender, e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Handle right mouse button down
                RightMouseDown(sender, e);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (nodeLayers.Count == 0)
            {
                MGraphEditorNodeLayer layer = new MGraphEditorNodeLayer();
                layer.nodes = [(IMGraphEditorNode)Activator.CreateInstance(nodeType)];
                nodeLayers.Add(layer);
            }
            if (edgeLayers.Count == 0)
            {
                MGraphEditorEdgeLayer layer = new MGraphEditorEdgeLayer();
                layer.edges = [(IMGraphEditorEdge)Activator.CreateInstance(edgeType)];
                edgeLayers.Add(layer);
            }
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            amode = ApplicationMode.AddVertex;
        }

        private void ViewBtn_Click(object sender, EventArgs e)
        {
            amode = ApplicationMode.View;
        }

        private void MoveBtn_Click(object sender, EventArgs e)
        {
            amode = ApplicationMode.Default;
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            amode = ApplicationMode.Connect;
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            foreach (IMGraphEditorNode node in nodeList)
            {
                foreach (MGraphEditorNodeLayer layer in nodeLayers)
                {
                    if (layer.Active)
                    {
                        node.Draw(sender, e, layer);
                    }
                }
            }
            foreach (IEdgeDrawable edge in edgeList)
            {
                foreach (MGraphEditorEdgeLayer layer in edgeLayers)
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
                node.X = e.X;
                node.Y = e.Y;
                nodeList.Add(node);

                foreach (MGraphEditorNodeLayer layer in nodeLayers)
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
                    foreach (INodeLayer layer in nodeLayers)
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
            }

            if (amode == ApplicationMode.Connect)
            {
                foreach (IMGraphEditorNode node in nodeList)
                {
                    foreach (INodeLayer layer in nodeLayers)
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

                                    using (EdgeForm edgeForm = new EdgeForm(edge))
                                    {
                                        edgeForm.OnOk += (s, e) =>
                                        {
                                            edgeForm.Close();
                                            edge.SourceDrawable = selectedNodeForConnection;
                                            edge.TargetDrawable = selectedNode;
                                            edgeList.Add(edge);
                                            selectedNode.Neighbours.Add(selectedNodeForConnection);
                                            selectedNodeForConnection.Neighbours.Add(selectedNode);
                                            selectedNode.Edges.Add(edge);
                                            selectedNodeForConnection.Edges.Add(edge);
                                            foreach (IEdgeLayer elayer in edgeLayers)
                                            {
                                                if (elayer.Active)
                                                {
                                                    elayer.edges.Add(edge);
                                                }
                                            }
                                        };
                                        edgeForm.ShowDialog();
                                    }
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
            if (amode == ApplicationMode.View)
            {
                isPanning = false;
            }
        }

        private void RightMouseDown(object sender, MouseEventArgs e)
        {

        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Settings button clicked");
        }


    }
}
