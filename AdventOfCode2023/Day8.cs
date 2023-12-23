namespace AdventOfCode2023;

public class Day8
{
    public static string Part1()
    {
        var lines = File.ReadAllLines("Day8Input.txt");
        var directions = Directions.Parse(lines);
        var count = directions.CountAmountOfInstructionsToReachEnd(startNodeName: "AAA", name => name == "ZZZ");
        return count.ToString();
    }

    public static string Part2()
    {
        var lines = File.ReadAllLines("Day8Input.txt");
        var directions = Directions.Parse(lines);
        var count = directions.CountAmountOfInstructionsToReachEndAsGhost();
        return count.ToString();
    }

    public enum Instruction
    {
        Left = 'L',
        Right = 'R'
    }

    public record Directions(List<Instruction> Instructions, Dictionary<string, Node> Nodes)
    {
        public long CountAmountOfInstructionsToReachEnd(string startNodeName, Func<string, bool> hasDestinationBeenReached)
        {
            var currentNode = Nodes[startNodeName];
            var totalInstructions = Instructions.Count;
            long count = 0;
            var instructionIndex = 0;

            while (!hasDestinationBeenReached(currentNode.Name))
            {
                var currentInstruction = Instructions[instructionIndex];
                var nextNode = GetNextNode(currentNode, currentInstruction);
                currentNode = nextNode;
                instructionIndex = (instructionIndex + 1) % totalInstructions;
                count++;
            }
            return count;
        }

        public long CountAmountOfInstructionsToReachEndAsGhost()
        {
            var startingNodes = Nodes.Where(x => x.Value.Name.EndsWith('A')).ToList();
            var counts = startingNodes.Select(x => CountAmountOfInstructionsToReachEnd(x.Key, name => name.EndsWith('Z'))).ToArray();
            return counts.Aggregate(MathHelper.LeastCommonMultiple);
        }

        private Node GetNextNode(Node currentNode, Instruction currentInstruction)
        {
            var nextNodeName = currentInstruction switch
            {
                Instruction.Left => currentNode.Left,
                Instruction.Right => currentNode.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(currentInstruction), currentInstruction, null)
            };
            return Nodes[nextNodeName];
        }

        public static Directions Parse(string[] lines)
        {
            var instructions = lines[0].Select(x => (Instruction)x).ToList();
            var nodes = lines[2..].Select(x => Node.Parse(x)).ToDictionary(x => x.Name);
            return new Directions(instructions, nodes);
        }
    }

    public record Node(string Name, string Left, string Right)
    {
        public static Node Parse(string line)
        {
            var parts = line.Split("=", StringSplitOptions.TrimEntries);
            var name = parts[0].Trim();
            var children = parts[1].Split(",", StringSplitOptions.TrimEntries).Select(x => RemoveParenthesis(x)).ToList();
            return new Node(name, children[0], children[1]);
        }

        private static string RemoveParenthesis(string x)
        {
            return x.Replace("(", "").Replace(")", "");
        }
    }

    public static class MathHelper
    {
        public static long LeastCommonMultiple(long a, long b)
        {
            return a * b / GreatestCommonDivisor(a, b);
        }

        public static long GreatestCommonDivisor(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
