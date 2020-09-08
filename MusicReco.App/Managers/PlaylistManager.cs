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
        private IPlaylistService _playlistService;
        private ISongService _songService;
        private HelperMethods helper;
        public PlaylistManager(MenuView menuView, ISongService songService, IPlaylistService playlistService)
        {
            _menuView = menuView;
            _playlistService = playlistService;
            _songService = songService;
            helper = new HelperMethods();
        }

        public int ChooseToCreateOrAddPlaylist()
        {
            Console.Clear();
            _menuView.ShowPlaylistMenu();
            var chosenOption = Console.ReadKey();
            //If pressed ESC, return to the main menu.
            if (chosenOption.Key == ConsoleKey.Escape)
                return -1;
            int chosenOptionId;
            Int32.TryParse(chosenOption.KeyChar.ToString(), out chosenOptionId);
            return chosenOptionId;
        }

        public bool CreateNewOrAddSwitcher(int choice)
        {
            switch (choice)
            {
                case -1: //return to Main Menu
                    return false;
                case 1:                   
                    Playlist createdPlaylist = CreateNewPlaylist();
                    int newPlaylistId = AddNewPlaylist(createdPlaylist);
                    Continue();
                    if (newPlaylistId != -1)
                    {
                        List<int> songsIds = ReturnSongsIdToBeAddedToPlaylist(newPlaylistId);
                        int howManyAdded = UpdatePlaylist(newPlaylistId, songsIds);
                        HowManySongsAddedToPlaylistInfo(howManyAdded);
                        _playlistService.UpdateFileWithPlaylists();
                    }                    
                    break;
                case 2:
                    int playlistId;
                    do
                    {
                        Console.Clear();
                        playlistId = ChoosePlaylistToUpdate();
                        if (playlistId != -1)
                        {
                            List<int> songsIds = ReturnSongsIdToBeAddedToPlaylist(playlistId);
                            int howManyAdded = UpdatePlaylist(playlistId, songsIds);
                            HowManySongsAddedToPlaylistInfo(howManyAdded);
                            _playlistService.UpdateFileWithPlaylists();
                        }
                    } while (playlistId != -1);
                    break;
                default:
                    Console.WriteLine("\r\nSuch operation doesn't exist.");
                    Continue();
                    break;
            }
            return true;
        }

        public Playlist CreateNewPlaylist()
        {
            var allSongs = _songService.GetAllItems();
            if (allSongs.Count == 0)
            {
                Console.WriteLine("You can't create your playlist, because database of songs is empty.");
                Console.WriteLine("Press any key to return to Main menu...");
                Console.ReadKey();
                return null;
            }

            //create new playlist with id, name and without songs
            Console.Write("\r\nPlease name new playlist: ");
            string playlistName = Console.ReadLine();
            if(playlistName == "")
                return null;

            int lastId = _playlistService.GetLastId();
            Playlist playlist = new Playlist(lastId + 1, playlistName);

            return playlist;
        }

        public int AddNewPlaylist(Playlist newPlaylist)
        {
            if (newPlaylist != null)
            {
                _playlistService.AddItem(newPlaylist);
                Console.WriteLine("Successfully created!");
                return newPlaylist.Id;
            }
            else
            {
                Console.WriteLine("\r\nSomething went wrong. Name your playlist!");
                return -1;
            }
        }

        public int ChoosePlaylistToUpdate()
        {
            var allPlaylists = _playlistService.GetAllItems();

            //If there are no playlist, return to Playlist menu.
            if (allPlaylists.Count == 0)
            {
                Console.WriteLine("\r\n\r\nThere is no playlist. You have to create playlist at first!");
                Continue();
                return -1;
            }
            _menuView.ShowUpdatingMenu(allPlaylists);
            //If ESC is pressed, retrun to Playlist menu.
            var chosenPlaylistKeyInfo = Console.ReadKey();
            if (chosenPlaylistKeyInfo.Key == ConsoleKey.Escape)
                return -1;
            Int32.TryParse(chosenPlaylistKeyInfo.KeyChar.ToString(), out int playlistId);
            //Chosen playlistId doesn't exist.
            if (playlistId <= 0 || playlistId > allPlaylists.Count)
            {
                Console.WriteLine("\r\nSuch playlist doesn't exist.");
                Continue();
                return -1;
            }
            return playlistId;

        }

        public List<int> ReturnSongsIdToBeAddedToPlaylist(int playlistId)
        {
            Console.Clear();
            Playlist playlistToUpdate = _playlistService.GetPlaylistById(playlistId);
            Console.WriteLine($"Chosen playlist: {playlistToUpdate.Name}\r\n");

            //Return songs which are not yet added to the playlist.
            List<Song> availableSongs = _playlistService.ReturnSongsAsidePlaylist(_songService.Items,playlistToUpdate);
            _menuView.ShowAvailableSongsToPlaylistAdd(availableSongs);

            //Songs to add, chosen by user.
            string chosenSongs = Console.ReadLine();
            List<int> numbers = new List<int>();
            numbers = helper.ChangeIdFromStrToList(chosenSongs);

            return numbers;                          
        }

        public int UpdatePlaylist(int playlistId, List<int> songsIds)
        {
            List<Song> allSongs = _songService.GetAllItems();
            int howManyAdded = 0;
            if (songsIds.Count == 0)
                return howManyAdded;

            Playlist playlistToUpdate = _playlistService.GetPlaylistById(playlistId);
            //Add new songs to the playlist.
            List<Song> availableSongs = _playlistService.ReturnSongsAsidePlaylist(allSongs, playlistToUpdate);

            for (int i = 0; i < availableSongs.Count; i++)
            {
                if (songsIds.Exists(p => p == availableSongs[i].Id))
                {
                    playlistToUpdate.Content.Add(availableSongs[i]);
                    howManyAdded++;
                }
            }
            return howManyAdded;
        }

        public void HowManySongsAddedToPlaylistInfo(int howMany)
        {
            Console.WriteLine();
            if(howMany==0)
                Console.WriteLine("You don't add any song to your playlist!");
            else
                Console.WriteLine($"You added {howMany} songs to the playlist!");
            Continue();
        }

        public int ChoosePlaylistToShow()
        {
            var allPlaylists = _playlistService.GetAllItems();
            if (allPlaylists.Count == 0)
            {
                Console.WriteLine("There is no playlist. Create something!");
                Continue();
                return -1;
            }

            _menuView.ShowAllPlaylists(allPlaylists);
            var chosenPlaylistKey = Console.ReadKey(true);
            Int32.TryParse(chosenPlaylistKey.KeyChar.ToString(), out int chosenPlaylistId);
            //If ESC is pressed, return to the Main menu.
            if (chosenPlaylistKey.Key == ConsoleKey.Escape)
                return -1;
            return chosenPlaylistId;
        }

        public void ShowPlaylist(int choice)
        {
            var allPlaylists = _playlistService.GetAllItems();
            if (choice == -1)
                return;

            if ((choice == 0) || (choice > allPlaylists.Count))
            {
                Console.WriteLine("\r\n\r\nSuch playlist Id doesn't exist. Press any key to try again...");
                Console.ReadKey();
            }
            else
            {
                Playlist chosenPlaylist = _playlistService.GetPlaylistById(choice);
                Console.Clear();
                _menuView.ShowPlaylistSongs(chosenPlaylist);
            }
        }

        private static void Continue()
        {
            Console.WriteLine("\r\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
