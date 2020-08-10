using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco
{
    public class Helpers
    {
        public void ChooseOfRecommendation(char chosenReco, SongService songService)
        {
            switch (chosenReco)
            {
                case '1':
                    songService.ArtistReco();
                    break;
                case '2':
                    songService.GenreReco();
                    break;
                case '3':
                    songService.YearReco();
                    break;
                default:
                    Console.WriteLine("\r\nSuch option doesn't exist.");
                    break;
            }
        }

    }
}
