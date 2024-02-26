using MultigraphEditor.src.layers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.graph
{
    public interface IDrawable
    {
        public bool IsInside(float x, float y);
        public int Identifier { get; set; }
        public int GetIdentifier();
    }
}
