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
        private int tabSize = Tab.DEFAULT_SIZE; 

        public GameConfigMenu(MainMenu mainMenu, GameMenu gameMenu)
        {
            this.mainMenu = mainMenu;
            this.gameMenu = gameMenu;
            gameController = this.gameMenu.GameController;
        }
        List<string> playerList = new();
        public void ResetConfigValues()
        {
            // This function is called when the user goes back to the main menu,
            // don't forget to add the next config parameters.
            tabSize = Tab.DEFAULT_SIZE;
        }
        public void AddPlayer()
        {
            int ctr = AnsiConsole.Prompt(new TextPrompt<int>("Combien de joueurs voulez vous ajouter ? :"));
            int cnt = 0;
            while (cnt <= ctr)
            {
                string player = AnsiConsole.Prompt(
                new TextPrompt<string>($"Ajouter un joueur, actuellement ({playerList.Count}) :"));
                playerList.Add(player);
                AnsiConsole.MarkupLine(playerList.Last());
                cnt ++;
            }
            ShowGameConfigMenu();
        }

        public void DelPlayer() 
        {
            AnsiConsole.MarkupLine("Joueurs actuels :");
            foreach(string i in playerList)
            {
                AnsiConsole.MarkupLine(i);
            }
            string player = AnsiConsole.Prompt(
            new TextPrompt<string>("Supprimer un joueur :"));
            playerList.Remove(player);
            string option = AnsiConsole.Prompt(new TextPrompt<string>("Voulez vous supprimez un autre joueur ? (oui/non)"));
            if(option == "oui")
            {
                DelPlayer();
            }
            else if(option == "non")
            {
                ShowGameConfigMenu();
            }
            
        }
        public void AddPlayer()
        {
            int ctr = AnsiConsole.Prompt(new TextPrompt<int>("Combien de joueurs voulez vous ajouter ? :"));
            int cnt = 0;
            while (cnt <= ctr)
            {
                string player = AnsiConsole.Prompt(
                new TextPrompt<string>($"Ajouter un joueur, actuellement ({playerList.Count}) :"));
                playerList.Add(player);
                Console.WriteLine(playerList.Last());
                cnt += 1;
            }
            ShowGameConfigMenu();
        }

        public void DelPlayer() 
        {
            Console.WriteLine("Joueurs actuels :");
            foreach(string i in playerList)
            {
                Console.WriteLine(i);
            }
            string player = AnsiConsole.Prompt(
            new TextPrompt<string>("Supprimer un joueur :"));
            playerList.Remove(player);
            string option = AnsiConsole.Prompt(new TextPrompt<string>("Voulez vous supprimez un autre joueur ? (oui/non)"));
            if(option == "oui")
            {
                DelPlayer();
            }
            else if(option == "non")
            {
                ShowGameConfigMenu();
            }
            
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
                { "Ajouter des joueurs", AddPlayer },
                { "Supprimer des joueurs", DelPlayer },
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
