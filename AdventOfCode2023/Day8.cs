namespace AdventOfCode2023;

public class Day8
{
    public static string Part1()
    {
        var lines = File.ReadAllLines("Day8Input.txt");
        var directions = Directions.Parse(lines);
        var count = directions.CountAmountOfInstructionsToReachEnd();
        return count.ToString();
    }

    public static string Part2()
    {
        var lines = File.ReadAllLines("Day8Input.txt");
        return "";
    }

    public enum Instruction
    {
        Left = 'L',
        Right = 'R'
    }

    public record Directions(List<Instruction> Instructions, List<Node> Nodes) {
        public long CountAmountOfInstructionsToReachEnd(string lastNodeName = "ZZZ")
        {
            var currentNode = Nodes.First();
            var totalInstructions = Instructions.Count;
            var instructionCount = 0;
            long count = 0;
            while (currentNode.Name != lastNodeName)
            {
                var currentInstruction = Instructions[instructionCount];
                var nextNode = GetNextNode(currentNode, currentInstruction);
                currentNode = nextNode;

                instructionCount = (instructionCount + 1) % totalInstructions;
                count++;
            }
            return count;
        }

        private Node GetNextNode(Node currentNode, Instruction currentInstruction)
        {
            var nextNodeName = currentInstruction switch
            {
                Instruction.Left => currentNode.Children.First(),
                Instruction.Right => currentNode.Children.Last(),
                _ => throw new ArgumentOutOfRangeException(nameof(currentInstruction), currentInstruction, null)
            };
            return Nodes.First(x => x.Name == nextNodeName);
        }

        public static Directions Parse(string[] lines)
        {
            var instructions = lines[0].Select(x => (Instruction)x).ToList();
            var nodes = lines[2..].Select(x => Node.Parse(x)).ToList();
            return new Directions(instructions, nodes);
        }
    }

    public record Node(string Name, List<string> Children)
    {
        public static Node Parse(string line)
        {
            var parts = line.Split("=", StringSplitOptions.TrimEntries);
            var name = parts[0].Trim();
            var children = parts[1].Split(",", StringSplitOptions.TrimEntries).Select(x => RemoveParenthesis(x)).ToList();
            return new Node(name, children);
        }

        private static string RemoveParenthesis(string x)
        {
            return x.Replace("(", "").Replace(")", "");
        }
    }
}
