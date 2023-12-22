namespace AdventOfCode2023;

public class Day5
{
    public static string Part1()
    {
        var lines = File.ReadAllLines("Day5Input.txt");
        var seeds = ParseSeeds(lines);
        List<Map> maps = ParseMaps(lines);
        return seeds.Select(s => GetDestination(s, maps)).Min().ToString();
    }

    public static string Part2()
    {
        var lines = File.ReadAllLines("Day5Input.txt");
        return "";
    }

    private static List<long> ParseSeeds(string[] lines) => lines[0].Split(':')[1].Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse).ToList();

    private static List<Map> ParseMaps(string[] lines)
    {
        var ramainingLines = lines[2..];

        var seedToSoilLines = GetLinesAfterMap(ramainingLines);
        var seedToSoilMap = Map.Parse(seedToSoilLines);
        ramainingLines = SkipToNextMap(ramainingLines, seedToSoilLines);

        var soilToFertilizerLines = GetLinesAfterMap(ramainingLines);
        var soilToFertilizerMap = Map.Parse(soilToFertilizerLines);
        ramainingLines = SkipToNextMap(ramainingLines, soilToFertilizerLines);

        var fertilizerToWaterLines = GetLinesAfterMap(ramainingLines);
        var fertilizerToWaterMap = Map.Parse(fertilizerToWaterLines);
        ramainingLines = SkipToNextMap(ramainingLines, fertilizerToWaterLines);

        var waterToLightLines = GetLinesAfterMap(ramainingLines);
        var waterToLightMap = Map.Parse(waterToLightLines);
        ramainingLines = SkipToNextMap(ramainingLines, waterToLightLines);

        var lightToTemperatureLines = GetLinesAfterMap(ramainingLines);
        var lightToTemperatureMap = Map.Parse(lightToTemperatureLines);
        ramainingLines = SkipToNextMap(ramainingLines, lightToTemperatureLines);

        var temperatureToHumidityLines = GetLinesAfterMap(ramainingLines);
        var temperatureToHumidityMap = Map.Parse(temperatureToHumidityLines);
        ramainingLines = SkipToNextMap(ramainingLines, temperatureToHumidityLines);

        var humidityToLocationLines = GetLinesAfterMap(ramainingLines);
        var humidityToLocationMap = Map.Parse(humidityToLocationLines);

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

    private static long GetDestination(long source, List<Map> maps)
    {
        return maps.Aggregate(source, (distance, map) => map.MapToDestination(distance));
    }

    public record Map(List<RangeMap> RangeMaps) {
        public long MapToDestination(long source) {
            var rangeMap = RangeMaps.FirstOrDefault(r => r.IsInRange(source));
            return rangeMap == null ? source : rangeMap.DestinationStart + (source - rangeMap.SourceStart);
        }

        public static Map Parse(string[] lines)
        {
            var rangemaps = lines.Select(l => RangeMap.Parse(l)).ToList();
            return new Map(rangemaps);
        }
    }

    public record RangeMap(long DestinationStart, long SourceStart, long Length)
    {
        public bool IsInRange(long source) => source >= SourceStart && source <= SourceStart + Length;
        public static RangeMap Parse(string line)
        {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return new RangeMap(long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
        }
    }
}
