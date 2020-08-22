using MusicReco.App.Concrete;
using MusicReco.Domain.Entity;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.App.Managers
{
    public class SongManager
    {   
        private readonly MenuActionService _menuActionService;
        private SongService _songService;
        public SongManager(MenuActionService menuActionService, SongService songService)
        {
            _songService = songService;
            _menuActionService = menuActionService;
        }
        private static void Continue()
        {
            Console.WriteLine("\r\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
        public int AddNewSong()
        {
            var addSongMenu = _menuActionService.GetMenuActionsByMenuName("AddSongMenu");
            Console.WriteLine("Please select genre of your song:");
            foreach (var menuAction in addSongMenu)
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.ActionName}");
            }
            Console.WriteLine("Press <ESC> to return to the Main menu...");
           
            Console.Write("Enter the number: ");
            var chosenGenre = Console.ReadKey(true);
            if (chosenGenre.Key==ConsoleKey.Escape)
            {
                Console.Clear();
                return -1;
            }

            char chosenGenreChar = chosenGenre.KeyChar;
            Int32.TryParse(chosenGenreChar.ToString(), out int chosenGenreId);
            
            if(chosenGenreId<=0 || chosenGenreId>=7)
            {
                Console.WriteLine("\r\nSuch operation doesn't exist. Press any key to return to Main menu...");
                Console.ReadKey();
                return -1;
            }
            Console.Clear();
            Console.WriteLine($"Chosen genre is: {(GenreName)chosenGenreId}");
            Console.Write("\r\nPlease enter artist name: ");
            string artistName = Console.ReadLine();
            Console.Write("Please enter title of the song: ");
            string title = Console.ReadLine();
            Console.Write("Please enter year of release: ");
             Int32.TryParse(Console.ReadLine(), out int yearOfRelease);
            Console.WriteLine("If you want, write short description of the song. If not, press enter... ");
            string description = Console.ReadLine();

            int lastId = _songService.GetLastId();
            Song song = new Song(lastId + 1,artistName,title,(GenreName)chosenGenreId,yearOfRelease,1,description);

            _songService.AddItem(song);
            Console.WriteLine("Successfully added! Press any key to continue...");
            Console.ReadKey();
            return song.Id;
        }
        private void ShowRecoMenu(List<MenuAction> recommendMenu)
        {
            Console.WriteLine("What recommendation do you need?");
            for (int i = 0; i < recommendMenu.Count; i++)
            {
                Console.WriteLine($"{recommendMenu[i].Id}. {recommendMenu[i].ActionName}");
            }
            Console.WriteLine("Press <ESC> to return to the Main menu...");
            Console.Write("Enter chosen number: ");
        }

        private void NoRecommendedSongs()
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
            recoSongs.Clear();
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
        private void RecoBasedOnArtist()
        {
            List<Song> recoSongs = new List<Song>();
            bool running = true;
            do
            {
                Console.Clear();
                Console.Write("Please enter the name of artist: ");
                string artistName = Console.ReadLine();

                //Search songs created by chosen artist and add to recoSongs
                foreach (var song in _songService.Items)
                {
                    if (song.Artist.Equals(artistName, StringComparison.OrdinalIgnoreCase))
                    {
                        recoSongs.Add(song);
                    }
                }
                if (recoSongs.Count == 0)
                {
                    NoRecommendedSongs();
                    running = Escape();
                }
                else
                {
                    ShowRecommendedSongs(recoSongs, "artist");
                    running = Escape();
                }
            } while (running);
        }
        private void RecoBasedOnGenre()
        {
            List<Song> recoSongs = new List<Song>();
            bool running = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Please select a genre:");
                for (int i = 1; i < 7; i++)
                {
                    Console.WriteLine($"{i}. {(GenreName)i}");
                }
                Console.Write("Enter the number: ");
                Int32.TryParse(Console.ReadLine(), out int chosenGenre);

                foreach (var song in _songService.Items)
                {
                    if ((int)song.Genre == chosenGenre)
                    {
                        recoSongs.Add(song);
                    }
                }
                if (recoSongs.Count == 0)
                {
                    NoRecommendedSongs();
                    running = Escape();
                }
                else
                {
                    ShowRecommendedSongs(recoSongs, "genre");
                    running = Escape();
                }
            } while (running);
        }
        private void RecoBasedOnYear()
        {
            List<Song> recoSongs = new List<Song>();
            bool running = true;
            do
            {
                Console.Clear();
                Console.Write("Please enter time interval, from: ");
                int from;
                Int32.TryParse(Console.ReadLine(), out from);
                Console.Write("till: ");
                int till;
                Int32.TryParse(Console.ReadLine(), out till);

                foreach (var song in _songService.Items)
                {
                    if (song.YearOfRelease >= from && song.YearOfRelease <= till)
                    {
                        recoSongs.Add(song);
                    }
                }
                if (recoSongs.Count == 0)
                {
                    NoRecommendedSongs();
                    running = Escape();
                }
                else
                {
                    ShowRecommendedSongs(recoSongs, "year of release");
                    running = Escape();
                }
            } while (running);
        }
        public void Recommend()
        {
            var recommendMenu = _menuActionService.GetMenuActionsByMenuName("RecommendMenu");
            while (true)
            {
                Console.Clear();
                ShowRecoMenu(recommendMenu);
                //If ESC, return to Main menu.
                var chosenReco = Console.ReadKey(true);
                if (chosenReco.Key == ConsoleKey.Escape)
                    return;

                char reco = chosenReco.KeyChar;
                switch(reco)
                {
                    case '1':
                        RecoBasedOnArtist();
                        break;
                    case '2':
                        RecoBasedOnGenre();
                        break;
                    case '3':
                        RecoBasedOnYear();
                        break;
                    default:
                        Console.WriteLine("\r\nSuch option doesn't exist.");
                        Continue();
                        break;
                }
            }
        }
        public int LikeChosenSong()
        {
            while (true)
            {
                Console.Write("Please enter the title of the song: ");
                string songToBeLiked = Console.ReadLine();
                int songId = -1;

                foreach (var song in _songService.Items)
                {
                    if (song.Title.Equals(songToBeLiked, StringComparison.OrdinalIgnoreCase))
                    {
                        song.Likes++;
                        songId = song.Id;
                        break;
                    }
                }
                if (songId == -1)
                {
                    Console.WriteLine("\r\nSuch song doesn't exist.");
                    Console.WriteLine("Press <ESC> to return to the Main menu... or different key to try again!");
                    var choice = Console.ReadKey(true);
                    if (choice.Key == ConsoleKey.Escape)
                        return -1;
                    else
                        Console.Clear();
                }
                else
                {
                    Console.WriteLine("\r\nYou liked it!");
                    Continue();
                    return songId;
                }
            }
        }

        public void ShowDetails()
        {

            while (true)
            {
                Console.Clear();
                Console.Write("Please enter the title of the song: ");
                string title = Console.ReadLine();
                bool success = false;
                foreach (var song in _songService.Items)
                {
                    if (song.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                    {
                        success = true;
                        if (song.Description == "")
                            song.Description = "No description";

                        Console.WriteLine($"\r\nSong id: {song.Id}\r\nArtist: {song.Artist}\r\nTitle: {song.Title}\r\n" +
                            $"Year of release: {song.YearOfRelease}\r\nGenre: {song.Genre}\r\nLikes: {song.Likes}\r\nDescription: {song.Description}");
                        Console.WriteLine("\r\nPress <ESC> to return to the Main menu... or different key to search next song!");
                        break;
                    }
                }
                if (!success)
                {
                    Console.WriteLine("Such title was not found.");
                    Console.WriteLine("\r\nPress <ESC> to return to the Main menu... or different key to try again!");
                }
                
                var option = Console.ReadKey(true);
                if (option.Key == ConsoleKey.Escape)
                    return;
            }
        }
    }
}
