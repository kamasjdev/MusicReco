using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.App.Abstract
{
    public interface IPlaylistService
    {
        List<Playlist> Items { get; set; }
        List<Playlist> GetAllItems();
        int AddItem(Playlist item);
        void RemoveItem(Playlist item);
        int UpdateItem(Playlist item);
        int GetLastId();
        Playlist GetPlaylistById(int playlistId);
        List<Song> ReturnSongsAsidePlaylist(List<Song> databaseSongs,Playlist playlistToUpdate);
        void AddSongToPlaylist(Playlist playlist, Song song);
        void UpdateFileWithPlaylists();
    }
}
