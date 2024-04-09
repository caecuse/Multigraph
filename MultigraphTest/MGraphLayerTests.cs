using Moq;
using MultigraphEditor.src.graph;
using MultigraphEditor.src.layers;
using MultigraphEditor.Src.graph;
using MultigraphEditor.Src.layers;
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
    }
}
