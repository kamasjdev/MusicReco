using MusicReco.App.Common;
using MusicReco.Domain.Entity;
using MusicReco.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.App.Concrete
{
    public class SongService : BaseService<Song>
    {
        public SongService()
        {
            CreateDatabase();
        }
        private void CreateDatabase()
        {
            AddItem(new Song() { Id = 1, Artist = "Pezet", Title = "Magenta", Genre = GenreName.HipHop, YearOfRelease = 2020, Likes = 1, Description = "Utwór pochodzący z albumu Muzyka Współczesna" });
            AddItem(new Song() { Id = 2, Artist = "Pezet", Title = "Zaalarmuj", Genre = GenreName.HipHop, YearOfRelease = 2010, Likes = 1, Description = "Utwór pochodzący z albumu Dziś w moim mieście" });
            AddItem(new Song() { Id = 3, Artist = "Amy Winehouse", Title = "Back to black", Genre = GenreName.Jazz, YearOfRelease = 2006, Likes = 1, Description = "Pod tym samym tytułem został wydany drugi i ostatni studyjny album piosenkarki." });
            AddItem(new Song() { Id = 4, Artist = "Amy Winehouse", Title = "Stronger than me", Genre = GenreName.Jazz, YearOfRelease = 2003, Likes = 1, Description = "" });
            AddItem(new Song() { Id = 5, Artist = "Rihanna", Title = "Umbrella", Genre = GenreName.Pop, YearOfRelease = 2008, Likes = 1, Description = "" });
            AddItem(new Song() { Id = 6, Artist = "Example", Title = "All night", Genre = GenreName.Electronic, YearOfRelease = 2019, Likes = 1, Description = "" });
            AddItem(new Song() { Id = 7, Artist = "Lady Pank ", Title = "Mniej niż zero", Genre = GenreName.Rock, YearOfRelease = 1983, Likes = 1, Description = "Utwór pochodzi z debiutanckiego albumu Lady Pank" });
            AddItem(new Song() { Id = 8, Artist = "Jon Lajoie", Title = "Everyday Normal Guy 2", Genre = GenreName.HipHop, YearOfRelease = 2009, Likes = 1, Description = "" });
            AddItem(new Song() { Id = 9, Artist = "Fergie", Title = "A little party never killed nobody", Genre = GenreName.Pop, YearOfRelease = 2013, Likes = 1, Description = "Singiel z dwiema nominacjami do World Music Award" });
            AddItem(new Song() { Id = 10, Artist = "Halsey", Title = "You should be sad", Genre = GenreName.Pop, YearOfRelease = 2020, Likes = 1, Description = "" });
            AddItem(new Song() { Id = 11, Artist = "Białas", Title = "Swoosh Gang", Genre = GenreName.HipHop, YearOfRelease = 2020, Likes = 1, Description = "Na feacie Pezet" });
            AddItem(new Song() { Id = 12, Artist = "Antonio Vivaldi", Title = "Cztery pory roku", Genre = GenreName.Classical, YearOfRelease = 1725, Likes = 1, Description = "Cykl 4 koncertów skrzypcowych" });
        }
    }
}
