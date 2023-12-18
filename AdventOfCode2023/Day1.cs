namespace AdventOfCode2023;

public class Day1
{
    public static int Part1()
    {
        var lines = File.ReadAllLines("Day1Input.txt");
        var sum = 0;
        foreach (var line in lines)
        {
            var digits = line.Where(char.IsDigit).ToArray();
            var first = digits.First();
            var last = digits.Last();
            sum += int.Parse($"{first}{last}");
        }
        return sum;
    }

    public static int Part2()
    {
        var lines = File.ReadAllLines("Day1Input.txt");
        var numbers = new List<string> { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        var sum = 0;

        foreach(var line in lines)
        {
            var digits = line.Where(char.IsDigit).ToArray();
            var hasDigits = digits.Any();
            string first = hasDigits ? digits.First().ToString() : "";
            string last = hasDigits ? digits.Last().ToString() : "";
            int firstDigitIndex = hasDigits ? line.IndexOf(first) : -1;
            int lastDigitIndex= hasDigits ? line.LastIndexOf(last) : -1;

            var firstIndices = numbers.Select(n => line.IndexOf(n)).ToList();
            var lastIndices = numbers.Select(n => line.LastIndexOf(n)).ToList();
            if (firstIndices.Any(i => i >= 0))
            {
                var lowestIndex = firstIndices.Where(x => x >= 0).Min();
                var highestIndex = lastIndices.Where(x => x >= 0).Max();
                if (firstDigitIndex == -1 || lowestIndex < firstDigitIndex)
                {
                    first = firstIndices.IndexOf(lowestIndex).ToString();
                }
                if (lastDigitIndex == -1 || highestIndex > lastDigitIndex)
                {
                    last = lastIndices.IndexOf(highestIndex).ToString();
                }
            }
            Console.WriteLine($"{line}: {first}{last}");
            sum += int.Parse($"{first}{last}");
        }
        return sum;
    }
}
