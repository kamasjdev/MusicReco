using MusicReco.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.App.Abstract
{
    public interface ISongService
    {
        List<Song> Items { get; set; }
        List<Song> GetAllItems();
        int AddItem(Song item);
        void RemoveItem(Song item);
        int UpdateItem(Song item);
        int GetLastId();
        int CheckSongExistsInDatabase(string songTitle);
        void LikeSong(int songId);
        Song GetSongById(int songId);
    }
}
