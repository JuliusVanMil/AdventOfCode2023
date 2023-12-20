using System.Text;

namespace AdventOfCode2023
{
    public class Day3
    {
        public static string Part1()
        {
            var lines = File.ReadAllLines("Day3Input.txt");
            var adjacentNumbers = FindAdjacentNumbersInLines(lines);
            return adjacentNumbers.Sum().ToString();
        }

        private static List<int> FindAdjacentNumbersInLines(string[] lines)
        {
            var adjacentNumbers = new List<int>();

            for (int i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];
                string previousLine = i > 0 ? lines[i - 1] : "";
                string nextLine = i < lines.Length - 1 ? lines[i + 1] : "";
                adjacentNumbers.AddRange(currentLine.FindNumbersAdjacentToLine(previousLine));
                adjacentNumbers.AddRange(currentLine.FindNumbersAdjacentToLine(currentLine));
                adjacentNumbers.AddRange(currentLine.FindNumbersAdjacentToLine(nextLine));
            }

            return adjacentNumbers;
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

            for (int j = currentSymbolIndex - 1; j >= 0 && char.IsDigit(current[j]); j--)
            {
                sb.Insert(0, current[j]);
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

            for (int j = currentSymbolIndex + 1; j < current.Length && char.IsDigit(current[j]); j++)
            {
                sb.Append(current[j]);
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
