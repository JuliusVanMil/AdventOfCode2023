namespace AdventOfCode2023;

public class Day2
{
    public static string Part1()
    {
        int maxRed = 12;
        int maxGreen = 13;
        int maxBlue = 14;
        var lines = File.ReadAllLines("Day2Input.txt");
        var games = lines.Select(ParseGame).ToList();
        games.RemoveAll(g => g.Red > maxRed || g.Green > maxGreen || g.Blue > maxBlue);
        return games.Sum(g => g.Id).ToString();
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
    }
}
