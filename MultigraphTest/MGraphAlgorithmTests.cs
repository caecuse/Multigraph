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

        }

    }
}
