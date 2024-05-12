using Moq;
using MultigraphEditor.src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultigraphEditor.src.algorithm;
using MultigraphEditor.src.layers;


namespace MultigraphTest
{
    [TestClass]
    public class MGraphAlgorithmTests
    {
        [TestMethod]
        public void DjikstraTest_simpleCase_1()
        {
            var algorithm = new DijkstraAlgorithm();

            var startNode = new MGraphEditorNode();
            var endNode = new MGraphEditorNode();
            var targetLayer = new MGraphLayer();
            targetLayer.nodes = new List<IMGraphEditorNode>();
            targetLayer.edges = new List<IMGraphEditorEdge>();
            var edge = new MGraphEditorEdge();

            edge.PopulateEdge(startNode, endNode, true, 5);
            startNode.Edges.Add(edge);
            endNode.Edges.Add(edge);
            targetLayer.edges.Add(edge);
            targetLayer.nodes.Add(startNode);
            targetLayer.nodes.Add(endNode);

            var path = algorithm.Output(startNode, endNode, targetLayer);
            Assert.AreEqual(2, path.Count);
        }

        [TestMethod]
        public void DjikstraTest_simpleCase_2()
        {
            var algorithm = new DijkstraAlgorithm();

            var startNode = new MGraphEditorNode();
            var endNode = new MGraphEditorNode();
            var targetLayer = new MGraphLayer();
            targetLayer.nodes = new List<IMGraphEditorNode>();
            targetLayer.edges = new List<IMGraphEditorEdge>();
            var edge = new MGraphEditorEdge();

            edge.PopulateEdge(startNode, endNode, true, 5);
            startNode.Edges.Add(edge);
            endNode.Edges.Add(edge);
            targetLayer.edges.Add(edge);
            targetLayer.nodes.Add(startNode);
            targetLayer.nodes.Add(endNode);

            var path = algorithm.Output(startNode, endNode, targetLayer);
            Assert.AreEqual(2, path.Count);
        }

        [TestMethod]
        public void DjikstraTest_noPath()
        {
            var algorithm = new DijkstraAlgorithm();

            var startNode = new MGraphEditorNode();
            var endNode = new MGraphEditorNode();
            var targetLayer = new MGraphLayer();
            targetLayer.nodes = new List<IMGraphEditorNode>();
            targetLayer.edges = new List<IMGraphEditorEdge>();
            var edge = new MGraphEditorEdge();

            edge.PopulateEdge(startNode, endNode, true, 5);
            targetLayer.edges.Add(edge);
            targetLayer.nodes.Add(startNode);
            targetLayer.nodes.Add(endNode);

            var path = algorithm.Output(startNode, endNode, targetLayer);
            Assert.AreEqual(1, path.Count);
            Assert.AreEqual("No path found", path[0]);
        }

