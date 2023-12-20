using System.Data.SqlTypes;

namespace AdventOfCode2023;

public class Day2
{
    public static string Part1()
    {
        var lines = File.ReadAllLines("Day2Input.txt");
        var games = lines.Select(ParseGame).ToList();
        games.RemoveAll(g => g.Red > 12 || g.Green > 13 || g.Blue > 14);
        return games.Sum(g => g.Id).ToString();
    }

    public static string Part2()
    {
        var lines = File.ReadAllLines("Day2Input.txt");
        var games = lines.Select(ParseGame).ToList();
        return games.Sum(g => g.Power).ToString();
    }

    private static Game ParseGame(string line)
    {
        var game = new Game();
        var parts = line.Split(": ");
        game.Id = int.Parse(parts[0].Split(" ")[1]);
        var gameRecords = parts[1].Split("; ");
        foreach (var record in gameRecords)
        {
            var recordParts = record.Split(", ");
            foreach (var recordPart in recordParts)
            {
                var colorSet = recordPart.Split(" ");
                var value = int.Parse(colorSet[0]);
                var color = colorSet[1];
                switch (color)
                {
                    case "red":
                        if (value > game.Red)
                            game.Red = value;
                        break;
                    case "green":
                        if (value > game.Green)
                            game.Green = value;
                        break;
                    case "blue":
                        if (value > game.Blue)
                            game.Blue = value;
                        break;
                }
            }
        }
        return game;
    }

    public class Game {
        public int Id { get; set; }
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
        public int Power => Red * Green * Blue;
    }
}
