using System;
using System.Globalization;
using System.Net.WebSockets;

namespace MusicReco
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello! Welcome to MusicReco App!");
            MenuActionService menuActionService = new MenuActionService();
            menuActionService = Initialize(menuActionService);
            SongService songService = new SongService();
            songService.CreateDatabase();
            Helpers helper = new Helpers();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\r\nWhat do you want to do?");
                var mainMenu = menuActionService.GetMenuActionsByMenuName("Main");
                foreach (var menuAction in mainMenu)
                {
                    Console.WriteLine($"{menuAction.Id}. {menuAction.ActionName}");
                }
                Console.Write("Enter the number: ");
                var operation = Console.ReadKey();

                switch (operation.KeyChar)
                {
                    case '1':
                        char genreId = songService.AddNewSongView(menuActionService).KeyChar;
                        var songId = songService.AddNewSong(genreId);
                        break;
                    case '2':
                        char chosenReco = songService.RecommendView(menuActionService).KeyChar;
                        helper.ChooseOfRecommendation(chosenReco, songService);
                        break;
                    case '3':
                        var likedSongId = songService.LikeChosenSong();
                        break;
                    case '4':
                        songService.ShowDetails();
                        break;
                    case '5':
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Such action doesn't exist.");
                        break;
                }
            }
        }
        private static MenuActionService Initialize(MenuActionService menuActionService)
        {
            menuActionService.AddNewAction(1,"Add new song to the database.","Main");
            menuActionService.AddNewAction(2,"Recommend me something.","Main");
            menuActionService.AddNewAction(3,"Like a chosen song.","Main");
            menuActionService.AddNewAction(4,"Show more information about a chosen song.","Main");
            menuActionService.AddNewAction(5,"Exit.","Main");

            menuActionService.AddNewAction(1, "Hip-Hop", "AddSongMenu");
            menuActionService.AddNewAction(2, "Electronic", "AddSongMenu");
            menuActionService.AddNewAction(3, "Jazz", "AddSongMenu");
            menuActionService.AddNewAction(4, "Pop", "AddSongMenu");
            menuActionService.AddNewAction(5, "Rock", "AddSongMenu");
            menuActionService.AddNewAction(6, "Classical", "AddSongMenu");

            menuActionService.AddNewAction(1, "Based on an artist", "RecommendMenu");
            menuActionService.AddNewAction(2, "Based on a genre", "RecommendMenu");
            menuActionService.AddNewAction(3, "Based on a year of release", "RecommendMenu");

            return menuActionService;
        }       
    }
}
