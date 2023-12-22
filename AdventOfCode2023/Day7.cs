namespace AdventOfCode2023;

public class Day7
{
    public static string Part1()
    {
        var lines = File.ReadAllLines("Day7Input.txt");
        var hands = lines.Select(CardHand.Parse).Order().ToList();
        var winnings = hands.Select((h, i) => h.Bid * (i + 1)).Sum();
        return winnings.ToString();
    }

    public static string Part2()
    {
        return "Day 7, Part 2";
    }

    public record CardHand: IComparable<CardHand> {
        public string Cards { get; init; }
        public int Bid { get; init; }
        public HandType HandType { get; init; }
        private readonly Dictionary<char, int> _cardValues = new() {
            {'2', 2},
            {'3', 3},
            {'4', 4},
            {'5', 5},
            {'6', 6},
            {'7', 7},
            {'8', 8},
            {'9', 9},
            {'T', 10},
            {'J', 11},
            {'Q', 12},
            {'K', 13},
            {'A', 14}
        };

        public CardHand(string cards, int bid) {
            Cards = cards;
            Bid = bid;
            HandType = CalculateHandType();
        }

        public int CompareTo(CardHand? other) {
            if (other == null) return 1;
            if (HandType != other.HandType) return HandType.CompareTo(other.HandType);
            for (var i = 0; i < Cards.Length; i++) {
                var result = CompareCards(Cards[i], other.Cards[i]);
                if (result != 0) return result;
            }
            return 0;
        }

        public int CompareCards(char card1, char card2)
        {
            return _cardValues[card1].CompareTo(_cardValues[card2]);
        }

        private HandType CalculateHandType() {
            var groups = Cards.GroupBy(c => c).Select(g => g.Count()).OrderByDescending(c => c).ToList();
            if (groups[0] == 5) return HandType.FiveOfAKind;
            if (groups[0] == 4) return HandType.FourOfAKind;
            if (groups[0] == 3 && groups[1] == 2) return HandType.FullHouse;
            if (groups[0] == 3) return HandType.ThreeOfAKind;
            if (groups[0] == 2 && groups[1] == 2) return HandType.TwoPairs;
            if (groups[0] == 2) return HandType.OnePair;
            return HandType.HighCard;
        }

        public static CardHand Parse(string line) {
            var parts = line.Split(" ");
            var cards = parts[0];
            var bid = int.Parse(parts[1]);
            return new CardHand(cards, bid);
        }
    }

    public enum HandType {
        HighCard,
        OnePair,
        TwoPairs,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
}
