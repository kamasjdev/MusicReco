using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco
{
    public class Song
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public int YearOfRelease { get; set; }
        public GenreName Genre { get; set; }
        public int Likes { get; set; }
        public string Description { get; set; }
    }
}
