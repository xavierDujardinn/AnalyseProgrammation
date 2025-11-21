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
        private GameController gameController;

        public GameMenu(MainMenu mainMenu)
        {
            this.mainMenu = mainMenu;
            gameController = new GameController();
            gameController.OnNewRoundStarted += DisplayRoundMenu;
        }

        private void DisplayRoundMenu()
        {
            AnsiConsole.MarkupLine($"[blue]Tour n° {gameController.CurrentRoundNumber}[/]");

            var roundActions = new Dictionary<string, Action>
            {
                { "Continuer", () => {  } },
                { "Afficher le tableau", ShowGameTab},
                { "Stop", () =>
                    {
                        gameController.EndGame();
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
        public void CreateGame()
        {
            AnsiConsole.MarkupLine("[yellow]Starting a new game...[/]");
            gameController.CreateGame();
        }

        public void ShowGameTab()
        {
            Tab tab = gameController.GameTab;
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
