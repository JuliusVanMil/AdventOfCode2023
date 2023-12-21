namespace AdventOfCode2023;

public class Day4
{
    public static string Part1()
    {
        var lines = File.ReadAllLines("Day4Input.txt");
        return lines.Select(ParseScratchcard).Select(x => x.Value).Sum().ToString();
    }

    public static string Part2()
    {
        return "";
    }

    public static Scratchcard ParseScratchcard(string line)
    {
        var numbers = line.Split(':', StringSplitOptions.TrimEntries)[1].Split('|', StringSplitOptions.TrimEntries);
        var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
        var numbersToCheck = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
        return new Scratchcard(winningNumbers, numbersToCheck);
    }

    public record Scratchcard(IEnumerable<int> WinningNumbers, IEnumerable<int> NumbersToCheck) {
        public bool IsWinning => WinningNumbers.Any(NumbersToCheck.Contains);
        public int AmountOfWinningNumbers => WinningNumbers.Count(NumbersToCheck.Contains);
        public double Value => IsWinning ? Math.Pow(2, AmountOfWinningNumbers - 1) : 0;
    }
}
