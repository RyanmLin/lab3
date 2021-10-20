using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Lab3
{
    class WordCountTester
    {
        static int Main()
        {
            string mode = "s";
            // map and mutex for thread safety
            Mutex mutex = new Mutex();
            Dictionary<string, int> wordcountSingleThread = new Dictionary<string, int>();

            Dictionary<string, int> wordcountMultiThread = new Dictionary<string, int>();

            List<Tuple<int, string>> newList = new List<Tuple<int, string>>();

            var filenames = new List<string> {
                "data/shakespeare_antony_cleopatra.txt",
                "data/shakespeare_hamlet.txt",
                "data/shakespeare_julius_caesar.txt",
                "data/shakespeare_king_lear.txt",
                "data/shakespeare_macbeth.txt",
                "data/shakespeare_merchant_of_venice.txt",
                "data/shakespeare_midsummer_nights_dream.txt",
                "data/shakespeare_much_ado.txt",
                "data/shakespeare_othello.txt",
                "data/shakespeare_romeo_and_juliet.txt",
               };

            //=============================================================
            // YOUR IMPLEMENTATION HERE TO COUNT WORDS IN SINGLE AND MULTIPLE THREADS
            //=============================================================
            Console.WriteLine("Write 's' for single thread and 'm' for multi thread");
            mode = Console.ReadLine();
            if (mode == "s")
            {
                Console.WriteLine("Running Single Thread Implementation");
                Console.WriteLine("press enter");
                Console.ReadLine();
                // Single Thread Implementation
                foreach (string play in filenames)
                {
                    Console.WriteLine("For the play: " + play);
                    HelperFunctions.CountCharacterWords("D:/Documents/Fourth Year/CPEN 333/lab3/" + play, mutex, wordcountSingleThread);

                    newList = HelperFunctions.SortCharactersByWordcount(wordcountSingleThread);

                    HelperFunctions.PrintListofTuples(newList);
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                }
            }
            else if (mode == "m")
            {
                Console.WriteLine("Running Multi Thread Implementation");
                Console.WriteLine("press enter");
                Console.ReadLine();
                // Multi Thread Implementation
                foreach (string play in filenames)
                {
                    NewThread(play, mutex, wordcountMultiThread, newList);
                }
            }
            else
            {
                Console.WriteLine("Must type s or m. Good Bye");
            }

            Console.WriteLine("Done");
            return 0;
        }

        public static Thread NewThread(string filename, Mutex mutex, Dictionary<string, int> wcounts, List<Tuple<int, string>> printableList)
        {
            var t = new Thread(() => DoThings(filename, mutex, wcounts, printableList));
            t.Start();
            t.Join();
            //PrintArray(array);
            return t;
        }

        static void DoThings(string filename, Mutex mutex, Dictionary<string, int> wcounts, List<Tuple<int, string>> printableList)
        {
            Console.WriteLine("For the play: " + filename);
            HelperFunctions.CountCharacterWords("D:/Documents/Fourth Year/CPEN 333/lab3/" + filename, mutex, wcounts);

            printableList = HelperFunctions.SortCharactersByWordcount(wcounts);

            HelperFunctions.PrintListofTuples(printableList);
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
        }

        /**
        * Tests word_count for the given line and starting index
        * @param line line in which to search for words
        * @param start_idx starting index in line to search for words
        * @param expected expected answer
        * @throws UnitTestException if the test fails
        */
        static void WCTester(string line, int start_idx, int expected)
        {
            int result = 0;
            // TO DO
            // Call your WordCount(ref line, start_idx) method
            result = HelperFunctions.WordCount(ref line, start_idx);

            // if not what we expect, throw an error
            if (result != expected)
            {
                throw new UnitTestException(ref line, start_idx, result, expected, String.Format("UnitTestFailed: result:{0} expected:{1}, line: {2} starting from index {3}", result, expected, line, start_idx));
            }

        }

        /////////////////////////////////////////////////////////////////////////////////
        /// Code for WordCount testing ///////////
        /////////////////////////////////////////////////////////////////////////////////
        /*
            try
            {

                // To DO:
                // YOUR TESTS HERE. Create a large list which includes the line, the // starting index and the expected result. You would want to check //// all the edge case scenarios.
                string line = "The brown fox jumped over the lazy dog";
                int startIdx = 0;
                int expectedResults = 8;

                WCTester(line, startIdx, expectedResults);

            }
            catch (UnitTestException e)
            {
                Console.WriteLine(e);
            }
        */
    }
}
