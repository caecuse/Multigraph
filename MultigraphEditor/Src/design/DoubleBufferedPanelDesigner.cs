using System.Windows.Forms.Design;

namespace MultigraphEditor.src.design
{
    internal class DoubleBufferedPanelDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules => base.SelectionRules & ~SelectionRules.AllSizeable;
    }
}
