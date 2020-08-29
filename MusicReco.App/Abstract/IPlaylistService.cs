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
    }
}
