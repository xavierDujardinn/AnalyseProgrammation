// See https://aka.ms/new-console-template for more information
using game.Locale;
using game.View;
using System.Text;

Console.WriteLine("Starting the game...");

// Le language par défaut est le français
LocaleController.LoadLocale(LocaleEnum.FR);

// On fixe en UTF8 pour avoir les carractères spéciaux
Console.OutputEncoding = Encoding.UTF8;

// On démarre le jeu
MainMenu mainMenu = new MainMenu();
mainMenu.ShowMainMenu();