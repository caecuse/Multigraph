using System;
using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using TestStack.White;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.ListBoxItems;
using TestStack.White.UIItems.MenuItems;
using TestStack.White.UIItems.WindowItems;


namespace MultigraphTest
{
    [TestClass]
    public class AutomationTests
    {
        [TestMethod]
        public void MainForm_NodeProperlyCreated()
        {
            Application app = Application.Launch("MultigraphEditor.exe");
            Window window = app.GetWindow("Multigraph Editor");
            Button addBtn = window.Get<Button>(SearchCriteria.ByText("Add"));
            Panel panel = window.Get<Panel>(SearchCriteria.ByAutomationId("canvas"));

            addBtn.Click();

            Point panelLocation = panel.Bounds.Location;
            int screenX = panelLocation.X + 100;
            int screenY = panelLocation.Y + 100;

            window.Mouse.Location = new Point(screenX, screenY);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            Thread.Sleep(1000);

            Window editWindow = window.ModalWindow("Edit Form");
            Button okBtn = editWindow.Get<Button>(SearchCriteria.ByText("Ok"));
            okBtn.Click();

            Thread.Sleep(1000);

            window.Mouse.Location = new Point(screenX, screenY);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            Window editWindowForCreatedNode = window.ModalWindow("Edit Form");

            Assert.IsNotNull(editWindowForCreatedNode, "Window for created node was not opened.");

            app.Close();
        }

        [TestMethod]
        public void MainForm_NodeProperlyDeleted()
        {
            Application app = Application.Launch("MultigraphEditor.exe");
            Window window = app.GetWindow("Multigraph Editor");
            Button addBtn = window.Get<Button>(SearchCriteria.ByText("Add"));
            Panel p = window.Get<Panel>(SearchCriteria.ByAutomationId("canvas"));

            addBtn.Click();

            Point panelLocation = p.Bounds.Location;
            int screenX = panelLocation.X + 100;
            int screenY = panelLocation.Y + 100;

            window.Mouse.Location = new Point(screenX, screenY);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            Thread.Sleep(1000);

            Window editWindow = window.ModalWindow("Edit Form");
            Button okBtn = editWindow.Get<Button>(SearchCriteria.ByText("Ok"));
            okBtn.Click();

            Thread.Sleep(1000);

            Button deleteBtn = window.Get<Button>(SearchCriteria.ByText("Remove object"));
            deleteBtn.Click();

            Thread.Sleep(1000);

            window.Mouse.Location = new Point(screenX, screenY);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            Thread.Sleep(1000);

            window.Mouse.Location = new Point(screenX, screenY);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
                Assert.Fail("Edit Form for deleted node was opened.");
            }
            else
            {
                Assert.IsTrue(true, "Edit Form for deleted node was not opened.");
            }

            app.Close();
        }

        [TestMethod]
        public void MainForm_NodeProperlyEdited()
        {
            Application app = Application.Launch("MultigraphEditor.exe");
            Window window = app.GetWindow("Multigraph Editor");
            Button addBtn = window.Get<Button>(SearchCriteria.ByText("Add"));
            Panel p = window.Get<Panel>(SearchCriteria.ByAutomationId("canvas"));

            addBtn.Click();

            Point panelLocation = p.Bounds.Location;
            int screenX = panelLocation.X + 100;
            int screenY = panelLocation.Y + 100;

            window.Mouse.Location = new Point(screenX, screenY);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            Thread.Sleep(1000);

            Window editWindow = window.ModalWindow("Edit Form");
            TextBox labelTextBox = editWindow.Get<TextBox>(SearchCriteria.ByAutomationId("Label"));
            labelTextBox.Text = "Test";

            Button okBtn = editWindow.Get<Button>(SearchCriteria.ByText("Ok"));
            okBtn.Click();

            Thread.Sleep(1000);

            window.Mouse.Location = new Point(screenX, screenY);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            Window editWindowForEditedNode = window.ModalWindow("Edit Form");
            TextBox labelTextBoxForEditedNode = editWindowForEditedNode.Get<TextBox>(SearchCriteria.ByAutomationId("Label"));

            Assert.AreEqual("Test", labelTextBoxForEditedNode.Text, "Node was not properly edited.");

            app.Close();
        }

