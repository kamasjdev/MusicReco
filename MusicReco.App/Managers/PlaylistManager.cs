using MusicReco.App.Abstract;
using MusicReco.App.Concrete;
using MusicReco.App.HelpersForManagers;
using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace MusicReco.App.Managers
{
    public class PlaylistManager
    {
        private readonly MenuView _menuView;
        private PlaylistService _playlistService;
        private ISongService _songService;       
        public PlaylistManager(MenuView menuView, ISongService songService)
        {
            _menuView = menuView;
            _playlistService = new PlaylistService();
            _songService = songService;                     
        }

        public void CreateNewOrAdd()
        {            
            while (true)
            {
                Console.Clear();
                _menuView.ShowPlaylistMenu();

                //If pressed ESC, return to the main menu.
                var chosenOption = Console.ReadKey(true);
                if (chosenOption.Key == ConsoleKey.Escape)
                    break;

                //When keys 1 or 2 are not pressed, return to the main menu or try again. 
                char chosenOptionId = chosenOption.KeyChar;
                if (chosenOptionId != '1' && chosenOptionId != '2')
                {
                    Console.Clear();
                    Console.WriteLine("Such operation doesn't exist. Press <ESC> to return to Main menu...or different key to try again!");
                    var choice = Console.ReadKey(true);
                    if (choice.Key == ConsoleKey.Escape)
                        break;
                    else
                        continue;
                }
                Console.Clear();

                switch(chosenOptionId)
                {
                    case '1':
                        CreateNewPlaylist();
                        break;
                    case '2':
                        AddSongsToPlaylist();
                        break;
                }
            }
        }
        public void ShowPlaylists()
        {
            var allPlaylists = _playlistService.GetAllItems();
            if (allPlaylists.Count == 0)
            {
                Console.WriteLine("There is no playlist. Create something!");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                //Show created playlists.
                Console.WriteLine("Press <ESC> to return to the Main menu...\r\n");
                foreach (var playlist in allPlaylists)
                {
                    Console.WriteLine($"{playlist.Id}. {playlist.Name}");
                }
                Console.Write("Please type playlist Id to show songs: ");
                var chosenPlaylistKey = Console.ReadKey(true);
                //If ESC is pressed, return to the Main menu.
                if (chosenPlaylistKey.Key == ConsoleKey.Escape)
                    return;

                //Chosen playlist doesn't exist, try again.
                int chosenPlaylistId;
                Int32.TryParse(chosenPlaylistKey.KeyChar.ToString(), out chosenPlaylistId);
                if ((chosenPlaylistId == 0) || (chosenPlaylistId > allPlaylists.Count))
                {
                    Console.WriteLine("\r\n\r\nSuch playlist Id doesn't exist. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }
                Console.Clear();

                //Search chosen playlist, if it's empty, give info about it.
                Playlist chosenPlaylist = allPlaylists.First(p => p.Id == chosenPlaylistId);
                Console.WriteLine($"Playlist: {chosenPlaylist.Name}\r\n");
                if (chosenPlaylist.Content.Count == 0)
                {
                    Console.WriteLine("There is no song here!");
                    Console.WriteLine("\r\nPress any key to return to the list of your playlists...");
                    Console.ReadKey(true);
                    continue;
                }
                //Sort songs by songId and show all.
                IEnumerable<Song> songsAtPlaylist = chosenPlaylist.Content.OrderBy(song => song.Id);
                _menuView.ShowPlaylistSongs(songsAtPlaylist);
            }
        }
        
        private List<int> ChangeIdFromStrToList(string chosenSongs)
        {
            List<int> numbers = new List<int>();
            string idToNumbers = "";
            for (int i = 0; i < chosenSongs.Length; i++)
            {
                if (i == chosenSongs.Length - 1 && Char.IsDigit(chosenSongs[i]))
                {
                    idToNumbers += chosenSongs[i];
                    Int32.TryParse(idToNumbers, out int num);
                    if (num != 0)
                        numbers.Add(num);
                    idToNumbers = "";
                }

                if (Char.IsDigit(chosenSongs[i]))
                {
                    idToNumbers += chosenSongs[i];
                }
                else
                {
                    Int32.TryParse(idToNumbers, out int num);
                    if (num != 0)
                        numbers.Add(num);
                    idToNumbers = "";
                }
            }
            return numbers;
        }
        

        private List<Song> ReturnSongsAsidePlaylist(Playlist playlistToUpdate)
        {
            List<Song> availableSongs = new List<Song>();
            availableSongs.AddRange(_songService.GetAllItems());
            foreach (var song in playlistToUpdate.Content)
            {
                if (song.Id > 0 && song.Id <= _songService.GetAllItems().Count)
                {
                    availableSongs.Remove(song);
                }
            }
            return availableSongs;
        }
        private int CreateNewPlaylist()
        {
            var allSongs = _songService.GetAllItems();

            //If there are no songs in the database, return to the main menu
            if (allSongs.Count == 0)
            {
                Console.WriteLine("You can't create your playlist, because database of songs is empty.");
                Console.WriteLine("Press any key to return to Main menu...");
                Console.ReadKey();
                return -1;
            }

            //create new playlist with id, name and without songs
            Console.Write("Please name new playlist: ");
            string playlistName = Console.ReadLine();
            int lastId = _playlistService.GetLastId();
            Playlist playlist = new Playlist(lastId + 1, playlistName);
            _playlistService.AddItem(playlist);

            Console.Clear();
            Console.WriteLine($"Created playlist: {playlistName}\r\n");
            _menuView.ShowAvailableSongs(allSongs);
            Console.WriteLine($"\r\nChoose songs to add, you can choose multiple songs at once. Enter Id of songs - must be separated by a comma!");
            Console.Write("Your choice: ");

            //songs to add, chosen by user
            string chosenSongs = Console.ReadLine();
            List<int> numbers = ChangeIdFromStrToList(chosenSongs);

            //If songId (from numbers) is in the database, add it to the playlist.
            int lastSongId = allSongs.Count;
            for (int i = 0; i < numbers.Count; i++)
            {
                if (numbers[i] >= 1 && numbers[i] <= lastSongId)
                    playlist.Content.Add(allSongs.First(p => p.Id == numbers[i]));
            }

            //Give info about created playlist, if nothing is added - write it's empty.
            string success = "Playlist was successfully created!";
            if (playlist.Content.Count == 0)
                Console.WriteLine(success + " It's empty, so add favourite songs ASAP!");
            else
                Console.WriteLine(success);

            Console.ReadKey();
            return playlist.Id;
        }

        private int AddSongsToPlaylist()
        {
            while (true)
            {
                Console.Clear();
                var allPlaylists = _playlistService.GetAllItems();

                //If there are no playlist, return to Playlist menu.
                if (allPlaylists.Count == 0)
                {
                    Console.WriteLine("There is no playlist. You have to create playlist at first!");
                    Console.ReadKey(true);
                    break;
                }

                _menuView.ShowUpdatingMenu(allPlaylists);
                //If ESC is pressed, retrun to Playlist menu.
                var chosenPlaylistKeyInfo = Console.ReadKey();
                if (chosenPlaylistKeyInfo.Key == ConsoleKey.Escape)
                    break;

                //If chosen playlistId doesn't exist, return to Playlist menu or try again.
                Int32.TryParse(chosenPlaylistKeyInfo.KeyChar.ToString(), out int playlistId);
                if (playlistId <= 0 || playlistId > allPlaylists.Count)
                {
                    Console.WriteLine("\r\nSuch playlist doesn't exist. Press <ESC> to return to Playlist menu...or different key to try again!");
                    var choice = Console.ReadKey(true);
                    if (choice.Key == ConsoleKey.Escape)
                        break;
                    else
                        continue;
                }

                Console.Clear();
                Playlist playlistToUpdate = allPlaylists.First(p => p.Id == playlistId);
                Console.WriteLine($"Chosen playlist to update: {playlistToUpdate.Name}\r\n");

                //Return songs which are not yet added to the playlist.
                List<Song> availableSongs = ReturnSongsAsidePlaylist(playlistToUpdate);
                _menuView.ShowAvailableSongs(availableSongs);
                Console.WriteLine($"\r\nChoose songs to add, you can choose multiple songs at once. Enter Id of songs - must be separated by a comma!");
                Console.Write("Your choice: ");

                //songs to add, chosen by user
                string chosenSongs = Console.ReadLine();
                List<int> numbers = ChangeIdFromStrToList(chosenSongs);

                //Add new songs to the playlist.
                int howManyAdded = 0;
                for (int i = 0; i < availableSongs.Count; i++)
                {
                    if (numbers.Exists(p => p == availableSongs[i].Id))
                    {
                        playlistToUpdate.Content.Add(availableSongs[i]);
                        howManyAdded++;
                    }
                }
                //If nothing was added, try again. Otherwise give info about success.
                if (howManyAdded == 0)
                {
                    Console.WriteLine("You don't add any song to your playlist! Try again...");
                    Console.ReadKey(true);
                    continue;
                }
                Console.WriteLine("Songs were added successfully!");
                Console.ReadKey(true);
                return _playlistService.UpdateItem(playlistToUpdate);
            }
            return -1;
        }

       
    }
}
