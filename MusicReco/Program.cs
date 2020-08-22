using MusicReco.App.Concrete;
using MusicReco.App.Managers;
using System;
using System.Dynamic;
using System.Globalization;
using System.Net.WebSockets;

namespace MusicReco
{
    class Program
    {
        static void Main(string[] args)
        {
            MenuActionService menuActionService = new MenuActionService();
            SongService songService = new SongService();
            SongManager songManager = new SongManager(menuActionService, songService);
            PlaylistManager playlistManager = new PlaylistManager(menuActionService, songService);
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Hello! Welcome to MusicReco App!\r\n");
                Console.WriteLine("What do you want to do?");
                var mainMenu = menuActionService.GetMenuActionsByMenuName("Main");
                foreach (var menuAction in mainMenu)
                {
                    Console.WriteLine($"{menuAction.Id}. {menuAction.ActionName}");
                }
                Console.Write("Enter the number: ");
                var operation = Console.ReadKey();
                Console.WriteLine();

                switch (operation.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        var newSongId = songManager.AddNewSong();
                        break;
                    case '2':
                        Console.Clear();
                        songManager.Recommend();
                        break;
                    case '3':
                        Console.Clear();
                        var likedSongId = songManager.LikeChosenSong();
                        break;
                    case '4':
                        Console.Clear();
                        songManager.ShowDetails();
                        break;
                    case '5':
                        Console.Clear();
                        playlistManager.CreateNewOrAdd();
                        break;
                    case '6':
                        Console.Clear();
                        playlistManager.ShowPlaylists();
                        break;
                    case '7':
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Such action doesn't exist. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
        }              
    }
}
