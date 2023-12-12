namespace DotNetRecruit
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class AnagramService
    {
        public IEnumerable<AnagramCounter> Compute(string dictionaryLocation)
        {
            //Instantiates the dictionary object that contains lists of strings that are anagrams
            Dictionary<int, Dictionary<string, List<string>>> dicAnagram = new Dictionary<int, Dictionary<string, List<string>>>();

            Console.WriteLine();
            Console.WriteLine("Reading file...");
            Console.WriteLine("Please Wait...");
           
            using (StreamReader sr = new StreamReader(dictionaryLocation))
            {
                //reading the dictionary file words line by line
                string word;
                while ((word = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrEmpty(word.Trim())) continue;
                    word = word.Split('|')[0].Trim();

                    //converting words to byte then sorting the words alphabetically
                    byte[] wordContent = Encoding.Default.GetBytes(word);
                    Array.Sort(wordContent);
                    string key = Encoding.Default.GetString(wordContent);
                    //Checking if the anagram dictionary has a key word of that length, if not we just add to the anagram dictionary the key length and a instantiating new dictionary object
                    if (!dicAnagram.ContainsKey(key.Length))
                    {
                        dicAnagram.Add(key.Length, new Dictionary<string, List<string>>());
                    }

                    //Initializing the dictionary by length object with the anagram dictionary at the index of the key length. 
                    Dictionary<string, List<string>> dicByLength = dicAnagram[key.Length];
                    //Checking if the dictionary by length contains the key if not add the key to the dictionary by length and initialize a new list of strings
                    if (!dicByLength.ContainsKey(key))
                    {
                        dicByLength.Add(key, new List<string>());
                    }
                    //Checks if the dictionary by length at index key has a key that is equal to the word if not add that word at index key
                    if (!dicByLength[key].Any(x => x.Equals(word)))
                    {
                        dicByLength[key].Add(word);
                    }
                }
            }


            Console.WriteLine("Finished reading...");

            string str;
            int strlen = 0;
            int[] arr = new int[100];
            //initializing the list
            List<AnagramCounter> li = new List<AnagramCounter>();
            foreach (Dictionary<string, List<string>> dic in dicAnagram.Values)
            {
               
                foreach (List<string> words in dic.Values)
                {
                    //Checking the word lengths and checking for the count of anagrams
                    str = words[0];
                    strlen = str.Length;
                    if (arr[strlen] != 0)
                    {
                        arr[strlen] = arr[strlen] + words.Count;
                    }
                    else
                    {
                        arr[strlen] = words.Count;
                    }

                 
                }

            }
            //Adding the found anagram word length and count to anagram counter list
            Console.WriteLine();
            Console.WriteLine("Showing the Anagram Counter results from 1 up until the last index that has greater than 0 anagrams count");
            var c = 1;
            for (int i = 0; i < arr.Length; i++)
            {
                if (i < 10 || arr[i] > 0) { 
                li.Add(new AnagramCounter { WordLength = c, Count = arr[i] });
                }
                c++;
                
                          
            }

            return li;

              
        }

    }
}
    

