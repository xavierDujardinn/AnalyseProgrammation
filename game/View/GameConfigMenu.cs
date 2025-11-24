using game.Controller;
using game.GamePackage;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game.View
{
    internal class GameConfigMenu
    {
        private MainMenu mainMenu;
        private GameMenu gameMenu;
        private GameController gameController;

        // Set default tabSize to default size
        private int tabSize = Tab.DEFAULT_Size; 

        public GameConfigMenu(MainMenu mainMenu, GameMenu gameMenu)
        {
            this.mainMenu = mainMenu;
            this.gameMenu = gameMenu;
            gameController = this.gameMenu.GameController;
        }

        public void ResetConfigValues()
        {
            // This function is called when the user goes back to the main menu,
            // don't forget to add the next config parameters.
            tabSize = Tab.DEFAULT_Size;
        }

        public void EditSizeParameter()
        {
            tabSize = AnsiConsole.Prompt(
                new TextPrompt<int>($"Taille du plateau (min 4), actuellement ({tabSize}) :")
                .Validate(n => n >= 4 ? ValidationResult.Success() : ValidationResult.Error("[red]La taille doit être ≥ 4[/]"))
            );
            ShowGameConfigMenu();
        }

        public void ShowGameConfigMenu()
        {
            var configActions = new Dictionary<string, Action>
            {
                { $"Éditer la taille du plateau : {tabSize}", EditSizeParameter },
                { "Continuer avec ces paramètres", () => 
                    {
                        AnsiConsole.MarkupLine($"[yellow]Lancement d'une partie avec un tableau de taille {tabSize}[/]");
                        gameController.CreateGame(tabSize);
                    } 
                },
                { "Retourner au menu", () => 
                    {
                        ResetConfigValues();
                        mainMenu.ShowMainMenu();
                    }
                }
            };

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Édition des paramètres")
                    .PageSize(5)
                    .AddChoices(configActions.Keys)
            );

            configActions[choice].Invoke();
        }
    }
}
