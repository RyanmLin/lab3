using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Lab3
{
    class HelperFunctions
    {
        /**
        * Counts number of words, separated by spaces, in a line.
        * @param line string in which to count words
        * @param start_idx starting index to search for words
        * @return number of words in the line
        */
        public static int WordCount(ref string line, int start_idx)
        {
            // YOUR IMPLEMENTATION HERE
            int count = 0;
            bool countedWord = false;

            for(int i = 0; i < line.Length; i++)
            {
                if(line[i] == ' ' && !countedWord)
                {
                    count++;
                    countedWord = true;
                }
                if(line[i] != ' ')
                {
                    countedWord = false;
                }
            }
            count++;
            Console.WriteLine("Number of Words: " + count);

            return count;
        }

        public static List<Tuple<int, string>> SortCharactersByWordcount(Dictionary<string, int> wordcount)
        {
            // Implement sorting by word count here
            List<Tuple<int, string>> newList = new List<Tuple<int, string>>();

            foreach(KeyValuePair<string, int> character in wordcount.OrderByDescending(key => key.Value))
            {
                newList.Add(new Tuple<int, string>(character.Value, character.Key));
            }

            return newList;
        }

        /**
        * Reads a file to count the number of words each actor speaks.
        *
        * @param filename file to open
        * @param mutex mutex for protected access to the shared wcounts map
        * @param wcounts a shared map from character -> word count
        */
        public static void CountCharacterWords(string filename,
                                 Mutex mutex,
                                 Dictionary<string, int> wcounts)
        {

            //===============================================
            //  IMPLEMENT THREAD SAFETY IN THIS METHOD
            //===============================================

            string line;  // for storing each line read from the file
            string character = "";  // empty character to start
            System.IO.StreamReader file = new System.IO.StreamReader(filename);

            while ((line = file.ReadLine()) != null)
            {
                //=================================================
                // YOUR JOB TO ADD WORD COUNT INFORMATION TO MAP
                //=================================================

                // Is the line a dialogueLine?
                //    If yes, get the index and the character name.
                //      if index > 0 and character not empty
                //        get the word counts
                //          if the key exists, update the word counts
                //          else add a new key-value to the dictionary
                //    reset the character   

            }
            // Close the file
        }

        public static void PrintListofTuples()
        {
            // Prints the list of tuples
        }
    }
}
