using Spectre.Console;

namespace game.View
{
    public class SplashScreen
    {
        private const string asciiArt = @"                                            
         :                  : ::  :::                       
       :::             :      :  :::::                      
     :::   :       :             ::::                       
  ::::::  :        :   :        ::::                        
     :::::    :::: :  ::::                                  
       ::    ::::::::::::                                   
                :: ::  :    ::::                            
           ::      :: :     :::::                           
            ::::::::::::::::::::::::::                      
             ::::::::::::::::::::::::::::::                 
              ::::::::::::::::::::::::::::::::::            
               :::::=::::::::::::::::::::::::::::::         
               :::     -:::::::::::::::::::::::::::::       
               :::     ::::::::::::::::::::::::::::::::     
              :::::: :::::::::::::::::::::::::::::::::::    
              :::::::::::::::::::::::::::::::::::::::::::   
              :::::::::::::::::::::::::::::::::::::::::::   
              ::::::::::::::::::::::::::::::::::::::::::::  
              ::::::::::::::::::::::::::::::::::::::::::::  
             :::::::::::::::::::::::::::::::::     :::::::  
             ::::::::::::::::::::::::::::::::     ::::::::  
            ::::::::   ::::::::::::::::::::::    :::::::::  
            :::::::       :::::::::::::::     ::::::::::::  
            ::::::            :::::::::::::::      :::::::  
           ::::::               ::::::::::::::      :::::   
           :::::                ::::::::   :::::    :::::   
          :::::                 :::::::     :::: :::::::    
        ::::::                   :::::      ::  :::::::     
  :::::::::::                                               
  ::::::::::";

        private const string gameNameStart =@"
                ==========================================================
                                Limping Dogs Studios            
                              DEMARRAGE DE SOUTH GUARD
                ==========================================================";

        public void Show()
        {
            Console.Clear();
            Console.WriteLine(asciiArt);

            AnsiConsole.MarkupLine("[red]" + gameNameStart + "[/]");

            int duration = 5000;
            int interval = 500;
            int elapsed = 0;
            bool visible = true;

            while (elapsed < duration)
            {
                if (visible)
                    AnsiConsole.MarkupLine("[yellow]Chargement du jeu...[/]");
                else
                    Console.WriteLine("                       ");

                Thread.Sleep(interval);
                elapsed += interval;
                visible = !visible;

                Console.SetCursorPosition(0, Console.CursorTop - 1);
            }
            Console.WriteLine("                       ");
        }
    }
}
