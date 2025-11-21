using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage
{
    internal abstract class CellContent
    {
        // Default value for debugging is '?'
        public virtual char Icon { get; internal set; } = '?';
    }
}
