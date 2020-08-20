using MusicReco.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.Domain.Entity
{
    public class Playlist : BaseEntity
    {
        public string Name { get; set; }
        public List<Song> Content { get; set; }

        public Playlist(int id, string name)
        {
            Id = id;
            Name = name;
            Content = new List<Song>();
        }
    }
}
