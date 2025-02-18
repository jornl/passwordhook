using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace PasswordHook
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine(Utility.PrintUsage());
                return;
            }

            string wordListPath = args[0];
            if (!File.Exists(wordListPath))
            {
                throw new FileNotFoundException("Word list file not found.", wordListPath);
            }

            if (!(wordListPath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)
                || wordListPath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)))
            {
                throw new FormatException("Word list file must be a .txt or .csv file.");
            }

            var wordList = new WordList(wordListPath).wordList;

            if (wordList.TryGetValue(args[1], out int value))
            {
                Console.WriteLine("");
                Console.WriteLine($"Found the word {args[1]} in the list, its value: {value}");
            }
            else
            {
                Console.WriteLine($"Word {args[1]} not found in word list.");
            }
        }


    }
}