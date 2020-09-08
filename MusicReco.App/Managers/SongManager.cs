using MusicReco.App.Abstract;
using MusicReco.App.Concrete;
using MusicReco.App.HelpersForManagers;
using MusicReco.Domain.Entity;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MusicReco.App.Managers
{
    public class SongManager
    {   
        private readonly MenuView _menuView;
        private ISongService _songService;
        private Recommendation _recommendation;
        public SongManager(MenuView menuView, ISongService songService)
        {
            _songService = songService;
            _menuView = menuView;
            _recommendation = new Recommendation();
        }

        public Song CreateNewSong()
        {
            _menuView.ShowAddSongMenu();          
            Console.Write("Enter the number: ");
            var chosenGenre = Console.ReadKey(true);
            if (chosenGenre.Key == ConsoleKey.Escape)
            {
                Console.Clear();
                return null;
            }

            char chosenGenreChar = chosenGenre.KeyChar;
            Int32.TryParse(chosenGenreChar.ToString(), out int chosenGenreId);
            
            if(chosenGenreId<=0 || chosenGenreId>=7)
            {
                Console.WriteLine("\r\nSuch operation doesn't exist. Press any key to return to Main menu...");
                Console.ReadKey();
                return null;
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
            if (description == "")
                description = "No description";
            int lastId = _songService.GetLastId();
            Song song = new Song(lastId + 1,artistName,title,(GenreName)chosenGenreId,yearOfRelease,1,description);

            return song;
        }

        public int AddNewSong(Song newSong)
        {
            if (newSong != null)
            {
                _songService.AddItem(newSong);
                _songService.UpdateFileWithSongs(newSong);
                Console.WriteLine("Successfully added! Press any key to continue...");
                return newSong.Id;               
            }
            else
                return -1;            
        }
       
        public int Recommend()
        {       
            _menuView.ShowRecoMenu();
            var chosenReco = Console.ReadKey(true);
            if (chosenReco.Key == ConsoleKey.Escape)
                return -1;

            int reco;
            Int32.TryParse(chosenReco.KeyChar.ToString(), out reco);
            return reco;
        }

        public bool RecommendationSwitcher(int choice)
        {
            List<Song> songs = _songService.GetAllItems();
            bool again = true;
            switch (choice)
            {
                case 1:                    
                    while(again)
                    {
                        string artistName = _menuView.ShowRecoBasedOnArtistMenu();
                        List<Song> recoSongs = _recommendation.RecoBasedOnArtist(songs, artistName);
                        again = _menuView.ShowResultOfReco(recoSongs, "artist");
                    }
                    break;
                case 2:
                    while (again)
                    {
                        int chosenGenre = _menuView.ShowRecoBasedOnGenreMenu();
                        List<Song> recoSongs = _recommendation.RecoBasedOnGenre(songs, chosenGenre);
                        again = _menuView.ShowResultOfReco(recoSongs, "genre");                       
                    }
                    break;
                case 3:
                    while (again)
                    {
                        int[] fromTill = _menuView.ShowRecoBasedOnYearMenu();
                        List<Song> recoSongs =  _recommendation.RecoBasedOnYear(songs, fromTill);
                        again = _menuView.ShowResultOfReco(recoSongs, "year of release");
                    }
                    break;
                case -1: //return to Main Menu.
                    again = false;
                    break;
                default:
                    Console.WriteLine("\r\nSuch option doesn't exist.");
                    Continue();
                    break;
            }
            return again;
        }
            
        public string ChooseSongToBeLiked()
        {
            _menuView.ShowAvailableSongs(_songService.Items);
            Console.Write("Please enter the title of the song, which you want to like: ");
            string songTitle = Console.ReadLine();
            return songTitle;

        }

        public int LikeChosenSong(string songTitle)
        {
            int songId = _songService.CheckSongExistsInDatabase(songTitle);
            if (songId != -1)
            {
                _songService.LikeSong(songId);
                _songService.SaveLikeToFile(songId);
            }
            return songId;
        }

        public bool SuccessfulOrFailedLikeInfo(int songId)
        {
            if (songId == -1)
                Console.WriteLine("\r\nSuch song doesn't exist.");
           else
                Console.WriteLine("\r\nYou liked it!");

            Console.WriteLine("Press <ESC> to return to the Main menu... or different key to try again!");
            var choice = Console.ReadKey(true);
            if (choice.Key == ConsoleKey.Escape)
                return false;
            return true;
        }

        public string ChooseSongToShowDetails()
        {
            _menuView.ShowAvailableSongs(_songService.Items);
            Console.Write("Please enter the title of the song to watch more details: ");
            string title = Console.ReadLine();
            return title;
        }

        public Song SearchSongToShowDetails(string songTitle)
        {
            int songId = _songService.CheckSongExistsInDatabase(songTitle);
            if (songId != -1)
            {
                Song song = _songService.GetSongById(songId);
                return song;               
            }
            else
                return null;           
        }

        public bool ShowDetails(Song song)
        {
            if(song != null)
            {
                Console.Clear();
                Console.WriteLine($"Song id: {song.Id}\r\nArtist: {song.Artist}\r\nTitle: {song.Title}\r\n" +
                $"Year of release: {song.YearOfRelease}\r\nGenre: {song.Genre}\r\nLikes: {song.Likes}\r\nDescription: {song.Description}");
                Console.WriteLine("\r\nPress <ESC> to return to the Main menu... or different key to search next song!");
            }
            else
            {
                Console.WriteLine("Such title was not found.");
                Console.WriteLine("\r\nPress <ESC> to return to the Main menu... or different key to try again!");
            }
            var option = Console.ReadKey(true);
            if (option.Key == ConsoleKey.Escape)
                return false;
            return true;
        }

        private static void Continue()
        {
            Console.WriteLine("\r\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }           
    }
}
