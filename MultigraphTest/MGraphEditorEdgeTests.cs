using Moq;
using MultigraphEditor.src.graph;

namespace MultigraphTest
{
    [TestClass]
    public class MGraphEditorEdgeTests
    {
        [TestMethod]
        public void Constructor_SetsUniqueIdentifierAndGuid()
        {
            MGraphEditorEdge edge1 = new MGraphEditorEdge();
            MGraphEditorEdge edge2 = new MGraphEditorEdge();

            Assert.AreNotEqual(edge1.Identifier, edge2.Identifier);
            Assert.AreNotEqual(edge1._guid, edge2._guid);
        }

        [TestMethod]
        public void PopulateDrawing_SetsCorrectControlPoints()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();
            Mock<INodeDrawable> srcDrawableMock = new Mock<INodeDrawable>();
            Mock<INodeDrawable> tgtDrawableMock = new Mock<INodeDrawable>();

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
            MGraphEditorEdge edge = new MGraphEditorEdge();
            Mock<INode> srcMock = new Mock<INode>();
            Mock<INode> tgtMock = new Mock<INode>();

            edge.PopulateEdge(srcMock.Object, tgtMock.Object, true, 5);

            Assert.AreEqual(srcMock.Object, edge.Source);
            Assert.AreEqual(tgtMock.Object, edge.Target);
            Assert.IsTrue(edge.Bidirectional);
            Assert.AreEqual(5, edge.Weight);
        }

        [TestMethod]
        public void Equals_IdentifiesSameInstanceAsEqual()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();

            Assert.IsTrue(edge.Equals(edge));
        }

        [TestMethod]
        public void Equals_IdentifiesDifferentInstancesAsNotEqual()
        {
            MGraphEditorEdge edge1 = new MGraphEditorEdge();
            MGraphEditorEdge edge2 = new MGraphEditorEdge();

            Assert.IsFalse(edge1.Equals(edge2));
        }

        [TestMethod]
        public void Equals_IdentifiesDifferentTypesAsNotEqual()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();
            object obj = new object();

            Assert.IsFalse(edge.Equals(obj));
        }

        [TestMethod]
        public void GetHashCode_ReturnsSameValueForSameInstance()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();

            Assert.AreEqual(edge.GetHashCode(), edge.GetHashCode());
        }

        [TestMethod]
        public void IsInside_ReturnsTrueForPointOnLine()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();
            Mock<INodeDrawable> srcDrawable = new Mock<INodeDrawable>();
            Mock<INodeDrawable> tgtDrawable = new Mock<INodeDrawable>();

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
            MGraphEditorEdge edge1 = new MGraphEditorEdge();
            MGraphEditorEdge edge3 = new MGraphEditorEdge();

            Assert.IsFalse(edge1.Equals(edge3)); // Different instances with different states
        }

        [TestMethod]
        public void PopulateNode_AssignsNodesAndAttributesCorrectly()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();
            Mock<INode> sourceMock = new Mock<INode>();
            Mock<INode> targetMock = new Mock<INode>();

            edge.PopulateEdge(sourceMock.Object, targetMock.Object, true, 10);

            Assert.AreEqual(sourceMock.Object, edge.Source);
            Assert.AreEqual(targetMock.Object, edge.Target);
            Assert.IsTrue(edge.Bidirectional);
            Assert.AreEqual(10, edge.Weight);
        }

        [TestMethod]
        public void GetHashCode_ReturnsDifferentValueForDifferentInstance()
        {
            MGraphEditorEdge edge1 = new MGraphEditorEdge();
            MGraphEditorEdge edge2 = new MGraphEditorEdge();

            Assert.AreNotEqual(edge1.GetHashCode(), edge2.GetHashCode());
        }

        [TestMethod]
        public void IsInside_ReturnsFalseForPointOutsideEdge()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();
            Mock<INodeDrawable> srcDrawable = new Mock<INodeDrawable>();
            Mock<INodeDrawable> tgtDrawable = new Mock<INodeDrawable>();

            // Mock source and target to form a straight horizontal line from (0, 0) to (100, 0)
            srcDrawable.SetupGet(s => s.X).Returns(0f);
            srcDrawable.SetupGet(s => s.Y).Returns(0f);
            srcDrawable.SetupGet(s => s.Diameter).Returns(0f); // Assuming no diameter for simplicity
            tgtDrawable.SetupGet(t => t.X).Returns(100f);
            tgtDrawable.SetupGet(t => t.Y).Returns(0f);
            tgtDrawable.SetupGet(t => t.Diameter).Returns(0f);

            edge.PopulateDrawing(srcDrawable.Object, tgtDrawable.Object);

            // Check a point outside the line
            Assert.IsFalse(edge.IsInside(50, 10));
        }

        [TestMethod]
        public void PopulateDrawing_SetsControlPointsCorrectly()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();
            Mock<INodeDrawable> srcDrawable = new Mock<INodeDrawable>();
            Mock<INodeDrawable> tgtDrawable = new Mock<INodeDrawable>();

            srcDrawable.SetupGet(s => s.X).Returns(0);
            srcDrawable.SetupGet(s => s.Y).Returns(0);
            tgtDrawable.SetupGet(t => t.X).Returns(100);
            tgtDrawable.SetupGet(t => t.Y).Returns(100);

            edge.PopulateDrawing(srcDrawable.Object, tgtDrawable.Object);

            Assert.AreEqual(50, edge.controlPointX);
            Assert.AreEqual(50, edge.controlPointY);
        }

        [TestMethod]
        public void Clone_ReturnsEqualInstance()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();
            IMGraphEditorEdge clonedEdge = edge.Clone();

            Assert.AreEqual(edge, clonedEdge);
        }

        [TestMethod]
        public void Clone_ReturnsDifferentInstance()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();
            IMGraphEditorEdge clonedEdge = edge.Clone();

            Assert.AreNotSame(edge, clonedEdge);
        }

        [TestMethod]
        public void Clone_ReturnsEqualButNotSameInstance()
        {
            MGraphEditorEdge edge = new MGraphEditorEdge();
            IMGraphEditorEdge clonedEdge = edge.Clone();

            Assert.AreEqual(edge, clonedEdge);
            Assert.AreNotSame(edge, clonedEdge);
        }
    }
}
