using MusicReco.Domain.Entity;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.App.HelpersForManagers
{
    public class Recommendation
    {
        private List<Song> _recoSongs;
        public Recommendation()
        {
            _recoSongs = new List<Song>();
        }
        public List<Song> RecoBasedOnArtist(List<Song> songs, string artistName)
        {          
            _recoSongs.Clear();
            //Search songs created by chosen artist and add to recoSongs
            foreach (var song in songs)
            {
                if (song.Artist.Equals(artistName, StringComparison.OrdinalIgnoreCase))
                {
                    _recoSongs.Add(song);
                }
            }
            return _recoSongs;    
        }
        public List<Song> RecoBasedOnGenre(List<Song> songs, int chosenGenre)
        {
            _recoSongs.Clear();
            foreach (var song in songs)
            {
                if ((int)song.Genre == chosenGenre)
                {
                    _recoSongs.Add(song);
                }
            }
            return _recoSongs;
        }
        public List<Song> RecoBasedOnYear(List<Song> songs, int[] fromTill)
        {
            _recoSongs.Clear();
            foreach (var song in songs)
            {
                if (song.YearOfRelease >= fromTill[0] && song.YearOfRelease <= fromTill[1])
                {
                    _recoSongs.Add(song);
                }
            }
            return _recoSongs;
        }       
    }
}
