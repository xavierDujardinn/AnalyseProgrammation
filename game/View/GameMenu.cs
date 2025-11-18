using game.Controller;
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

        private void DisplayRoundMenu(int roundNumber)
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine($"[blue]Début du tour n° {roundNumber}[/]");

            var roundActions = new Dictionary<string, Action>
            {
                { "Continuer", () => {  } },
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
        public void StartGame()
        {
            AnsiConsole.MarkupLine("[yellow]Starting a new game...[/]");
            gameController.StartGame();
        }
    }
}
