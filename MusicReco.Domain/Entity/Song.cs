using MusicReco.Domain.Common;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.Domain.Entity
{
    public class Song : BaseEntity
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public GenreName Genre { get; set; }
        public int Likes { get; set; }
        public string Description { get; set; }
        public Song()
        { }
        public Song(int id, string artist, string title, GenreName genre, int yearOfRelease, int likes, string des)
        {
            Id = id;
            Artist = artist;
            Title = title;
            Genre = genre;
            YearOfRelease = yearOfRelease;
            Likes = likes;
            Description = des;
        }
    }
}
