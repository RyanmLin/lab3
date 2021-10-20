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

            return count;
        }


        /**
         * Checks if the line specifies a character's dialogue, returning
         * the index of the start of the dialogue.  If the
         * line specifies a new character is speaking, then extracts the
         * character's name.
         *
         * Assumptions: (doesn't have to be perfect)
         *     Line that starts with exactly two spaces has
         *       CHARACTER. <dialogue>
         *     Line that starts with exactly four spaces
         *       continues the dialogue of previous character
         *
         * @param line line to check
         * @param character extracted character name if new character,
         *        otherwise leaves character unmodified
         * @return index of start of dialogue if a dialogue line,
         *      -1 if not a dialogue line
         */
        public static int IsDialogueLine(string line, ref string character)
        {
            // new character
            if (line.Length >= 3 && line[0] == ' '
                && line[1] == ' ' && line[2] != ' ')
            {
                // extract character name

                int start_idx = 2;
                int end_idx = 3;
                while (end_idx <= line.Length && line[end_idx - 1] != '.')
                {
                    ++end_idx;
                }

                // no name found
                if (end_idx >= line.Length)
                {
                    return 0;
                }

                // extract character's name
                character = line.Substring(start_idx, end_idx - start_idx - 1);
                return end_idx;
            }

            // previous character
            if (line.Length >= 5 && line[0] == ' '
                && line[1] == ' ' && line[2] == ' '
                && line[3] == ' ' && line[4] != ' ')
            {
                // continuation
                return 4;
            }

            return 0;
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

            int index;
            string line;  // for storing each line read from the file
            string character = "";  // empty character to start
            System.IO.StreamReader file = new System.IO.StreamReader(filename);

            while ((line = file.ReadLine()) != null)
            {
                //=================================================
                // YOUR JOB TO ADD WORD COUNT INFORMATION TO MAP
                //=================================================

                // Is the line a dialogueLine?
                index = IsDialogueLine(line, ref character); // gets the index of when dialogue starts. Also gets character name
                if (index != 0) // Checks if the line is a dialogue Line
                {

                    if (index > 0 && character !=  "") // Checks that index is greater than 0 and that there is a character
                    {
                        int wordsInLine = WordCount(ref line, index); // Gets the number of words in the dialogue
                        if(wcounts.ContainsKey(character)) // checks dictionary for the key
                        {
                            wcounts[character] += wordsInLine; // adds the line word count to existing word count
                        }
                        else
                        {
                            wcounts.Add(character, wordsInLine); // Adds a new key-value to the dictionary
                        }
                    }
                    character = ""; // resets character
                }
            }
            // Close the file
            file.Close();
        }

        public static void PrintListofTuples(List<Tuple<int, string>> printableList)
        {
            // Prints the list of tuples
            foreach(Tuple<int, string> character in printableList)
            {
                Console.WriteLine("{0} spoke {1} words in this play", character.Item2, character.Item1);
            }
        }
    }
}
