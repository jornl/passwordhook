namespace PasswordHook
{
    public class WordList
    {
        public Dictionary<string, int> wordList { get; }

        public WordList(string wordListPath)
        {
            wordList = new(StringComparer.OrdinalIgnoreCase);

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
                        throw new FormatException("Invalid line format: " + line);
                    }

                    string word = parts[0].Trim();
                    if (int.TryParse(parts[1], out int numericValue) == false)
                    {
                        throw new FormatException("Invalid numeric value: " + parts[1]);
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
                throw new Exception("Error reading word list file.", e);
            }
        }
    }
}