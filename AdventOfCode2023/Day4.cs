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
        scratchcards.Select((card, index) => new { card, index }).ToList()
            .ForEach(x => CopyNextCardsInLine(scratchcards, x.index, x.card));
        return scratchcards.Select(x => x.AmountOfCopies).Sum().ToString();
    }

    private static void CopyNextCardsInLine(List<Scratchcard> scratchcards, int i, Scratchcard scratchcard)
    {
        for (int j = 1; j <= scratchcard.AmountOfWinningNumbers; j++)
        {
            var scratchcardToCopy = scratchcards[i + j];
            scratchcard.CopyNextScratchcard(scratchcardToCopy);
        }
    }

    public static Scratchcard ParseScratchcard(string line)
    {
        var numbers = line.Split(':', StringSplitOptions.TrimEntries)[1].Split('|', StringSplitOptions.TrimEntries);
        var winningNumbers = numbers[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
        var numbersToCheck = numbers[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(int.Parse);
        return new Scratchcard(winningNumbers, numbersToCheck);
    }

    public record Scratchcard(IEnumerable<int> WinningNumbers, IEnumerable<int> NumbersToCheck)
    {
        public int AmountOfCopies { get; set; } = 1;
        public bool IsWinning => WinningNumbers.Any(NumbersToCheck.Contains);
        public int AmountOfWinningNumbers => WinningNumbers.Count(NumbersToCheck.Contains);
        public double Value => IsWinning ? Math.Pow(2, AmountOfWinningNumbers - 1) : 0;

        public void Copy()
        {
            AmountOfCopies++;
        }

        public void CopyNextScratchcard(Scratchcard scratchcardToCopy)
        {
            for (int k = 0; k < AmountOfCopies; k++)
            {
                scratchcardToCopy.Copy();
            }
        }
    }
}
