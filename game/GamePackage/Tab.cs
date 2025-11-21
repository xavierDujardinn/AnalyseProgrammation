using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.GamePackage
{
    internal class Tab
    {
        // For now, the tab should always be a square, we might want to edit this later
        public static readonly int DEFAULT_SIZE = 16; 
        private int size;

        // Two dimension tab
        public Cell[,] Cells { get; private set; }

        public Tab(int size)
        {
            if (size > 4)
            {
                this.size = size;
            }
            else
            {
                this.size = DEFAULT_SIZE;
            }
            Cells = new Cell[this.size, this.size];
            GenerateTab();
        }
        public Tab() : this(DEFAULT_SIZE)
        {
        }

        public void GenerateTab()
        {
            // Si on veut de la génération aléatoire il faudra l'implémenter par ici
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    Cells[i,j] = new Cell();
                }
            }
        }
    }
}