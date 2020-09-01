using MusicReco.App.Abstract;
using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicReco.App.Concrete
{
    public class PlaylistService : IPlaylistService
    {
        public List<Playlist> Items { get; set; }
        public PlaylistService()
        {
            Items = new List<Playlist>();
        }

        public int GetLastId()
        {
            int lastId;
            if (Items.Any())
            {
                lastId = Items.OrderBy(p => p.Id).LastOrDefault().Id;
            }
            else
            {
                lastId = 0;
            }
            return lastId;
        }

        public int AddItem(Playlist item)
        {
            Items.Add(item);
            return item.Id;
        }

        public List<Playlist> GetAllItems()
        {
            return Items;
        }

        public void RemoveItem(Playlist item)
        {
            Items.Remove(item);
        }

        public int UpdateItem(Playlist item)
        {
            var entity = Items.FirstOrDefault(p => p.Id == item.Id);

            if (entity != null)
            {
                entity = item;
            }
            return entity.Id;
        }

        public List<Song> ReturnSongsAsidePlaylist(List<Song> databaseSongs, Playlist playlistToUpdate)
        {
            List<Song> availableSongs = new List<Song>();
            availableSongs.AddRange(databaseSongs);
            foreach (var song in playlistToUpdate.Content)
            {
                if (song.Id > 0 && song.Id <= databaseSongs.Count)
                {
                    availableSongs.Remove(song);
                }
            }
            return availableSongs;
        }

        public Playlist GetPlaylistById(int playlistId)
        {
            return Items.FirstOrDefault(p => p.Id == playlistId);
        }

        public void AddSongToPlaylist(Playlist playlist, Song song)
        {
            playlist.Content.Add(song);
        }
    }
}
