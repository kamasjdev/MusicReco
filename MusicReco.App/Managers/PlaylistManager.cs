using MusicReco.App.Concrete;
using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace MusicReco.App.Managers
{
    public class PlaylistManager
    {
        private readonly MenuActionService _menuActionService;
        private PlaylistService _playlistService;
        private SongService _songService;
        public PlaylistManager(MenuActionService menuActionService, SongService songService)
        {
            _playlistService = new PlaylistService();
            _menuActionService = menuActionService;
            _songService = songService;
        }

        public int CreateNewOrAdd()
        {
            var playlistMenu = _menuActionService.GetMenuActionsByMenuName("PlaylistMenu");

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Press <ESC> to return to the Main menu...\r\n");
                Console.WriteLine("Please choose one option:");
                foreach (var menuAction in playlistMenu)
                {
                    Console.WriteLine($"{menuAction.Id}. {menuAction.ActionName}");
                }
                Console.Write("Enter the number: ");
                var chosenOption = Console.ReadKey(true);
                if (chosenOption.Key == ConsoleKey.Escape)
                    return -1;

                int chosenOptionId;
                Int32.TryParse(chosenOption.KeyChar.ToString(), out chosenOptionId);
                if (chosenOptionId < 1 || chosenOptionId > 2)
                {
                    Console.Clear();
                    Console.WriteLine("Such operation doesn't exist. Press <ESC> to return to Main menu...or different key to try again!");
                    var choice = Console.ReadKey(true);
                    if (choice.Key == ConsoleKey.Escape)
                        return -1;
                    else
                        continue;
                }
                Console.Clear();

                if (chosenOptionId == 1)
                {
                    var allSongs = _songService.GetAllItems();
                    if (allSongs.Count == 0)
                    {
                        Console.WriteLine("You can't create your playlist, because database of songs is empty.");
                        Console.WriteLine("Press any key to return to Main menu...");
                        Console.ReadKey();
                        return -1;
                    }
                    Console.Write("Please name new playlist: ");
                    string playlistName = Console.ReadLine();
                    int lastId = _playlistService.GetLastId();
                    Playlist playlist = new Playlist(lastId + 1, playlistName);
                    _playlistService.AddItem(playlist);
                    Console.Clear();

                    Console.WriteLine($"Created playlist: {playlistName}\r\n");
                    Console.WriteLine("Available songs in database:");
                    Console.WriteLine("\r\nSong id...  Arist... - Title...");
                    foreach (var song in allSongs)
                    {
                        Console.WriteLine($"{song.Id}. {song.Artist} - {song.Title}");
                    }
                    Console.WriteLine($"\r\nChoose songs to add, you can choose multiple songs at once. Enter Id of songs - must be separated by a comma!");
                    Console.Write("Your choice: ");
                    string chosenSongs = Console.ReadLine();

                    List<int> numbers = new List<int>();
                    string idFromNumbers = "";
                    for (int i = 0; i < chosenSongs.Length; i++)
                    {
                        if (i == chosenSongs.Length - 1 && Char.IsDigit(chosenSongs[i]))
                        {
                            idFromNumbers += chosenSongs[i];
                            Int32.TryParse(idFromNumbers, out int num);
                            if (num != 0)
                                numbers.Add(num);
                            idFromNumbers = "";
                        }

                        if (Char.IsDigit(chosenSongs[i]))
                        {
                            idFromNumbers += chosenSongs[i];
                        }
                        else
                        {
                            Int32.TryParse(idFromNumbers, out int num);
                            if (num != 0)
                                numbers.Add(num);
                            idFromNumbers = "";
                        }
                    }

                    int lastSongId = allSongs.Count;
                    for (int i = 0; i < numbers.Count; i++)
                    {
                        if (numbers[i] >= 1 && numbers[i] <= lastSongId)
                            playlist.Content.Add(allSongs.FirstOrDefault(p => p.Id == numbers[i]));
                    }
                    string success = "Playlist was successfully created!";
                    if (playlist.Content.Count == 0)
                        Console.WriteLine(success + " It's empty, so add favourite songs ASAP!");
                    else
                        Console.WriteLine(success);

                    Console.ReadKey();
                    return playlist.Id;
                }
                else
                {
                   while(true)
                    {
                        Console.Clear();
                        var allPlaylists = _playlistService.GetAllItems();
                        if(allPlaylists.Count == 0)
                        {
                            Console.WriteLine("There is no playlist. You have to create playlist at first!");
                            Console.ReadKey(true);
                            break;
                        }
                        Console.WriteLine("Press <ESC> to return to the Playlist menu...\r\n");
                        Console.WriteLine("Please choose playlist, which you would like to update:");
                        foreach (var playlist in allPlaylists)
                        {
                            Console.WriteLine($"{playlist.Id}. {playlist.Name}");
                        }
                        Console.Write("Enter playlist Id: ");
                        var chosenPlaylistKeyInfo = Console.ReadKey(true);
                        if (chosenPlaylistKeyInfo.Key == ConsoleKey.Escape)
                            break;

                        Console.Clear();
                        Int32.TryParse(chosenPlaylistKeyInfo.KeyChar.ToString(), out int playlistId);
                        if(playlistId<=0 || playlistId >allPlaylists.Count)
                        {
                            Console.WriteLine("Such playlist doesn't exist. Press <ESC> to return to Playlist menu...or different key to try again!");
                            var choice = Console.ReadKey(true);
                            if (choice.Key == ConsoleKey.Escape)
                                break;
                            else
                                continue;
                        }
                        Console.Clear();
                        Playlist playlistToUpdate = allPlaylists.First(p => p.Id == playlistId);
                        Console.WriteLine($"Chosen playlist to update: {playlistToUpdate.Name}\r\n");
                        Console.WriteLine("Available songs to add:\r\n");
                        List<Song> availableSongs = new List<Song>();
                        availableSongs.AddRange(_songService.GetAllItems());
                        foreach(var song in playlistToUpdate.Content)
                        {
                            if(song.Id > 0 && song.Id <= _songService.GetAllItems().Count)
                            {
                                availableSongs.Remove(song);
                            }
                        }
                        Console.WriteLine("Song id...  Arist... - Title...");
                        foreach (var song in availableSongs)
                        {
                            Console.WriteLine($"{song.Id}. {song.Artist} - {song.Title}");
                        }

                        Console.WriteLine($"\r\nChoose songs to add, you can choose multiple songs at once. Enter Id of songs - must be separated by a comma!");
                        Console.Write("Your choice: ");
                        string chosenSongs = Console.ReadLine();

                        List<int> numbers = new List<int>();
                        string idFromNumbers = "";
                        for (int i = 0; i < chosenSongs.Length; i++)
                        {
                            if (i == chosenSongs.Length - 1 && Char.IsDigit(chosenSongs[i]))
                            {
                                idFromNumbers += chosenSongs[i];
                                Int32.TryParse(idFromNumbers, out int num);
                                if (num != 0)
                                    numbers.Add(num);
                                idFromNumbers = "";
                            }

                            if (Char.IsDigit(chosenSongs[i]))
                            {
                                idFromNumbers += chosenSongs[i];
                            }
                            else
                            {
                                Int32.TryParse(idFromNumbers, out int num);
                                if (num != 0)
                                    numbers.Add(num);
                                idFromNumbers = "";
                            }
                        }
                        int howManyAdded = 0;
                        for(int i=0; i<availableSongs.Count; i++)
                        {
                            if(numbers.Exists( p => p == availableSongs[i].Id))
                            {
                                playlistToUpdate.Content.Add(availableSongs[i]);
                                howManyAdded++;
                            }
                        }
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
                }
            }
        }

        public void ShowPlaylists()
        {
            var allPlaylists = _playlistService.GetAllItems();
            if(allPlaylists.Count == 0)
            {
                Console.WriteLine("There is no playlist. Create something!");
                Console.ReadKey();
                return;
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Press <ESC> to return to the Main menu...\r\n");
                foreach (var playlist in allPlaylists)
                {
                    Console.WriteLine($"{playlist.Id}. {playlist.Name}");
                }
                Console.Write("Please type playlist Id to show songs: ");
                var chosenPlaylistKey = Console.ReadKey(true);
                if (chosenPlaylistKey.Key == ConsoleKey.Escape)
                    return;

                int chosenPlaylistId;
                Int32.TryParse(chosenPlaylistKey.KeyChar.ToString(), out chosenPlaylistId);
                if ((chosenPlaylistId == 0) || (chosenPlaylistId > allPlaylists.Count))
                {
                    Console.WriteLine("\r\n\r\nSuch playlist Id doesn't exist. Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }
                Console.Clear();
                Playlist chosenPlaylist = allPlaylists.First(p => p.Id == chosenPlaylistId);
                Console.WriteLine($"Playlist: {chosenPlaylist.Name}\r\n");
                if(chosenPlaylist.Content.Count == 0)
                {
                    Console.WriteLine("There is no song here!");
                    Console.WriteLine("\r\nPress any key to return to the list of your playlists...");
                    Console.ReadKey(true);
                    continue;
                }
                IEnumerable<Song> songsAtPlaylist = chosenPlaylist.Content.OrderBy(song => song.Id);
                Console.WriteLine("Song id...  Arist... - Title...");
                foreach (var song in songsAtPlaylist)
                {
                    Console.WriteLine($"{song.Id}. {song.Artist} - {song.Title}");
                }
                Console.WriteLine("\r\nPress any key to return to the list of your playlists...");
                Console.ReadKey(true);
            }
        }
    }
}
