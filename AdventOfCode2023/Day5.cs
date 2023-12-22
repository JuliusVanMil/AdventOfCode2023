namespace AdventOfCode2023;

public class Day5
{
    public static string Part1()
    {
        var lines = File.ReadAllLines("Day5Input.txt");
        var seeds = lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToList();
        List<Map> maps = ParseMaps(lines);
        return seeds.Select(s => GetDistance(s, maps)).Min().ToString();
    }

    public static string Part2()
    {
        var lines = File.ReadAllLines("Day5Input.txt");
        return "";
    }

    private static List<Map> ParseMaps(string[] lines)
    {
        var ramainingLines = lines.Skip(2).ToArray();

        var seedToSoilLines = GetLinesAfterMap(ramainingLines);
        var seedToSoilMap = ParseMap(seedToSoilLines);
        ramainingLines = SkipToNextMap(ramainingLines, seedToSoilLines);

        var soilToFertilizerLines = GetLinesAfterMap(ramainingLines);
        var soilToFertilizerMap = ParseMap(soilToFertilizerLines);
        ramainingLines = SkipToNextMap(ramainingLines, soilToFertilizerLines);

        var fertilizerToWaterLines = GetLinesAfterMap(ramainingLines);
        var fertilizerToWaterMap = ParseMap(fertilizerToWaterLines);
        ramainingLines = SkipToNextMap(ramainingLines, fertilizerToWaterLines);

        var waterToLightLines = GetLinesAfterMap(ramainingLines);
        var waterToLightMap = ParseMap(waterToLightLines);
        ramainingLines = SkipToNextMap(ramainingLines, waterToLightLines);

        var lightToTemperatureLines = GetLinesAfterMap(ramainingLines);
        var lightToTemperatureMap = ParseMap(lightToTemperatureLines);
        ramainingLines = SkipToNextMap(ramainingLines, lightToTemperatureLines);

        var temperatureToHumidityLines = GetLinesAfterMap(ramainingLines);
        var temperatureToHumidityMap = ParseMap(temperatureToHumidityLines);
        ramainingLines = SkipToNextMap(ramainingLines, temperatureToHumidityLines);

        var humidityToLocationLines = GetLinesAfterMap(ramainingLines);
        var humidityToLocationMap = ParseMap(humidityToLocationLines);

        return new List<Map> { seedToSoilMap, soilToFertilizerMap, fertilizerToWaterMap, waterToLightMap, lightToTemperatureMap, temperatureToHumidityMap, humidityToLocationMap };
    }

    private static string[] GetLinesAfterMap(string[] ramainingLines)
    {
        return ramainingLines.SkipWhile(l => !l.Contains("map")).Skip(1).TakeWhile(l => !l.Contains("map") && !string.IsNullOrWhiteSpace(l)).ToArray();
    }

    private static string[] SkipToNextMap(string[] ramainingLines, string[] seedToSoilLines)
    {
        return ramainingLines.Skip(seedToSoilLines.Length + 2).ToArray();
    }

    private static Map ParseMap(string[] lines)
    {
        var map = new Map();
        foreach (var line in lines)
        {
            map.RangeMaps.Add(ParseRangeMap(line));
        }
        return map;
    }

    private static RangeMap ParseRangeMap(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return new RangeMap(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
    }

    private static long GetDistance(long source, List<Map> maps)
    {
        return maps.Aggregate(source, (distance, map) => map.MapToDestination(distance));
    }

    public class Map {
        public List<RangeMap> RangeMaps { get; set; } = new List<RangeMap>();
        public long MapToDestination(long source) {
            var rangeMap = RangeMaps.FirstOrDefault(r => r.IsInRange(source));
            return rangeMap == null ? source : rangeMap.DestinationStart + (source - rangeMap.SourceStart);
        }
    }

    public record RangeMap(long DestinationStart, long SourceStart, long Length)
    {
        public bool IsInRange(long source) => source >= SourceStart && source <= SourceStart + Length;
    }
}
