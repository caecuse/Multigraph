using System.ComponentModel;

namespace MultigraphEditor.Src.design
{
    [ToolboxItem(true)]
    [Designer(typeof(DoubleBufferedPanelDesigner))]
    public class DoubleBufferedPanel : Panel
    {
        public DoubleBufferedPanel()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
        }
    }
}
