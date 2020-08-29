using MusicReco.App.Concrete;
using MusicReco.Domain.Entity;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicReco.App.HelpersForManagers
{
    public class MenuView
    {
        private MenuActionService _menuActionService;
        public MenuView()
        {
            _menuActionService = new MenuActionService();
        }
        public void ShowMainMenu()
        {
            Console.WriteLine("Hello! Welcome to MusicReco App!\r\n");
            Console.WriteLine("What do you want to do?");
            var mainMenu = _menuActionService.GetMenuActionsByMenuName("Main");
            foreach (var menuAction in mainMenu)
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.ActionName}");
            }
            Console.Write("Enter the number: ");
        }
        public void ShowAddSongMenu()
        {
            var addSongMenu = _menuActionService.GetMenuActionsByMenuName("AddSongMenu");
            Console.WriteLine("Please select genre of your song:");
            foreach (var menuAction in addSongMenu)
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.ActionName}");
            }
            Console.WriteLine("Press <ESC> to return to the Main menu...");
        }
        public void ShowRecoMenu()
        {
            var recommendMenu = _menuActionService.GetMenuActionsByMenuName("RecommendMenu");
            Console.WriteLine("What recommendation do you need?");
            for (int i = 0; i < recommendMenu.Count; i++)
            {
                Console.WriteLine($"{recommendMenu[i].Id}. {recommendMenu[i].ActionName}");
            }
            Console.WriteLine("Press <ESC> to return to the Main menu...");
            Console.Write("Enter chosen number: ");
        }
        public string ShowRecoBasedOnArtistMenu()
        {
            Console.Clear();
            Console.Write("Please enter the name of artist: ");
            string artistName = Console.ReadLine();
            return artistName;
        }
        public int ShowRecoBasedOnGenreMenu()
        {
            Console.Clear();
            Console.WriteLine("Please select a genre:");
            for (int i = 1; i < 7; i++)
            {
                Console.WriteLine($"{i}. {(GenreName)i}");
            }
            Console.Write("Enter the number: ");
            Int32.TryParse(Console.ReadLine(), out int chosenGenre);
            return chosenGenre;
        }
        public int[] ShowRecoBasedOnYearMenu()
        {
            Console.Clear();
            Console.Write("Please enter time interval, from: ");
            int from;
            Int32.TryParse(Console.ReadLine(), out from);
            Console.Write("till: ");
            int till;
            Int32.TryParse(Console.ReadLine(), out till);
            int[] fromTill = { from,till };
            return fromTill;
        }
        public bool ShowResultOfReco(List<Song> recoSongs, string basedOn)
        {
            bool running = true;
            if (recoSongs.Count == 0)
            {
                ShowNoRecommendedSongs();
                running = Escape();
            }
            else
            {
                ShowRecommendedSongs(recoSongs, basedOn);
                running = Escape();
            }
            return running;
        }
        
        public void ShowAvailableSongs(List<Song> allSongs)
        {
            Console.WriteLine("Available songs in database:");
            Console.WriteLine("\r\nSong id...  Arist... - Title...");
            foreach (var song in allSongs)
            {
                Console.WriteLine($"{song.Id}. {song.Artist} - {song.Title}");
            }
            Console.WriteLine();
        }

        public void ShowPlaylistMenu()
        {
            var playlistMenu = _menuActionService.GetMenuActionsByMenuName("PlaylistMenu");
            Console.WriteLine("Press <ESC> to return to the Main menu...\r\n");
            Console.WriteLine("Please choose one option:");
            foreach (var menuAction in playlistMenu)
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.ActionName}");
            }
            Console.Write("Enter the number: ");
        }

        public void ShowUpdatingMenu(List<Playlist> allPlaylists)
        {
            Console.WriteLine("Press <ESC> to return to the Playlist menu...\r\n");
            Console.WriteLine("Please choose playlist, which you would like to update:");
            foreach (var playlist in allPlaylists)
            {
                Console.WriteLine($"{playlist.Id}. {playlist.Name}");
            }
            Console.Write("Enter playlist Id: ");
        }

        public void ShowPlaylistSongs(IEnumerable<Song> songsAtPlaylist)
        {

            Console.WriteLine("Song id...  Arist... - Title...");
            foreach (var song in songsAtPlaylist)
            {
                Console.WriteLine($"{song.Id}. {song.Artist} - {song.Title}");
            }
            Console.WriteLine("\r\nPress any key to return to the list of your playlists...");
            Console.ReadKey(true);
        }
        private void ShowNoRecommendedSongs()
        {
            Console.WriteLine("There is no song fulfilling these criteria.");
            Console.WriteLine("\r\nPress <ESC> to return to the Menu of recommendation... or different key to try again!");
        }
        private void ShowRecommendedSongs(List<Song> recoSongs, string basedOn)
        {
            Console.WriteLine("\r\nSong id...  Arist... - Title...");
            for (int i = 0; i < recoSongs.Count; i++)
            {
                Console.WriteLine($"{recoSongs[i].Id}.  {recoSongs[i].Artist} - {recoSongs[i].Title}");
            }
            Console.WriteLine($"\r\nPress <ESC> to return to the Menu of recommendation... or different key to get RECO based on {basedOn} again!");
        }
        private bool Escape()
        {
            bool running = true;
            var choice = Console.ReadKey(true);
            if (choice.Key == ConsoleKey.Escape)
                running = false;

            return running;
        }


    }
}
