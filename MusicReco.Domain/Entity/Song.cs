using MusicReco.Domain.Common;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace MusicReco.Domain.Entity
{
    public class Song : BaseEntity
    {
        [XmlElement("Artist")]
        public string Artist { get; set; }
        [XmlElement("Title")]
        public string Title { get; set; }
        [XmlElement("YearOfRelease")]
        public int YearOfRelease { get; set; }
        [XmlElement("Genre")]
        public GenreName Genre { get; set; }
        [XmlElement("Likes")]
        public int Likes { get; set; }
        [XmlElement("Description")]
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