        [TestMethod]
        public void DjikstraTest_complexCase_1()
        {
            var algorithm = new DijkstraAlgorithm();

            var startNode = new MGraphEditorNode();
            var node1 = new MGraphEditorNode();
            var node2 = new MGraphEditorNode();
            var node3 = new MGraphEditorNode();
            var endNode = new MGraphEditorNode();

            var targetLayer = new MGraphLayer();
            targetLayer.nodes = new List<IMGraphEditorNode>();
            targetLayer.edges = new List<IMGraphEditorEdge>();
            var edge1 = new MGraphEditorEdge();
            var edge2 = new MGraphEditorEdge();
            var edge3 = new MGraphEditorEdge();
            var edge4 = new MGraphEditorEdge();
            var edge5 = new MGraphEditorEdge();
            var edge6 = new MGraphEditorEdge();
            var edge7 = new MGraphEditorEdge();
            var edge8 = new MGraphEditorEdge();

            edge1.PopulateEdge(startNode, node1, true, 5);
            edge2.PopulateEdge(startNode, node2, true, 5);
            edge3.PopulateEdge(node1, node2, true, 5);
            edge4.PopulateEdge(node1, node3, true, 5);
            edge5.PopulateEdge(node2, node3, true, 5);
            edge6.PopulateEdge(node2, endNode, true, 5);
            edge7.PopulateEdge(node3, endNode, true, 5);
            edge8.PopulateEdge(node1, endNode, true, 5);

            startNode.Edges.Add(edge1);
            startNode.Edges.Add(edge2);
            node1.Edges.Add(edge1);
            node1.Edges.Add(edge3);
            node1.Edges.Add(edge4);
            node1.Edges.Add(edge8);
            node2.Edges.Add(edge2);
            node2.Edges.Add(edge3);
            node2.Edges.Add(edge5);
            node2.Edges.Add(edge6);
            node3.Edges.Add(edge4);
            node3.Edges.Add(edge5);
            node3.Edges.Add(edge7);
            node3.Edges.Add(edge8);
            endNode.Edges.Add(edge6);
            endNode.Edges.Add(edge7);
            endNode.Edges.Add(edge8);

            targetLayer.edges.Add(edge1);
            targetLayer.edges.Add(edge2);
            targetLayer.edges.Add(edge3);
            targetLayer.edges.Add(edge4);
            targetLayer.edges.Add(edge5);
            targetLayer.edges.Add(edge6);
            targetLayer.edges.Add(edge7);
            targetLayer.edges.Add(edge8);
            targetLayer.nodes.Add(startNode);
            targetLayer.nodes.Add(node1);
            targetLayer.nodes.Add(node2);
            targetLayer.nodes.Add(node3);
            targetLayer.nodes.Add(endNode);

            var path = algorithm.Output(startNode, endNode, targetLayer);
            Assert.AreEqual(3, path.Count);
        }

        [TestMethod]
        public void CheckHamiltonCycleTest_simpleCase()
        {
            var algorithm = new CheckHamiltonCycle();

            var node1 = new MGraphEditorNode();
            var node2 = new MGraphEditorNode();
            var node3 = new MGraphEditorNode();

            var targetLayer = new MGraphLayer();
            targetLayer.nodes = new List<IMGraphEditorNode>();
            targetLayer.edges = new List<IMGraphEditorEdge>();
            var edge1 = new MGraphEditorEdge();
            var edge2 = new MGraphEditorEdge();
            var edge3 = new MGraphEditorEdge();

            edge1.PopulateEdge(node1, node2, true, 5);
            edge2.PopulateEdge(node2, node3, true, 5);
            edge3.PopulateEdge(node3, node1, true, 5);

            node1.Edges.Add(edge1);
            node1.Edges.Add(edge3);
            node2.Edges.Add(edge1);
            node2.Edges.Add(edge2);
            node3.Edges.Add(edge2);
            node3.Edges.Add(edge3);

            targetLayer.edges.Add(edge1);
            targetLayer.edges.Add(edge2);
            targetLayer.edges.Add(edge3);
            targetLayer.nodes.Add(node1);
            targetLayer.nodes.Add(node2);
            targetLayer.nodes.Add(node3);
            
            var output = algorithm.Output(node1, node1, targetLayer);
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual("Graph has a Hamilton cycle", output[0]);
        }

        [TestMethod]
        public void CheckHamiltonCycleTest_moreNodesThanEdges()
        {
            var algorithm = new CheckHamiltonCycle();

            var node1 = new MGraphEditorNode();
            var node2 = new MGraphEditorNode();
            var node3 = new MGraphEditorNode();

            var targetLayer = new MGraphLayer();
            targetLayer.nodes = new List<IMGraphEditorNode>();
            targetLayer.edges = new List<IMGraphEditorEdge>();
            var edge1 = new MGraphEditorEdge();
            var edge2 = new MGraphEditorEdge();

            edge1.PopulateEdge(node1, node2, true, 5);
            edge2.PopulateEdge(node2, node3, true, 5);

            node1.Edges.Add(edge1);
            node2.Edges.Add(edge1);
            node2.Edges.Add(edge2);
            node3.Edges.Add(edge2);

            targetLayer.edges.Add(edge1);
            targetLayer.edges.Add(edge2);
            targetLayer.nodes.Add(node1);
            targetLayer.nodes.Add(node2);
            targetLayer.nodes.Add(node3);

            var output = algorithm.Output(node1, node1, targetLayer);
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual("Graph has more nodes than edges, so it can't have a Hamilton cycle", output[0]);
        }

