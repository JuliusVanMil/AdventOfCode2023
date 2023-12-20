using System.Text;

namespace AdventOfCode2023
{
    public class Day3
    {
        public static string Part1()
        {
            var lines = File.ReadAllLines("Day3Input.txt");
            var adjacentNumbers = FindNumbersAdjacentToSymbolsInLines(lines);
            return adjacentNumbers.Sum().ToString();
        }

        public static string Part2()
        {
            var lines = File.ReadAllLines("Day3Input.txt");
            var gearRatios = FindAllGearRatiosInLines(lines);
            return gearRatios.Sum().ToString();
        }

        private static List<int> FindNumbersAdjacentToSymbolsInLines(string[] lines)
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

        private static List<long> FindAllGearRatiosInLines(string[] lines)
        {
            var gearRatios = new List<long>();

            for (int i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];
                string previousLine = i > 0 ? lines[i - 1] : "";
                string nextLine = i < lines.Length - 1 ? lines[i + 1] : "";
                var gearIndices = currentLine.GetGearIndices();
                var numbersAdjacentToGear = gearIndices.Select(gearIndex =>
                {
                    var numbersPreviousLine = previousLine.FindNumbersAdjacentToGear(gearIndex);
                    var numbersCurrentLine = currentLine.FindNumbersAdjacentToGear(gearIndex);
                    var numbersNextLine = nextLine.FindNumbersAdjacentToGear(gearIndex);
                    return numbersPreviousLine.Concat(numbersCurrentLine).Concat(numbersNextLine).ToList();
                }).Where(x => x.Count == 2).Select(x => (long)x[0] * (long)x[1]).ToList();
                gearRatios.AddRange(numbersAdjacentToGear);
            }

            return gearRatios;
        }
    }

    public static class Day3Extensions {

        public static IEnumerable<int> FindNumbersAdjacentToLine(this string current, string line)
        {
            return GetSymbolIndices(line).Select(index => FindNumbersAdjacentToIndex(current, index)).SelectMany(x => x).ToList();
        }

        public static IEnumerable<int> FindNumbersAdjacentToGear(this string line, int index)
        {
            return FindNumbersAdjacentToIndex(line, index).ToList();
        }

        private static IEnumerable<int> FindNumbersAdjacentToIndex(string current, int index)
        {
            var numbers = new List<int>();
            var isCurrentCharacterNumber = char.IsDigit(current[index]);
            var numberBefore = ExtractNumber(current, index, -1, out var foundNumberBefore);
            var numberAfter = ExtractNumber(current, index, 1, out var foundNumberAfter);

            if (isCurrentCharacterNumber)
            {
                numbers.Add(int.Parse($"{numberBefore}{current[index]}{numberAfter}"));
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

        public static List<int> GetGearIndices(this string line)
        {
            return line
                .Select((character, index) => new { character, index })
                .Where(x => x.character == '*')
                .Select(c => c.index)
                .ToList();
        }
    }
}
