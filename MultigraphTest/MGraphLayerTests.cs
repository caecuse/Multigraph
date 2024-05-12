using Moq;
using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using System.Drawing;

namespace MultigraphTest
{
    [TestClass]
    public class MgraphLayerTests
    {
        [TestMethod]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            MGraphLayer layer = new MGraphLayer();

            Assert.IsNotNull(layer.Font);
            Assert.AreEqual(Color.Black, layer.Color);
            Assert.AreEqual(4, layer.nodeWidth);
            Assert.AreEqual(4, layer.edgeWidth);
            Assert.IsTrue(layer.Active);
            Assert.AreEqual(10, layer.arrowSize);
            Assert.IsNotNull(layer.Name);
            Assert.IsTrue(layer.Identifier >= 0); // Assuming id starts from 0 and increments
        }

        [TestMethod]
        public void ChangeActive_TogglesActiveState()
        {
            MGraphLayer layer = new MGraphLayer();
            bool initialState = layer.Active;

            layer.changeActive();

            Assert.AreNotEqual(initialState, layer.Active);
        }

        [TestMethod]
        public void UpdateNodeReferences_UpdatesNodesList()
        {
            MGraphLayer layer = new MGraphLayer();
            IMGraphEditorNode originalNode = new Mock<IMGraphEditorNode>().Object;
            IMGraphEditorNode newNode = new Mock<IMGraphEditorNode>().Object;
            layer.nodes = new List<IMGraphEditorNode> { originalNode };

            Dictionary<IMGraphEditorNode, IMGraphEditorNode> nodeMap = new Dictionary<IMGraphEditorNode, IMGraphEditorNode>
        {
            { originalNode, newNode }
        };

            layer.UpdateNodeReferences(nodeMap);

            Assert.IsTrue(layer.nodes.Contains(newNode) && !layer.nodes.Contains(originalNode));
        }

        [TestMethod]
        public void Equals_IdentifiesSameInstanceAsEqual()
        {
            MGraphLayer layer = new MGraphLayer();

            Assert.IsTrue(layer.Equals(layer));
        }

        [TestMethod]
        public void Equals_IdentifiesDifferentInstancesAsNotEqual()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();

            Assert.IsFalse(layer1.Equals(layer2));
        }

        [TestMethod]
        public void Equals_IdentifiesDifferentTypesAsNotEqual()
        {
            MGraphLayer layer = new MGraphLayer();
            MGraphEditorNode node = new MGraphEditorNode();

            Assert.IsFalse(layer.Equals(node));
        }

        [TestMethod]
        public void Clone_CreatesNewInstance()
        {
            MGraphLayer layer = new MGraphLayer();
            IMGraphLayer clonedLayer = layer.Clone();

            Assert.AreEqual(layer, clonedLayer);
        }

        [TestMethod]
        public void Clone_CopiesProperties()
        {
            MGraphLayer layer = new MGraphLayer();
            IMGraphLayer clonedLayer = layer.Clone();

            Assert.AreEqual(layer.Font, clonedLayer.Font);
            Assert.AreEqual(layer.Color, clonedLayer.Color);
            Assert.AreEqual(layer.nodeWidth, clonedLayer.nodeWidth);
            Assert.AreEqual(layer.edgeWidth, clonedLayer.edgeWidth);
            Assert.AreEqual(layer.Active, clonedLayer.Active);
            Assert.AreEqual(layer.arrowSize, clonedLayer.arrowSize);
            Assert.AreEqual(layer.Name, clonedLayer.Name);
        }

        [TestMethod]
        public void Clone_CopiesNodes()
        {
            MGraphLayer layer = new MGraphLayer();
            layer.nodes = new List<IMGraphEditorNode>();
            MGraphEditorNode node = new MGraphEditorNode();
            layer.nodes.Add(node);

            IMGraphLayer clonedLayer = layer.Clone();

            Assert.IsTrue(clonedLayer.nodes.Contains(node));
        }

        [TestMethod]
        public void Clone_CopiesEdges()
        {
            MGraphLayer layer = new MGraphLayer();
            layer.edges = new List<IMGraphEditorEdge>();
            MGraphEditorEdge edge = new MGraphEditorEdge();
            layer.edges.Add(edge);

            IMGraphLayer clonedLayer = layer.Clone();

            Assert.IsTrue(clonedLayer.edges.Contains(edge));
        }

        [TestMethod]
        public void GetHashCode_ReturnsSameValueForSameInstance()
        {
            MGraphLayer layer = new MGraphLayer();

            Assert.AreEqual(layer.GetHashCode(), layer.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentInstance()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentProperties()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();
            layer2.Name = "Test";

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentNodes()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();
            layer2.nodes = new List<IMGraphEditorNode> { new MGraphEditorNode() };

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentEdges()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();
            layer2.edges = new List<IMGraphEditorEdge> { new MGraphEditorEdge() };

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentActiveState()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();
            layer2.Active = false;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentArrowSize()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();
            layer2.arrowSize = 20;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentNodeWidth()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();
            layer2.nodeWidth = 20;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentEdgeWidth()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();
            layer2.edgeWidth = 20;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentColor()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();
            layer2.Color = Color.Red;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentFont()
        {
            MGraphLayer layer1 = new MGraphLayer();
            MGraphLayer layer2 = new MGraphLayer();
            layer2.Font = new Font("Arial", 12);

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void CompareLayers_Clone()
        {
            MGraphLayer layer = new MGraphLayer();
            MGraphEditorNode node1 = new MGraphEditorNode();
            MGraphEditorNode node2 = new MGraphEditorNode();
            MGraphEditorEdge edge1 = new MGraphEditorEdge();
            MGraphEditorEdge edge2 = new MGraphEditorEdge();

            layer.nodes = new List<IMGraphEditorNode> { node1, node2 };
            layer.edges = new List<IMGraphEditorEdge> { edge1, edge2 };

            IMGraphLayer clonedLayer = layer.Clone();

            Assert.AreEqual(layer, clonedLayer);
            Assert.AreNotSame(layer, clonedLayer);
        }
    }
}
