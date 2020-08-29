using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicReco.App.Common
{
    public class BasePlaylistService
    {
        public List<Playlist> Items { get; set; }
        public BasePlaylistService()
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
    }
}
