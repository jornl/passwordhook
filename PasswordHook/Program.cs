
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
                Console.WriteLine("Word list file not found.");
                return;
            }

            if (!(wordListPath.EndsWith(".txt", StringComparison.OrdinalIgnoreCase)
                || wordListPath.EndsWith(".csv", StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Word list file must be a .txt or .csv file.");
                return;
            }

            var wordList = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

            try
            {
                string[] lines = File.ReadAllLines(wordListPath);

                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }
                    string[] parts = line.Split(',');
                    if (parts.Length != 2)
                    {
                        Console.WriteLine($"Invalid line format: {line} (should be word,value).");
                        return;
                    }

                    string word = parts[0].Trim();
                    if (int.TryParse(parts[1], out int numericValue) == false)
                    {
                        Console.WriteLine($"Invalid value format: {parts[1]} for {word} (should be a numeric value).");
                        return;
                    }

                    if (wordList.ContainsKey(word))
                    {
                        continue;
                    }


                    wordList.Add(word, numericValue);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error reading word list file: " + e.Message);
                return;
            }

            foreach (var pair in wordList)
            {
                Console.WriteLine($"Word: {pair.Key}, value: {pair.Value}");
            }

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