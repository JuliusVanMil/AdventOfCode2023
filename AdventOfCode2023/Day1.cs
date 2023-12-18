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
}
