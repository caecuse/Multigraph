using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace MultigraphEditor.Src.design
{
    internal class DoubleBufferedPanelDesigner : ControlDesigner
    {
        public override SelectionRules SelectionRules => base.SelectionRules & ~SelectionRules.AllSizeable;
    }
}