        [TestMethod]
        public void MainForm_All()
        {
            // Get Main Window
            Application app = Application.Launch("MultigraphEditor.exe");
            Window window = app.GetWindow("Multigraph Editor");

            // Node locations
            Point node1 = new Point(200, 200);
            Point node2 = new Point(300, 300);
            Point node3 = new Point(400, 300);
            Point node4 = new Point(500, 200);
            Point node5 = new Point(400, 100);
            Point node6 = new Point(300, 100);

            // Create nodes
            window.Get<Button>(SearchCriteria.ByText("Add")).Click();
            Panel p = window.Get<Panel>(SearchCriteria.ByAutomationId("canvas"));
            Window ew;

            Point panelLocation = p.Bounds.Location;

            // node 1
            window.Mouse.Location = new Point(panelLocation.X + node1.X, panelLocation.Y + node1.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // node 2
            window.Mouse.Location = new Point(panelLocation.X + node2.X, panelLocation.Y + node2.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // node 3
            window.Mouse.Location = new Point(panelLocation.X + node3.X, panelLocation.Y + node3.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // node 4
            window.Mouse.Location = new Point(panelLocation.X + node4.X, panelLocation.Y + node4.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // node 5
            window.Mouse.Location = new Point(panelLocation.X + node5.X, panelLocation.Y + node5.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // node 6
            window.Mouse.Location = new Point(panelLocation.X + node6.X, panelLocation.Y + node6.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // Create edges connecting nodes 1 to 2, 2 to 3, 3 to 4, 4 to 5, 5 to 6, 6 to 1
            window.Get<Button>(SearchCriteria.ByText("Connect")).Click();

            // 1 to 2
            window.Mouse.Location = new Point(panelLocation.X + node1.X, panelLocation.Y + node1.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            window.Mouse.Location = new Point(panelLocation.X + node2.X, panelLocation.Y + node2.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // 2 to 3
            window.Mouse.Location = new Point(panelLocation.X + node2.X, panelLocation.Y + node2.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            window.Mouse.Location = new Point(panelLocation.X + node3.X, panelLocation.Y + node3.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // 3 to 4
            window.Mouse.Location = new Point(panelLocation.X + node3.X, panelLocation.Y + node3.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            window.Mouse.Location = new Point(panelLocation.X + node4.X, panelLocation.Y + node4.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // 4 to 5
            window.Mouse.Location = new Point(panelLocation.X + node4.X, panelLocation.Y + node4.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            window.Mouse.Location = new Point(panelLocation.X + node5.X, panelLocation.Y + node5.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // 5 to 6
            window.Mouse.Location = new Point(panelLocation.X + node5.X, panelLocation.Y + node5.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            window.Mouse.Location = new Point(panelLocation.X + node6.X, panelLocation.Y + node6.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // 6 to 1
            window.Mouse.Location = new Point(panelLocation.X + node6.X, panelLocation.Y + node6.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            window.Mouse.Location = new Point(panelLocation.X + node1.X, panelLocation.Y + node1.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Left);

            ew = window.ModalWindow("Edit Form");
            ew.Get<Button>(SearchCriteria.ByText("Ok")).Click();

            // Check existance of nodes and edges

            // node 1
            window.Mouse.Location = new Point(panelLocation.X + node1.X, panelLocation.Y + node1.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 1 was not opened.");
            }

            // node 2
            window.Mouse.Location = new Point(panelLocation.X + node2.X, panelLocation.Y + node2.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 2 was not opened.");
            }

            // node 3
            window.Mouse.Location = new Point(panelLocation.X + node3.X, panelLocation.Y + node3.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 3 was not opened.");
            }

            // node 4
            window.Mouse.Location = new Point(panelLocation.X + node4.X, panelLocation.Y + node4.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 4 was not opened.");
            }

            // node 5
            window.Mouse.Location = new Point(panelLocation.X + node5.X, panelLocation.Y + node5.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 5 was not opened.");
            }

            // node 6
            window.Mouse.Location = new Point(panelLocation.X + node6.X, panelLocation.Y + node6.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 6 was not opened.");
            }

            // Check edges
            // edge 1 to 2
            window.Mouse.Location = new Point(panelLocation.X + ((node1.X + node2.X) / 2), panelLocation.Y + ((node1.Y + node2.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 1 to 2 was not opened.");
            }

            // edge 2 to 3
            window.Mouse.Location = new Point(panelLocation.X + ((node2.X + node3.X) / 2), panelLocation.Y + ((node2.Y + node3.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 2 to 3 was not opened.");
            }

            // edge 3 to 4
            window.Mouse.Location = new Point(panelLocation.X + ((node3.X + node4.X) / 2), panelLocation.Y + ((node3.Y + node4.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 3 to 4 was not opened.");
            }

            // edge 4 to 5
            window.Mouse.Location = new Point(panelLocation.X + ((node4.X + node5.X) / 2), panelLocation.Y + ((node4.Y + node5.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 4 to 5 was not opened.");
            }

            // edge 5 to 6
            window.Mouse.Location = new Point(panelLocation.X + ((node5.X + node6.X) / 2), panelLocation.Y + ((node5.Y + node6.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 5 to 6 was not opened.");
            }

            // edge 6 to 1
            window.Mouse.Location = new Point(panelLocation.X + ((node6.X + node1.X) / 2), panelLocation.Y + ((node6.Y + node1.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 6 to 1 was not opened.");
            }

            // Check Dijkstra algorithm
            window.Get<Button>(SearchCriteria.ByText("Algorithms")).Click();
            Window algoForm = window.ModalWindow("Algorithm Form");
            var algobox = algoForm.Get<ComboBox>(SearchCriteria.ByAutomationId("algorithmComboBox"));
            // If the combobox is not expanded it doesnt have "any" items
            algobox.Click();
            algoForm.Get<ComboBox>(SearchCriteria.ByAutomationId("algorithmComboBox")).Select("DijkstraAlgorithm");
            algoForm.Get<Button>(SearchCriteria.ByText("Run selected alghoritm")).Click();

            Window DijWindow = algoForm.ModalWindow("Dijkstra's Algorithm");

            var layerbox = DijWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("layerComboBox"));
            // Again if the combobox is not expanded it doesnt have "any" items
            layerbox.Click();

            DijWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("layerComboBox")).Select("Layer 0");

            var startNode = DijWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("startNode"));
            startNode.Click();
            DijWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("startNode")).Select("0");

            var endNode = DijWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("endNode"));
            endNode.Click();
            DijWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("endNode")).Select("5");

            DijWindow.Get<Button>(SearchCriteria.ByText("Find path")).Click();

            var result = DijWindow.Get<ListBox>(SearchCriteria.ByAutomationId("pathListBox"));

            Assert.AreEqual("0", result.Items[0].Text, "Dijkstra algorithm failed.");
            Assert.AreEqual("1", result.Items[1].Text, "Dijkstra algorithm failed.");
            Assert.AreEqual("2", result.Items[2].Text, "Dijkstra algorithm failed.");
            Assert.AreEqual("3", result.Items[3].Text, "Dijkstra algorithm failed.");
            Assert.AreEqual("4", result.Items[4].Text, "Dijkstra algorithm failed.");
            Assert.AreEqual("5", result.Items[5].Text, "Dijkstra algorithm failed.");

            DijWindow.Close();

            algobox.Click();
            algoForm.Get<ComboBox>(SearchCriteria.ByAutomationId("algorithmComboBox")).Select("HamiltonCycleAlgorithm");
            algoForm.Get<Button>(SearchCriteria.ByText("Run selected alghoritm")).Click();

            Window HamWindow = algoForm.ModalWindow("Hamilton Cycle Algorithm");
            layerbox = HamWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("layerComboBox"));
            // Again if the combobox is not expanded it doesnt have "any" items
            layerbox.Click();

            HamWindow.Get<ComboBox>(SearchCriteria.ByAutomationId("layerComboBox")).Select("Layer 0");
            HamWindow.Get<Button>(SearchCriteria.ByText("Check Hamilton cycle")).Click();

            var resultHam = HamWindow.Get<TextBox>(SearchCriteria.ByAutomationId("outputLabel"));
            Assert.AreEqual("Graph has a Hamilton cycle", resultHam.Text, "Hamilton cycle check failed.");

            HamWindow.Close();

            algoForm.Close();

            window.Get<Button>(SearchCriteria.ByText("Graph")).Click();
            window.Get<Menu>(SearchCriteria.ByText("Adjacency matrix")).Click();

            Window matrixForm = window.ModalWindow("Adjacency Matrix");
            var resultMatrix = matrixForm.Get<TextBox>(SearchCriteria.ByAutomationId("matrixDisplay"));
            Assert.AreEqual("0, 1, 0, 0, 0, 0, \r\n0, 0, 1, 0, 0, 0, \r\n0, 0, 0, 1, 0, 0, \r\n0, 0, 0, 0, 1, 0, \r\n0, 0, 0, 0, 0, 1, \r\n1, 0, 0, 0, 0, 0, \r\n", resultMatrix.Text, "Adjacency matrix failed.");
            matrixForm.Close();

            window.Get<Button>(SearchCriteria.ByText("Graph")).Click();
            window.Get<Menu>(SearchCriteria.ByText("Incidence matrix")).Click();

            matrixForm = window.ModalWindow("Incidence Matrix");
            resultMatrix = matrixForm.Get<TextBox>(SearchCriteria.ByAutomationId("matrixDisplay"));
            Assert.AreEqual(" 1,  0,  0,  0,  0,  -1, \r\n -1,  1,  0,  0,  0,  0, \r\n 0,  -1,  1,  0,  0,  0, \r\n 0,  0,  -1,  1,  0,  0, \r\n 0,  0,  0,  -1,  1,  0, \r\n 0,  0,  0,  0,  -1,  1, \r\n", resultMatrix.Text, "Incidence matrix failed.");
            matrixForm.Close();

            window.Get<Button>(SearchCriteria.ByText("Graph")).Click();
            window.Get<Menu>(SearchCriteria.ByText("Distance matrix")).Click();

            matrixForm = window.ModalWindow("Distance Matrix");
            resultMatrix = matrixForm.Get<TextBox>(SearchCriteria.ByAutomationId("matrixDisplay"));
            Assert.AreEqual("   0,    1,    2,    3,    4,    5, \r\n   5,    0,    1,    2,    3,    4, \r\n   4,    5,    0,    1,    2,    3, \r\n   3,    4,    5,    0,    1,    2, \r\n   2,    3,    4,    5,    0,    1, \r\n   1,    2,    3,    4,    5,    0, \r\n", resultMatrix.Text, "Distance matrix failed.");
            matrixForm.Close();

            // Save and close
            window.Get<Button>(SearchCriteria.ByText("Graph")).Click();
            window.Get<Menu>(SearchCriteria.ByText("Save")).Click();

            Window saveForm = window.ModalWindow("Save a Multigraph File");
            // Generate random file name
            char[] stringChars = new char[10];
            for (int i = 0; i < 10; i++)
            {
                stringChars[i] = (char)('a' + new Random().Next(0, 26));
            }
            string randomString = new String(stringChars) + ".mg";
        
            CultureInfo ci = CultureInfo.InstalledUICulture;
            var lang = ci.Name.Substring(0, 2);
            string fn = "Nazwa pliku:";
            string sd = "Zapisz";
            if (lang == "en")
            {
                fn = "File name:";
                sd = "Save";
            }

            // This will work in windows 11 with polish language
            saveForm.Get<TextBox>(SearchCriteria.ByText(fn)).Text = randomString;
            saveForm.Get<Button>(SearchCriteria.ByText(sd)).Click();

            app.Close();
            // Get Main Window
            app = Application.Launch("MultigraphEditor.exe");
            window = app.GetWindow("Multigraph Editor");

            // Load file
            window.Get<Button>(SearchCriteria.ByText("Graph")).Click();
            window.Get<Menu>(SearchCriteria.ByText("Open")).Click();

            Window openForm = window.ModalWindow("Open a Multigraph File");
            openForm.Get<TextBox>(SearchCriteria.ByText(fn)).Text = randomString;
            openForm.Keyboard.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);
            openForm.Keyboard.PressSpecialKey(TestStack.White.WindowsAPI.KeyboardInput.SpecialKeys.RETURN);

            // Check existance of nodes and edges

            Panel np = window.Get<Panel>(SearchCriteria.ByAutomationId("canvas"));

            panelLocation = np.Bounds.Location;
            // node 1
            window.Mouse.Location = new Point(panelLocation.X + node1.X, panelLocation.Y + node1.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 1 was not opened.");
            }

            // node 2
            window.Mouse.Location = new Point(panelLocation.X + node2.X, panelLocation.Y + node2.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 2 was not opened.");
            }

            // node 3
            window.Mouse.Location = new Point(panelLocation.X + node3.X, panelLocation.Y + node3.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 3 was not opened.");
            }

            // node 4
            window.Mouse.Location = new Point(panelLocation.X + node4.X, panelLocation.Y + node4.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 4 was not opened.");
            }

            // node 5
            window.Mouse.Location = new Point(panelLocation.X + node5.X, panelLocation.Y + node5.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 5 was not opened.");
            }

            // node 6
            window.Mouse.Location = new Point(panelLocation.X + node6.X, panelLocation.Y + node6.Y);
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for node 6 was not opened.");
            }

            // Check edges
            // edge 1 to 2
            window.Mouse.Location = new Point(panelLocation.X + ((node1.X + node2.X) / 2), panelLocation.Y + ((node1.Y + node2.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 1 to 2 was not opened.");
            }

            // edge 2 to 3
            window.Mouse.Location = new Point(panelLocation.X + ((node2.X + node3.X) / 2), panelLocation.Y + ((node2.Y + node3.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 2 to 3 was not opened.");
            }

            // edge 3 to 4
            window.Mouse.Location = new Point(panelLocation.X + ((node3.X + node4.X) / 2), panelLocation.Y + ((node3.Y + node4.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 3 to 4 was not opened.");
            }

            // edge 4 to 5
            window.Mouse.Location = new Point(panelLocation.X + ((node4.X + node5.X) / 2), panelLocation.Y + ((node4.Y + node5.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 4 to 5 was not opened.");
            }

            // edge 5 to 6
            window.Mouse.Location = new Point(panelLocation.X + ((node5.X + node6.X) / 2), panelLocation.Y + ((node5.Y + node6.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 5 to 6 was not opened.");
            }

            // edge 6 to 1
            window.Mouse.Location = new Point(panelLocation.X + ((node6.X + node1.X) / 2), panelLocation.Y + ((node6.Y + node1.Y) / 2));
            window.Mouse.Click(TestStack.White.InputDevices.MouseButton.Right);

            if (FindModalWindow(window, "Edit Form"))
            {
                window.ModalWindow("Edit Form").Get<Button>(SearchCriteria.ByText("Ok")).Click();
            }
            else
            {
                Assert.Fail("Edit Form for edge 6 to 1 was not opened.");
            }

            app.Close();
        }

        public static bool FindModalWindow(Window window, string title)
        {
            try
            {
                window.ModalWindow(title);
                return true;
            }
            catch (AutomationException)
            {
                return false; // Return null if the window is not found
            }
        }

    }
}
