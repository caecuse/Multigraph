using Moq;
using MultigraphEditor.src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphTest
{
    [TestClass]
    public class MGraphEditorEdgeTests
    {
        [TestMethod]
        public void Constructor_SetsUniqueIdentifierAndGuid()
        {
            var edge1 = new MGraphEditorEdge();
            var edge2 = new MGraphEditorEdge();

            Assert.AreNotEqual(edge1.Identifier, edge2.Identifier);
            Assert.AreNotEqual(edge1._guid, edge2._guid);
        }

        [TestMethod]
        public void PopulateDrawing_SetsCorrectControlPoints()
        {
            var edge = new MGraphEditorEdge();
            var srcDrawableMock = new Mock<INodeDrawable>();
            var tgtDrawableMock = new Mock<INodeDrawable>();

            srcDrawableMock.SetupGet(s => s.X).Returns(0);
            srcDrawableMock.SetupGet(s => s.Y).Returns(0);
            tgtDrawableMock.SetupGet(t => t.X).Returns(100);
            tgtDrawableMock.SetupGet(t => t.Y).Returns(100);

            edge.PopulateDrawing(srcDrawableMock.Object, tgtDrawableMock.Object);

            Assert.AreEqual(50, edge.controlPointX);
            Assert.AreEqual(50, edge.controlPointY);
        }

        [TestMethod]
        public void PopulateNode_SetsPropertiesCorrectly()
        {
            var edge = new MGraphEditorEdge();
            var srcMock = new Mock<INode>();
            var tgtMock = new Mock<INode>();

            edge.PopulateEdge(srcMock.Object, tgtMock.Object, true, 5);

            Assert.AreEqual(srcMock.Object, edge.Source);
            Assert.AreEqual(tgtMock.Object, edge.Target);
            Assert.IsTrue(edge.Bidirectional);
            Assert.AreEqual(5, edge.Weight);
        }

        [TestMethod]
        public void Equals_IdentifiesSameInstanceAsEqual()
        {
            var edge = new MGraphEditorEdge();

            Assert.IsTrue(edge.Equals(edge));
        }

        [TestMethod]
        public void Equals_IdentifiesDifferentInstancesAsNotEqual()
        {
            var edge1 = new MGraphEditorEdge();
            var edge2 = new MGraphEditorEdge();

            Assert.IsFalse(edge1.Equals(edge2));
        }

        [TestMethod]
        public void Equals_IdentifiesDifferentTypesAsNotEqual()
        {
            var edge = new MGraphEditorEdge();
            var obj = new object();

            Assert.IsFalse(edge.Equals(obj));
        }

        [TestMethod]
        public void GetHashCode_ReturnsSameValueForSameInstance()
        {
            var edge = new MGraphEditorEdge();

            Assert.AreEqual(edge.GetHashCode(), edge.GetHashCode());
        }

        [TestMethod]
        public void IsInside_ReturnsTrueForPointOnLine()
        {
            var edge = new MGraphEditorEdge();
            var srcDrawable = new Mock<INodeDrawable>();
            var tgtDrawable = new Mock<INodeDrawable>();

            // Mock source and target to form a straight horizontal line from (0, 0) to (100, 0)
            srcDrawable.SetupGet(s => s.X).Returns(0f);
            srcDrawable.SetupGet(s => s.Y).Returns(0f);
            srcDrawable.SetupGet(s => s.Diameter).Returns(0f); // Assuming no diameter for simplicity
            tgtDrawable.SetupGet(t => t.X).Returns(100f);
            tgtDrawable.SetupGet(t => t.Y).Returns(0f);
            tgtDrawable.SetupGet(t => t.Diameter).Returns(0f);

            edge.PopulateDrawing(srcDrawable.Object, tgtDrawable.Object);

            // Check a point directly on the line
            Assert.IsTrue(edge.IsInside(50, 0));
        }

        [TestMethod]
        public void Equals_CorrectlyIdentifiesEqualAndNonEqualInstances()
        {
            var edge1 = new MGraphEditorEdge();
            var edge3 = new MGraphEditorEdge();

            Assert.IsFalse(edge1.Equals(edge3)); // Different instances with different states
        }

        [TestMethod]
        public void PopulateNode_AssignsNodesAndAttributesCorrectly()
        {
            var edge = new MGraphEditorEdge();
            var sourceMock = new Mock<INode>();
            var targetMock = new Mock<INode>();

            edge.PopulateEdge(sourceMock.Object, targetMock.Object, true, 10);

            Assert.AreEqual(sourceMock.Object, edge.Source);
            Assert.AreEqual(targetMock.Object, edge.Target);
            Assert.IsTrue(edge.Bidirectional);
            Assert.AreEqual(10, edge.Weight);
        }
    }
}
