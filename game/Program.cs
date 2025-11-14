// See https://aka.ms/new-console-template for more information
using game.Locale;
using game.View;

Console.WriteLine("Starting the game...");

// Le language par défaut est le français
LocaleController.LoadLocale(LocaleEnum.FR);

// On démarre le jeu
MainMenu mainMenu = new MainMenu();
mainMenu.ShowMainMenu();