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

            // Now calculate the score
            int score = CalculateScore(args[1], wordList);

            // Print the result
            Console.WriteLine($"Score for '{args[1]}' = {score}");

            if (score >= 10)
            {
                // Change the password.
                Environment.Exit(0);
            } else {
                Environment.Exit(1);
            }
        }
        public static int CalculateScore(string input, Dictionary<string, int> wordList)
        {
            int totalScore = 0;
            int i = 0;

            while (i < input.Length)
            {
                bool matchedDictionaryWord = false;

                // 1) Try to match dictionary words first
                foreach (var kvp in wordList)
                {
                    string dictWord = kvp.Key;
                    int dictValue = kvp.Value;

                    // Make sure we don't go out of range
                    if (i + dictWord.Length <= input.Length)
                    {
                        // Extract a substring of the same length as dictWord
                        string candidate = input.Substring(i, dictWord.Length);

                        // Case-insensitive compare
                        if (candidate.Equals(dictWord, StringComparison.OrdinalIgnoreCase))
                        {
                            totalScore += dictValue;  // Add the dictionary word's value
                            i += dictWord.Length;     // Move past this word
                            matchedDictionaryWord = true;
                            break;                    // Stop checking other dictionary words
                        }
                    }
                }

                // 2) If no dictionary word matched at position i, handle repeated characters
                if (!matchedDictionaryWord)
                {
                    // We'll count how many identical characters appear from position i forward
                    char currentChar = input[i];
                    int runLength = 1;
                    int j = i + 1;

                    while (j < input.Length && input[j] == currentChar)
                    {
                        runLength++;
                        j++;
                    }

                    // Now we have a run of `runLength` identical characters
                    if (runLength <= 3)
                    {
                        // For runs of up to 3, score = runLength
                        totalScore += runLength;
                    }
                    else
                    {
                        // For runs of 4 or more, score = floor(runLength / 2)
                        totalScore += runLength / 2;  // integer division automatically floors
                    }

                    // Move 'i' forward by the length of that run
                    i += runLength;
                }
            }

            return totalScore;
        }


    }
}