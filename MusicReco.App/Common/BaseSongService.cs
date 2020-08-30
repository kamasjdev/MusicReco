using MusicReco.App.Abstract;
using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicReco.App.Common
{
    public class BaseSongService:ISongService
    {
        public List<Song> Items { get; set; }
        public BaseSongService()
        {
            Items = new List<Song>();
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

        public int AddItem(Song item)
        {
            Items.Add(item);
            return item.Id;
        }

        public void RemoveItem(Song item)
        {
            Items.Remove(item);
        }

        public List<Song> GetAllItems()
        {
            return Items;
        }

        public int UpdateItem(Song item)
        {
            var entity = Items.FirstOrDefault(p => p.Id == item.Id);

            if (entity != null)
            {
                entity = item;
            }
            return entity.Id;
        }

        public int CheckSongExistsInDatabase(string songTitle)
        {
            int songId = -1;
            foreach (var song in Items)
            {
                if (song.Title.Equals(songTitle, StringComparison.OrdinalIgnoreCase))
                {
                    songId = song.Id;
                    break;
                }
            }
            return songId;
        }

        public void LikeSong(int songId)
        {
            foreach(var song in Items)
            {
                if(song.Id == songId)
                {
                    song.Likes++;
                }
            }
        }
        public Song GetSongById(int songId)
        {
            return Items.FirstOrDefault(p => p.Id == songId);
            
        }
    }
}
