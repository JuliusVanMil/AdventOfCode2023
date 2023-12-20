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
            return GetSymbolIndices(line).Select(i => FindNumbersAdjacentToIndex(current, i)).SelectMany(x => x).ToList();
        }

        private static IEnumerable<int> FindNumbersAdjacentToIndex(string current, int currentSymbolIndex)
        {
            var numbers = new List<int>();
            var isCurrentCharacterNumber = char.IsDigit(current[currentSymbolIndex]);
            var numberBefore = ExtractNumber(current, currentSymbolIndex, -1, out var foundNumberBefore);
            var numberAfter = ExtractNumber(current, currentSymbolIndex, 1, out var foundNumberAfter);

            if (isCurrentCharacterNumber)
            {
                numbers.Add(int.Parse($"{numberBefore}{current[currentSymbolIndex]}{numberAfter}"));
            }
            else
            {
                if (foundNumberBefore)
                    numbers.Add(int.Parse($"{numberBefore}"));

                if (foundNumberAfter)
                    numbers.Add(int.Parse($"{numberAfter}"));
            }

            return numbers;
        }

        private static string ExtractNumber(string line, int startIndex, int step, out bool foundNumber)
        {
            var sb = new StringBuilder();
            for (int j = startIndex + step; j >= 0 && j < line.Length && char.IsDigit(line[j]); j += step)
            {
                if (step > 0)
                    sb.Append(line[j]);
                else
                    sb.Insert(0, line[j]);
            }
            foundNumber = sb.Length > 0;
            return sb.ToString();
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
