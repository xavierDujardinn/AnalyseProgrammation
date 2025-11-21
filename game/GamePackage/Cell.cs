using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage
{
    internal class Cell
    {
        public CellContent Content { get; set; }
        public Cell(CellContent content)
        {
            Content = content;
        }
        public Cell() : this(new EmptyCell())
        {
        }

        public override string ToString()
        { 
            return Content.Icon.ToString();
        }
    }
}
