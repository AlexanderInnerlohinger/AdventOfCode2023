using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.VisualBasic;

namespace AdventOfCode2023.Days
{
    internal class CamelCards
    {
        internal enum CardType
        {
            Flush,
            Poker,
            FullHouse,
            ThreeOfaKind,
            TwoPair,
            OnePair,
            HighCard,
            Undefined
        }

        public static void Run()
        {
            Console.WriteLine("CamelCards-method is being run now...\n\n");
            string pathInput = "/adventofcode.com_2023_day_7_input.txt";

            string[] input = File.ReadAllLines(Directory.GetCurrentDirectory() + pathInput);
            Dictionary<CardType, List<CamelLine> >handDictionary = new Dictionary<CardType, List<CamelLine>>()
            {
                {CardType.Undefined, new List<CamelLine>()},
                {CardType.HighCard, new List<CamelLine>()},
                {CardType.OnePair, new List<CamelLine>()},
                {CardType.TwoPair, new List<CamelLine>()},
                {CardType.ThreeOfaKind, new List<CamelLine>()},
                {CardType.FullHouse, new List<CamelLine>()},
                {CardType.Poker, new List<CamelLine>()},
                {CardType.Flush, new List<CamelLine>()}
            };

            // Fill dictionary with all hands and bids
            foreach (string s in input)
            {
                char[] hand = s.Split(' ')[0].ToArray();
                int bid = Convert.ToInt32(s.Split(' ')[1]);

                CardType handCardType = GetHandType(hand);
                Debug.WriteLine($"For {new string(hand)} a cardType of {handCardType} was found.");

                handDictionary[handCardType].Add(new CamelLine(hand, bid, handCardType));
            }

            // Sort dictionary acceding according to poker rules
            foreach (KeyValuePair<CardType, List<CamelLine>> kvp in handDictionary)
                kvp.Value.Sort((x,y) => CustomCompare(x.Hand, y.Hand));

            // Now add all camelLines to a list to calculate final result
            List<CamelLine> resultList = new List<CamelLine>();
            foreach (KeyValuePair<CardType, List<CamelLine>> kvp in handDictionary)
                foreach (CamelLine camelLine in kvp.Value)
                    resultList.Add(camelLine);

            int totalWinnings = 0;
            for (int i = 0; i < resultList.Count; i++)
            {
                totalWinnings += resultList[i].Bid * (i + 1);
                Console.WriteLine($"{resultList[i].Type} {new string(resultList[i].Hand)} {resultList[i].Bid}");
                Console.WriteLine($"Added {resultList[i].Bid} times {i+1} to the total winnings.\n");
            }

            Console.WriteLine($"Total winnings resulted in {totalWinnings}.");
        }

        private static int CustomCompare(char[] xHand, char[] yHand)
        {
            string customOrder = "23456789TJQKA";

            // Vergleiche die Indizes der Buchstaben in der benutzerdefinierten Reihenfolge
            for (int i = 0; i < Math.Min(xHand.Length, yHand.Length); i++)
            {
                int indexX = customOrder.IndexOf(xHand[i]);
                int indexY = customOrder.IndexOf(yHand[i]);

                if (indexX != indexY)
                {
                    return indexX.CompareTo(indexY);
                }
            }

            return xHand.Length.CompareTo(yHand.Length);
        }

        private static CardType GetHandType(char[] hand)
        {
            CardType cardType = CardType.Undefined;

            // Look for types from weakest to strongest and override accordingly
            if (hand.Distinct().Count().Equals(5))
                cardType = CardType.HighCard;

            if (hand.Distinct().Count().Equals(4))
                cardType = CardType.OnePair;

            // Could be TwoPair or ThreeOfaKind
            if (hand.Distinct().Count().Equals(3))
            {
                // Create dictionary with count of unique elements
                var distinctElements = hand.Distinct();

                Dictionary<char, int> elementCounter = new Dictionary<char, int>();
                foreach (char element in distinctElements)
                {
                    foreach (char card in hand)
                    {
                        if (element.Equals(card))
                        {
                            elementCounter.TryAdd(element, 0);
                            elementCounter[element] += 1;
                        }
                    }
                }

                if (elementCounter.Values.Max() == 3)
                    cardType = CardType.ThreeOfaKind;
                else
                    cardType = CardType.TwoPair;
            }

            // Could be FullHouse or Poker
            if (hand.Distinct().Count().Equals(2))
            {
                // Create dictionary with count of unique elements
                var distinctElements = hand.Distinct();

                Dictionary<char, int> elementCounter = new Dictionary<char, int>();
                foreach (char element in distinctElements)
                {
                    foreach (char card in hand)
                    {
                        if (element.Equals(card))
                        {
                            elementCounter.TryAdd(element, 0);
                            elementCounter[element] += 1;
                        }
                    }
                }

                if (elementCounter.Values.Max() == 4)
                    cardType = CardType.Poker;
                else
                    cardType = CardType.FullHouse;
            }

            if (hand.Distinct().Count().Equals(1))
                cardType = CardType.Flush;

            return cardType;
        }
    }

    internal class CamelLine
    {
        public char[] Hand {get; set; }
        public int Bid { get; set; }
        public CamelCards.CardType Type { get; set; }

        public CamelLine(char[] hand, int bid, CamelCards.CardType type)
        {
            Hand = hand;
            Bid = bid;
            Type = type;
        }
    }
}
