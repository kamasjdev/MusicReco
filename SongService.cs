using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace MusicReco
{
    public class SongService
    {
        public List<Song> Songs { get; set; }
        public SongService()
        {
            Songs = new List<Song>();           
        }
        public void CreateDatabase()
        {
            Songs.Add(new Song() { Id = 1, Artist = "Pezet", Title = "Magenta", Genre = GenreName.HipHop, YearOfRelease = 2020, Likes = 1, Description = "Utwór pochodzący z albumu Muzyka Współczesna" });
            Songs.Add(new Song() { Id = 2, Artist = "Pezet", Title = "Zaalarmuj", Genre = GenreName.HipHop, YearOfRelease = 2010, Likes = 1, Description = "Utwór pochodzący z albumu Dziś w moim mieście" });
            Songs.Add(new Song() { Id = 3, Artist = "Amy Winehouse", Title = "Back to black", Genre = GenreName.Jazz, YearOfRelease = 2006, Likes = 1, Description = "Pod tym samym tytułem został wydany drugi i ostatni studyjny album piosenkarki." });
            Songs.Add(new Song() { Id = 4, Artist = "Amy Winehouse", Title = "Stronger than me", Genre = GenreName.Jazz, YearOfRelease = 2003, Likes = 1, Description = "" });
            Songs.Add(new Song() { Id = 5, Artist = "Rihanna", Title = "Umbrella", Genre = GenreName.Pop, YearOfRelease = 2008, Likes = 1, Description = "" });
            Songs.Add(new Song() { Id = 6, Artist = "Example", Title = "All night", Genre = GenreName.Electronic, YearOfRelease = 2019, Likes = 1, Description = "" });
            Songs.Add(new Song() { Id = 7, Artist = "Lady Pank ", Title = "Mniej niż zero", Genre = GenreName.Rock, YearOfRelease = 1983, Likes = 1, Description = "Utwór pochodzi z debiutanckiego albumu Lady Pank" });
            Songs.Add(new Song() { Id = 2, Artist = "Białas", Title = "Swoosh Gang", Genre = GenreName.HipHop, YearOfRelease = 2020, Likes = 1, Description = "Na feacie Pezet" });
        }
        public ConsoleKeyInfo AddNewSongView (MenuActionService menuActionService)
        {
            var addSongMenu = menuActionService.GetMenuActionsByMenuName("AddSongMenu");
            Console.WriteLine("\r\n\r\nPlease select genre of your song:");
            foreach(var menuAction in addSongMenu)
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.ActionName}");
            }
            Console.Write("Enter the number: ");
            var option = Console.ReadKey();
            return option;
        }

        public int AddNewSong(char genreId)
        {
            int chosenGenreId;
            Int32.TryParse(genreId.ToString(), out chosenGenreId);

            Song song = new Song();
            song.Genre = (GenreName)chosenGenreId;

            Console.Write("\r\nPlease enter id for new song: ");
            Int32.TryParse(Console.ReadLine(), out int songId);
            Console.Write("Please enter artist name: ");
            string artistName = Console.ReadLine();
            Console.Write("Please enter title of the song: ");
            string title = Console.ReadLine();
            Console.Write("Please enter year of release: ");
            Int32.TryParse(Console.ReadLine(), out int yearOfRelease);
            Console.WriteLine("If you want, write short description of the song. If not, press enter... ");
            string description = Console.ReadLine();

            song.Id = songId;
            song.Artist = artistName;
            song.Title = title;
            song.YearOfRelease = yearOfRelease;
            song.Description = description;
            song.Likes = 1;

            Songs.Add(song);

            return songId;
        }

        public ConsoleKeyInfo RecommendView (MenuActionService menuActionService)
        {
            var recommendMenu = menuActionService.GetMenuActionsByMenuName("RecommendMenu");

            Console.WriteLine("\r\nWhat recommendation do you need?");
            
            for(int i=0; i<recommendMenu.Count; i++)
            {
                Console.WriteLine($"{recommendMenu[i].Id}. {recommendMenu[i].ActionName}");
            }
            Console.Write("Enter chosen number: ");
            var option = Console.ReadKey();
            return option;
        }

        public void ArtistReco()
        {
            List<Song> artistSongs = new List<Song>();
            Console.Write("\r\nPlease enter the name of artist: ");
            string artistName = Console.ReadLine();  

            foreach(var song in Songs)
            {
                if(song.Artist.Equals(artistName, StringComparison.OrdinalIgnoreCase))
                {
                    artistSongs.Add(song);
                }
            }
            Console.WriteLine("\r\nSong id...  Arist... - Title...");
            if (artistSongs.Count == 0)
                Console.WriteLine("There is no song fulfilling these criteria.");
            else
            {
                for (int i = 0; i < artistSongs.Count; i++)
                {
                    Console.WriteLine($"{artistSongs[i].Id}.  {artistSongs[i].Artist} - {artistSongs[i].Title}");
                }
            }
        }

        public void GenreReco()
        {
            List<Song> genreSongs = new List<Song>();
            Console.WriteLine("\r\nPlease select a genre:");
            for(int i=1; i<7; i++)
            {
                Console.WriteLine($"{i}. {(GenreName)i}");
            }
            Console.Write("Enter the number: ");
            Int32.TryParse(Console.ReadLine(), out int chosenGenre);

            foreach (var song in Songs)
            {
                if ((int)song.Genre ==chosenGenre)
                {
                    genreSongs.Add(song);
                }
            }
            Console.WriteLine("\r\nSong id...  Arist... - Title...");
            if (genreSongs.Count == 0)
                Console.WriteLine("There is no song fulfilling these criteria.");
            else
            {
                for (int i = 0; i < genreSongs.Count; i++)
                {
                    Console.WriteLine($"{genreSongs[i].Id}.  {genreSongs[i].Artist} - {genreSongs[i].Title}");
                }
            }
        }
        public void YearReco()
        {
            List<Song> yearSongs = new List<Song>();
            Console.Write("\r\nPlease enter time interval, from: ");
            int from;
            Int32.TryParse(Console.ReadLine(), out from);
            Console.Write("  till: ");
            int till;
            Int32.TryParse(Console.ReadLine(), out till);

            foreach (var song in Songs)
            {
                if (song.YearOfRelease>=from && song.YearOfRelease<=till)
                {
                    yearSongs.Add(song);
                }
            }
            Console.WriteLine("\r\nSong id...  Arist... - Title...");
            if (yearSongs.Count == 0)
                Console.WriteLine("There is no song fulfilling these criteria.");
            else
            {
                for (int i = 0; i < yearSongs.Count; i++)
                {
                    Console.WriteLine($"{yearSongs[i].Id}.  {yearSongs[i].Artist} - {yearSongs[i].Title}");
                }
            }
        }

        public int LikeChosenSong()
        {
            Console.Write("\r\nPlease enter the title of the song: ");
            string songToBeLiked = Console.ReadLine();
            int songId = -1;
   
            foreach(var song in Songs)
            {
                if(song.Title.Equals(songToBeLiked,StringComparison.OrdinalIgnoreCase))
                {
                    song.Likes++;
                    songId = song.Id;
                    break;
                }
            }         
            
            if(songId==-1)
                Console.WriteLine("\r\nSuch song doesn't exist.");
            else
                Console.WriteLine("\r\nYou liked it!");
            return songId;
        }

        public void ShowDetails()
        {
            Console.Write("\r\nPlease enter the title of the song: ");
            string title = Console.ReadLine();
            bool success = false;

            foreach(var song in Songs)
            {
                if(song.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    success = true;
                    if (song.Description == "")
                        song.Description = "No description";
                    
                    Console.WriteLine($"\r\nSong id: {song.Id}\r\nArtist: {song.Artist}\r\nTitle: {song.Title}\r\n" +
                        $"Year of release: {song.YearOfRelease}\r\nGenre: {song.Genre}\r\nLikes: {song.Likes}\r\nDescription: {song.Description}");
                    break;
                }
            }
            if(!success)
                Console.WriteLine("Such title was not found.");
        }

    }
}
