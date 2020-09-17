using MusicReco.App.Abstract;
using MusicReco.Domain.Entity;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MusicReco.App.Concrete
{
    public class SongService : ISongService
    {
        public string songDatabasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SongDatabase.xml");
        public List<Song> Items { get; set; }
        public SongService()
        {
            Items = LoadSongsFromXmlFile();
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
            foreach (var song in Items)
            {
                if (song.Id == songId)
                {
                    song.Likes++;
                }
            }
        }

        public Song GetSongById(int songId)
        {
            return Items.FirstOrDefault(p => p.Id == songId);

        }

        public void UpdateFileWithSongs(Song newSong)
        {
            XmlRootAttribute root = new XmlRootAttribute();
            root.ElementName = "Songs";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Song>), root);

            using StreamWriter sw = new StreamWriter(songDatabasePath);
            xmlSerializer.Serialize(sw, Items);

        }

        public void SaveLikeToFile(int songId)
        {
            Song chosenSong = Items.FirstOrDefault(p => p.Id == songId);

            XDocument doc = XDocument.Load(songDatabasePath);
            var songs = doc.Root.Elements("Song").Where(
                song => song.Attribute("Id").Value == songId.ToString());
            if(songs.Any())
            {
                songs.First().Element("Likes").Value = chosenSong.Likes.ToString();
            }
            doc.Save(songDatabasePath);
        }

        private List<Song> LoadSongsFromXmlFile()
        {
            List<Song> songs = new List<Song>();
            XmlRootAttribute root = new XmlRootAttribute();
            root.ElementName = "Songs";
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Song>), root);

            string xml = File.ReadAllText(songDatabasePath);
            StringReader sr = new StringReader(xml);            
            songs = (List<Song>)xmlSerializer.Deserialize(sr);
            return songs;
        }
    }
}
