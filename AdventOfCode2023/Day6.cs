namespace AdventOfCode2023;

public class Day6
{
    public static long Part1()
    {
        var input = File.ReadAllText("Day6Input.txt");
        return ParseRaces(input)
            .Select(x => x.CalculateNumberOfWins())
            .Aggregate((x, y) => x * y);
    }

    public static long Part2()
    {
        var input = File.ReadAllText("Day6Input.txt");
        var race = ParseRaces2(input);
        return race.CalculateNumberOfWins();
    }

    public static List<Race> ParseRaces(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var times = lines[0].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..];
        var distances = lines[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..];
        return times.Zip(distances, (time, distance) => new Race(long.Parse(time), long.Parse(distance))).ToList();
    }

    public static Race ParseRaces2(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var time = lines[0].Split(':').Last().Replace(" ", "");
        var distance = lines[1].Split(':').Last().Replace(" ", "");;
        return new Race(long.Parse(time), long.Parse(distance));
    }

    public record Race(long Time, long Distance)
    {
        public long CalculateNumberOfWins() => GetLastWinningRace() - GetFirstWinningRace() + 1;

        public long GetFirstWinningRace()
        {
            for (var i = 0L; i < Time; i++)
            {
                if (PressButton(i))
                    return i;
            }
            return 0;
        }

        public long GetLastWinningRace()
        {
            for (var i = Time; i > 0; i--)
            {
                if (PressButton(i))
                    return i;
            }
            return 0;
        }

        public bool PressButton(long pressedTime)
        {
            var timeLeft = Time - pressedTime;
            var travelledDistance = timeLeft * pressedTime;
            return travelledDistance > Distance;
        }
    }
}
