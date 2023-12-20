using System.Text;

namespace AdventOfCode2023
{
    public class Day3
    {
        public static string Part1()
        {
            var lines = File.ReadAllLines("Day3Input.txt");
            var adjacentNumbers = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {
                string current = lines[i];
                string previous = i > 0 ? lines[i - 1] : "";
                string next = i < lines.Length - 1 ? lines[i + 1] : "";
                adjacentNumbers.AddRange(current.FindNumbersAdjacentToLine(previous));
                adjacentNumbers.AddRange(current.FindNumbersAdjacentToLine(current));
                adjacentNumbers.AddRange(current.FindNumbersAdjacentToLine(next));
            }

            return adjacentNumbers.Sum().ToString();
        }
    }

    public static class Day3Extensions {

        public static IEnumerable<int> FindNumbersAdjacentToLine(this string current, string line)
        {
            var adjacentNumbers = new List<int>();
            var symbolIndices = GetSymbolIndices(line);

            foreach (var symbolIndex in symbolIndices)
            {
                adjacentNumbers.AddRange(TraverseCharacters(current, symbolIndex));
            }

            return adjacentNumbers;
        }

        private static IEnumerable<int> TraverseCharacters(string current, int currentSymbolIndex)
        {
            var numbers = new List<int>();
            var sb = new StringBuilder();

            for (int j = currentSymbolIndex - 1; j >= 0; j--)
            {
                var currentChar = current[j];

                if (char.IsDigit(currentChar))
                {
                    sb.Insert(0, currentChar);
                }
                else
                {
                    break;
                }
            }

            var foundNumber = sb.Length > 0;
            var isCurrentCharacterNumber = char.IsDigit(current[currentSymbolIndex]);

            if (isCurrentCharacterNumber)
            {
                sb.Append(current[currentSymbolIndex]);
            }

            if (!isCurrentCharacterNumber && foundNumber)
            {
                numbers.Add(int.Parse(sb.ToString()));
                sb.Clear();
            }

            for (int j = currentSymbolIndex + 1; j < current.Length; j++)
            {
                var currentChar = current[j];

                if (char.IsDigit(currentChar))
                {
                    sb.Append(currentChar);
                }
                else
                {
                    break;
                }
            }

            if (sb.Length > 0)
            {
                numbers.Add(int.Parse(sb.ToString()));
                sb.Clear();
            }

            return numbers;
        }

        private static List<int> GetSymbolIndices(string line)
        {
            return line
                .Select((character, index) => new { character, index })
                .Where(x => !char.IsLetterOrDigit(x.character) && x.character != '.')
                .Select(c => c.index)
                .ToList();
        }
    }
}
