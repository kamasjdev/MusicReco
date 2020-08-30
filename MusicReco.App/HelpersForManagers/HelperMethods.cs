using System;
using System.Collections.Generic;
using System.Text;

namespace MusicReco.App.HelpersForManagers
{
    public class HelperMethods
    {
        public List<int> ChangeIdFromStrToList(string chosenSongs)
        {
            List<int> numbers = new List<int>();
            string idToNumbers = "";
            for (int i = 0; i < chosenSongs.Length; i++)
            {
                if (i == chosenSongs.Length - 1 && Char.IsDigit(chosenSongs[i]))
                {
                    idToNumbers += chosenSongs[i];
                    Int32.TryParse(idToNumbers, out int num);
                    if (num != 0)
                        numbers.Add(num);
                    idToNumbers = "";
                }

                if (Char.IsDigit(chosenSongs[i]))
                {
                    idToNumbers += chosenSongs[i];
                }
                else
                {
                    Int32.TryParse(idToNumbers, out int num);
                    if (num != 0)
                        numbers.Add(num);
                    idToNumbers = "";
                }
            }
            return numbers;
        }
    }
}
