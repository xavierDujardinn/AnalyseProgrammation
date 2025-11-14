using game.Locale;
using Spectre.Console;
using System;

namespace game.View
{
    internal class MainMenu
    {
        public MainMenu()
        {
            AnsiConsole.MarkupLine("[green]Welcome to the game ![/]");
        }

        public void ShowMainMenu()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("What do you want to do?")
                    .PageSize(10)
                    .AddChoices(new[] {
                        "Start game",
                        "Load game",
                        "Settings",
                        "Exit"
                    }));

            switch (choice)
            {
                case "Start game":
                    AnsiConsole.MarkupLine("[yellow]Starting a new game...[/]");
                    break;

                case "Load game":
                    AnsiConsole.MarkupLine("[yellow]Loading saved game...[/]");
                    break;

                case "Settings":
                    ShowSettingsMenu();
                    break;

                case "Exit":
                    AnsiConsole.MarkupLine("[red]Goodbye ![/]");
                    Environment.Exit(0);
                    break;
            }
        }

        private void ShowLanguageSettings()
        {
            AnsiConsole.Clear();
            AnsiConsole.MarkupLine("[yellow]" + LocaleController.GetLocaleLabel("greeting") + "[/]");

            var setting = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Language settings")
                    .AddChoices("Français", "English", "Back"));

            if (setting == "Back")
            {
                ShowSettingsMenu();
            }
            else if (setting == "Français")
            {
                LocaleController.LoadLocale(LocaleEnum.FR);
                ShowLanguageSettings();
            }
            else if (setting == "English")
            {
                LocaleController.LoadLocale(LocaleEnum.EN);
                ShowLanguageSettings();
            }
        }

        private void ShowSettingsMenu()
        {
            var setting = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Settings")
                    .AddChoices("Language", "Back"));

            if (setting == "Back")
                ShowMainMenu();
            else if (setting == "Language")
            {
                ShowLanguageSettings();
            }
            else
                AnsiConsole.MarkupLine($"[blue]{setting} settings coming soon![/]");
        }
    }
}
