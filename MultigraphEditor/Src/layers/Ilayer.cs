using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.layers
{
    public interface ILayer
    {
        Font Font { get; set; }
        Color Color { get; set; }
        int Width { get; set; }
        bool Active { get; set; }
        int Identifier { get; set; }
        String Name { get; set; }
        void changeActive();
    }
}
