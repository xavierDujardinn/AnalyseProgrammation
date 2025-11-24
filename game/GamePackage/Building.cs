using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage
{
    internal abstract class Building : CellContent
    {
        public string Name { get; protected set; }

        protected Building(string name, char icon)
        {
            Name = name;
            Icon = icon;
        }

        public override string ToString()
        {
            return Icon.ToString();
        }
    }
}
