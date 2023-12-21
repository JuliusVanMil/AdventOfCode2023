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
        var lines = File.ReadAllLines("Day4Input.txt");
        var scratchcards = lines.Select(ParseScratchcard).ToList();
        for (int i = 0; i < scratchcards.Count; i++)
        {
            var scratchcard = scratchcards.ElementAt(i);
            for (int j = 1; j <= scratchcard.AmountOfWinningNumbers; j++)
            {
                for (int k = 0; k < scratchcard.AmountOfCopies; k++)
                {
                    var scratchcardToCopy = scratchcards.ElementAt(i + j);
                    scratchcardToCopy.Copy();
                }
            }
        }
        return scratchcards.Select(x => x.AmountOfCopies).Sum().ToString();
    }

    public static Scratchcard ParseScratchcard(string line)
    {
        var numbers = line.Split(':', StringSplitOptions.TrimEntries)[1].Split('|', StringSplitOptions.TrimEntries);
        var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
        var numbersToCheck = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
        return new Scratchcard(winningNumbers, numbersToCheck);
    }

    public class Scratchcard {
        public Scratchcard(IEnumerable<int> winningNumbers, IEnumerable<int> numbersToCheck)
        {
            WinningNumbers = winningNumbers;
            NumbersToCheck = numbersToCheck;
        }
        public IEnumerable<int> WinningNumbers { get; set; }
        public IEnumerable<int> NumbersToCheck { get; set; }
        public int AmountOfCopies { get; set; } = 1;
        public bool IsWinning => WinningNumbers.Any(NumbersToCheck.Contains);
        public int AmountOfWinningNumbers => WinningNumbers.Count(NumbersToCheck.Contains);
        public double Value => IsWinning ? Math.Pow(2, AmountOfWinningNumbers - 1) : 0;
        public void Copy()
        {
            AmountOfCopies++;
        }
    }
}
