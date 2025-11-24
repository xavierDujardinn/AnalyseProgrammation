using game.Controller;
using game.GamePackage;
using Spectre.Console;
using System;
using System.Collections.Generic;

namespace game.View
{
    internal class GameMenu
    {
        private MainMenu mainMenu;
        public GameController GameController { get; private set; }

        public GameMenu(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
            GameController = new GameController();
            GameController.OnNewRoundStarted += DisplayRoundMenu;
        }

        private void DisplayRoundMenu()
        {
            AnsiConsole.MarkupLine($"[blue]Tour n° {GameController.CurrentRoundNumber}[/]");

            var roundActions = new Dictionary<string, Action>
            {
                { "Continuer", () => {  } },
                { "Afficher le tableau", ShowGameTab},
                { "Placer un bâtiment", PlaceBuilding },
                { "Supprimer un bâtiment", RemoveBuilding },
                { "Stop", () =>
                    {
                        GameController.EndGame();
                        AnsiConsole.MarkupLine("[red]Le jeu est terminé. Retour au menu principal.[/]");
                        mainMenu.ShowMainMenu();
                    }
                }
            };

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Que voulez-vous faire ?")
                    .PageSize(5)
                    .AddChoices(roundActions.Keys)
            );

            roundActions[choice].Invoke();
        }
        private void PlaceBuilding()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]Placer un bâtiment (#)[/]\n");

            int maxCoord = GameController.GameTab.Size - 1;

            int x = AnsiConsole.Prompt(
                new TextPrompt<int>($"Entrez la coordonnée X (0 - {maxCoord}):")
                    .Validate(n => n>= 0 && n <= maxCoord)
                    .ValidationErrorMessage($"[red]Veuillez entrer un nombre valide entre 0 et {maxCoord}.[/]")
            );

            int y = AnsiConsole.Prompt(
                new TextPrompt<int>($"Entrez la coordonnée Y (0 - {maxCoord}):")
                    .Validate(n => n >= 0 && n <= maxCoord)
                    .ValidationErrorMessage($"[red]Veuillez entrer un nombre valide entre 0 et {maxCoord}.[/]")
            );

            Building building = new BaseBuilding();

            if (GameController.GameTab.PlaceBuilding(x, y, building))
            {
                AnsiConsole.MarkupLine($"[green]Bâtiment placé en ({x}, {y}).[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Impossible de placer le bâtiment en ({x}, {y}). Emplacement occupé.[/]");
            }

            ShowGameTab();
        }

        private void RemoveBuilding()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]Supprimer un bâtiment (X)[/]\n");

            int maxCoord = GameController.GameTab.Size - 1;

            int x = AnsiConsole.Prompt(
                new TextPrompt<int>($"Entrez la coordonnée X (0 - {maxCoord}):")
                    .Validate(n => n >= 0 && n <= maxCoord)
                    .ValidationErrorMessage($"[red]Veuillez entrer un nombre valide entre 0 et {maxCoord}.[/]")
            );

            int y = AnsiConsole.Prompt(
                new TextPrompt<int>($"Entrez la coordonnée Y (0 - {maxCoord}):")
                    .Validate(n => n >= 0 && n <= maxCoord)
                    .ValidationErrorMessage($"[red]Veuillez entrer un nombre valide entre 0 et {maxCoord}.[/]")
            );

            CellContent? content = GameController.GameTab.GetCellContent(x, y);

            if (content is Building)
            {
                GameController.GameTab.RemoveBuilding(x, y);
                AnsiConsole.MarkupLine($"[green]Bâtiment supprimé en ({x}, {y}).[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]Aucun bâtiment à supprimer en ({x}, {y}).[/]");
            }
            ShowGameTab();
        }
        public void ShowGameTab()
        {
            Tab tab = GameController.GameTab;
            Cell[,] cells = tab.Cells;
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                string line = "";

                for (int x = 0; x < cells.GetLength(0); x++)
                {
                    line += $"[yellow]{cells[x, y]}[/] ";
                }

                AnsiConsole.MarkupLine(line);
            }
            DisplayRoundMenu();
        }
    }
}
