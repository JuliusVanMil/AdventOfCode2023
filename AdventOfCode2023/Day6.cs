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
        var timeline = input
            .Split(Environment.NewLine)
            .First();
        var distanceline = input
            .Split(Environment.NewLine)
            .Last();

        var times = timeline.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..];
        var distances = distanceline.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[1..];
        return times.Select((time, index) => new Race(long.Parse(time), long.Parse(distances[index]))).ToList();
    }

    public static Race ParseRaces2(string input)
    {
        var timeline = input
            .Split(Environment.NewLine)
            .First();
        var distanceline = input
            .Split(Environment.NewLine)
            .Last();

        var time = timeline.Replace(" ", "").Split(':').Last();
        var distance = distanceline.Replace(" ", "").Split(':').Last();

        return new Race(long.Parse(time), long.Parse(distance));
    }

    public record Race(long Time, long Distance)
    {
        public long CalculateNumberOfWins()
        {
            return GetLastRace() - GetFirstRace() + 1;
        }

        public long GetFirstRace()
        {
            for (var i = 0L; i < Time; i++)
            {
                if (PressButton(i))
                    return i;
            }
            return 0;
        }

        public long GetLastRace()
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
