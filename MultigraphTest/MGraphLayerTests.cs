using Moq;
using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphTest
{
    [TestClass]
    public class MgraphLayerTests
    {
        [TestMethod]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var layer = new MGraphLayer();

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
            var layer = new MGraphLayer();
            bool initialState = layer.Active;

            layer.changeActive();

            Assert.AreNotEqual(initialState, layer.Active);
        }

        [TestMethod]
        public void UpdateNodeReferences_UpdatesNodesList()
        {
            var layer = new MGraphLayer();
            var originalNode = new Mock<IMGraphEditorNode>().Object;
            var newNode = new Mock<IMGraphEditorNode>().Object;
            layer.nodes = new List<IMGraphEditorNode> { originalNode };

            var nodeMap = new Dictionary<IMGraphEditorNode, IMGraphEditorNode>
        {
            { originalNode, newNode }
        };

            layer.UpdateNodeReferences(nodeMap);

            Assert.IsTrue(layer.nodes.Contains(newNode) && !layer.nodes.Contains(originalNode));
        }

        [TestMethod]
        public void Equals_IdentifiesSameInstanceAsEqual()
        {
            var layer = new MGraphLayer();

            Assert.IsTrue(layer.Equals(layer));
        }

        [TestMethod]
        public void Equals_IdentifiesDifferentInstancesAsNotEqual()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();

            Assert.IsFalse(layer1.Equals(layer2));
        }

        [TestMethod]
        public void Equals_IdentifiesDifferentTypesAsNotEqual()
        {
            var layer = new MGraphLayer();
            var node = new MGraphEditorNode();

            Assert.IsFalse(layer.Equals(node));
        }

        [TestMethod]
        public void Clone_CreatesNewInstance()
        {
            var layer = new MGraphLayer();
            var clonedLayer = layer.Clone();

            Assert.AreEqual(layer, clonedLayer);
        }

        [TestMethod]
        public void Clone_CopiesProperties()
        {
            var layer = new MGraphLayer();
            var clonedLayer = layer.Clone();

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
            var layer = new MGraphLayer();
            layer.nodes = new List<IMGraphEditorNode>();
            var node = new MGraphEditorNode();
            layer.nodes.Add(node);

            var clonedLayer = layer.Clone();

            Assert.IsTrue(clonedLayer.nodes.Contains(node));
        }

        [TestMethod]
        public void Clone_CopiesEdges()
        {
            var layer = new MGraphLayer();
            layer.edges = new List<IMGraphEditorEdge>();
            var edge = new MGraphEditorEdge();
            layer.edges.Add(edge);

            var clonedLayer = layer.Clone();

            Assert.IsTrue(clonedLayer.edges.Contains(edge));
        }

        [TestMethod]
        public void GetHashCode_ReturnsSameValueForSameInstance()
        {
            var layer = new MGraphLayer();

            Assert.AreEqual(layer.GetHashCode(), layer.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentInstance()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentProperties()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();
            layer2.Name = "Test";

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentNodes()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();
            layer2.nodes = new List<IMGraphEditorNode> { new MGraphEditorNode() };

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentEdges()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();
            layer2.edges = new List<IMGraphEditorEdge> { new MGraphEditorEdge() };

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentActiveState()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();
            layer2.Active = false;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentArrowSize()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();
            layer2.arrowSize = 20;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentNodeWidth()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();
            layer2.nodeWidth = 20;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentEdgeWidth()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();
            layer2.edgeWidth = 20;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentColor()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();
            layer2.Color = Color.Red;

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentFont()
        {
            var layer1 = new MGraphLayer();
            var layer2 = new MGraphLayer();
            layer2.Font = new Font("Arial", 12);

            Assert.AreNotEqual(layer1.GetHashCode(), layer2.GetHashCode());
        }

        [TestMethod]
        public void CompareLayers_Clone()
        {
            var layer = new MGraphLayer();
            var node1 = new MGraphEditorNode();
            var node2 = new MGraphEditorNode();
            var edge1 = new MGraphEditorEdge();
            var edge2 = new MGraphEditorEdge();

            layer.nodes = new List<IMGraphEditorNode> { node1, node2 };
            layer.edges = new List<IMGraphEditorEdge> { edge1, edge2 };

            var clonedLayer = layer.Clone();

            Assert.AreEqual(layer, clonedLayer);
            Assert.AreNotSame(layer, clonedLayer);
        }
    }
}
