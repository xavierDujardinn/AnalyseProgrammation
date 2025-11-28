using game.GamePackage.Buildings;
using game.GamePackage.Ressources;
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
        public int Size { get; private set; }

        // Two dimension tab
        public Cell[,] Cells { get; private set; }

        public Tab(int size)
        {
            if (size > 4)
            {
                Size = size;
            }
            else
            {
                Size = DEFAULT_SIZE;
            }
            Cells = new Cell[Size, Size];
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

        #region building methods

        public bool PlaceBuilding(int x, int y, BaseBuilding building)
        {
            if (x < 0 || x >= Size || y <0 || y >= Size)
            {  
                return false;
            }

            if (Cells[x, y].Content is EmptyCell)
            {
                Cells[x, y].Content = building;
                return true;
            }

            return false;
        }

        public void RemoveBuilding(int x, int y)
        {
            if (x>= 0 && x < Size && y >= 0 && y < Size)
            {
                Cells[x, y].Content = new EmptyCell();
            }

        }
        #endregion

        public CellContent? GetCellContent(int x, int y)
        {
            if (x >= 0 && x < Size && y >= 0 && y < Size)
            {
                return Cells[x, y].Content;
            }
            return null;
        }

        public Dictionary<string, int> GetAllRessourcesOnTab()
        {
            Dictionary<string, int> ressources = new Dictionary<string, int>();

            foreach (Cell cell in Cells) 
            { 
                if (cell.Content is BaseBuilding building)
                {
                    if (ressources.ContainsKey(building.Ressource.Name))
                    {
                        ressources[building.Ressource.Name] += building.RessourceQuantity;
                    }
                    else
                    {
                        ressources.Add(building.Ressource.Name, building.RessourceQuantity);
                    }
                }
            }

            return ressources;
        }
    }
}