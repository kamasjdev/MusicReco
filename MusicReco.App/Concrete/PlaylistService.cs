using MusicReco.App.Abstract;
using MusicReco.Domain.Entity;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MusicReco.App.Concrete
{
    public class PlaylistService : IPlaylistService
    {
        public List<Playlist> Items { get; set; }
        public PlaylistService()
        {
            if (File.Exists(@"Playlists.json"))
                Items = LoadPlaylistsFromFile();
            else
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

        public void UpdateFileWithPlaylists()
        {
            using FileStream fs = File.Open(@"Playlists.json",FileMode.OpenOrCreate);
            using StreamWriter sw = new StreamWriter(fs);
            using JsonWriter writer = new JsonTextWriter(sw);
            JsonSerializer serializer = new JsonSerializer();

            serializer.Serialize(writer, Items);            
        }

        private List<Playlist> LoadPlaylistsFromFile()
        {
            string output = File.ReadAllText(@"Playlists.json");
            var playlists = JsonConvert.DeserializeObject<List<Playlist>>(output);
            return playlists;
        }
    }
}