        [TestMethod]
        public void CheckHamiltonCycleTest_lessThan3Nodes()
        {
            var algorithm = new CheckHamiltonCycle();

            var node1 = new MGraphEditorNode();
            var node2 = new MGraphEditorNode();

            var targetLayer = new MGraphLayer();
            targetLayer.nodes = new List<IMGraphEditorNode>();
            targetLayer.edges = new List<IMGraphEditorEdge>();
            var edge1 = new MGraphEditorEdge();

            edge1.PopulateEdge(node1, node2, true, 5);

            node1.Edges.Add(edge1);
            node2.Edges.Add(edge1);

            targetLayer.edges.Add(edge1);
            targetLayer.nodes.Add(node1);
            targetLayer.nodes.Add(node2);

            var output = algorithm.Output(node1, node1, targetLayer);
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual("Graph has less than 3 nodes, so it can't have a Hamilton cycle", output[0]);
        }

        [TestMethod]
        public void CheckHamiltonCycleTest_nodeWithLessThan2Edges()
        {
            var algorithm = new CheckHamiltonCycle();

            var node1 = new MGraphEditorNode();
            var node2 = new MGraphEditorNode();
            var node3 = new MGraphEditorNode();

            var targetLayer = new MGraphLayer();
            targetLayer.nodes = new List<IMGraphEditorNode>();
            targetLayer.edges = new List<IMGraphEditorEdge>();
            var edge1 = new MGraphEditorEdge();
            var edge2 = new MGraphEditorEdge();

            edge1.PopulateEdge(node1, node2, true, 5);
            edge2.PopulateEdge(node2, node3, true, 5);

            node1.Edges.Add(edge1);
            node2.Edges.Add(edge1);
            node2.Edges.Add(edge2);
            node3.Edges.Add(edge2);

            targetLayer.edges.Add(edge1);
            targetLayer.edges.Add(edge2);
            targetLayer.nodes.Add(node1);
            targetLayer.nodes.Add(node2);
            targetLayer.nodes.Add(node3);

            var output = algorithm.Output(node1, node1, targetLayer);
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual("Graph has more nodes than edges, so it can't have a Hamilton cycle", output[0]);
        }

        [TestMethod]
        public void CheckHamiltonCycleTest_noHamiltonCycle()
        {
            var algorithm = new CheckHamiltonCycle();

            var node1 = new MGraphEditorNode();
            var node2 = new MGraphEditorNode();
            var node3 = new MGraphEditorNode();

            var targetLayer = new MGraphLayer();
            targetLayer.nodes = new List<IMGraphEditorNode>();
            targetLayer.edges = new List<IMGraphEditorEdge>();
            var edge1 = new MGraphEditorEdge();
            var edge2 = new MGraphEditorEdge();
            var edge3 = new MGraphEditorEdge();

            edge1.PopulateEdge(node1, node2, true, 5);
            edge2.PopulateEdge(node2, node3, true, 5);
            edge3.PopulateEdge(node3, node1, true, 5);

            node1.Edges.Add(edge1);
            node1.Edges.Add(edge3);
            node2.Edges.Add(edge1);
            node2.Edges.Add(edge2);
            node3.Edges.Add(edge2);

            targetLayer.edges.Add(edge1);
            targetLayer.edges.Add(edge2);
            targetLayer.edges.Add(edge3);
            targetLayer.nodes.Add(node1);
            targetLayer.nodes.Add(node2);
            targetLayer.nodes.Add(node3);

            var output = algorithm.Output(node1, node2, targetLayer);
            Assert.AreEqual(1, output.Count);
            Assert.AreEqual("Node " + node3.Label +" has less than 2 edges, so it can't be part of a Hamilton cycle", output[0]);
        }
    }
}
