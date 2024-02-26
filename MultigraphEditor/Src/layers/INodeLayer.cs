using MultigraphEditor.src.graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultigraphEditor.src.layers
{
    public interface INodeLayer : ILayer
    {
        public List<INodeDrawable> nodes { get; set; }
    }
}
