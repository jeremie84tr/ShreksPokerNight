using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Poker
{
    public class Hand {

        private List<Card> myCards;

        public Hand() {
            myCards = new List<Card>();
        }

        public void addCard(Card card) {
            myCards.Add(card);
        }

        public int CalculateScore(List<Card> dealerHand) {
            List<Card> allCards = new List<Card>();
            foreach (var card in myCards)
            {
                allCards.Add(card);  
            }
            foreach (var card in dealerHand)
            {
                allCards.Add(card);
            }
            return CalculateStraightFlushRoyale(allCards);
        }

        private int CalculateHighCard(List<Card> allCards) {//max = 13
            int max = 0;
            foreach (var card in allCards)
            {
                if (Card.Score[card.CardRank] > max) {
                    max = Card.Score[card.CardRank];
                }
            }
            return max;
        }

        private int CalculatePairs(List<Card> allCards) {//max = 41
            int valuePair = 14;
            
            List<Card> pairs = new List<Card>();
            for (int cardIndex = 0; cardIndex < allCards.Count - 2; cardIndex++)
            {
                for (int cardTested = cardIndex + 1; cardTested < allCards.Count - 1; cardTested++)
                {
                    if (allCards[cardIndex].CardRank == allCards[cardTested].CardRank) {
                        pairs.Add(allCards[cardIndex]);
                        allCards.Remove(allCards[cardIndex]);
                        allCards.Remove(allCards[cardTested]);
                    }
                }
            }
            if (pairs.Count > 0) {
                if (pairs.Count == 2) {
                    int score = 0;
                    for (int i = 0; i < 2; i++) {
                        score += Card.Score[pairs[i].CardRank] + valuePair * (i + 1);
                    }
                }
                return Card.Score[pairs[0].CardRank] + valuePair + CalculateHighCard(allCards);
            }
            return CalculateHighCard(allCards);
        }

        private int CalculateBrelan(List<Card> allCards) {//max = 55
            int valueBrelan = 42;
            
            for (int cardIndex = 0; cardIndex < allCards.Count - 2; cardIndex++)
            {
                for (int cardTested = cardIndex + 1; cardTested < allCards.Count - 1; cardTested++)
                {
                    for (int cardThird = cardTested + 1; cardThird < allCards.Count; cardThird++)
                    {
                        if (allCards[cardIndex].CardRank == allCards[cardTested].CardRank && allCards[cardIndex].CardRank == allCards[cardThird].CardRank) {
                            return Card.Score[allCards[cardIndex].CardRank] + valueBrelan;
                        }
                    }
                }
            }
            return CalculatePairs(allCards);
        }

        private int CalculateStraight(List<Card> allCards) {//max = 69
            int valueStraight = 56;
            
            allCards.OrderBy(card => card.CardRank);
            int precedent = Card.Score[allCards[0].CardRank];
            int index = 1;
            while (index < allCards.Count && Card.Score[allCards[index].CardRank] == precedent + 1) {
                precedent = Card.Score[allCards[index].CardRank];
                index++;
            }
            if(index == allCards.Count) {
                return Card.Score[allCards[index - 1].CardRank] + valueStraight;
            }
            return CalculateBrelan(allCards);
        }

        private int CalculateFlush(List<Card> allCards) {//max = 139
            int valueFlush = 70;

            Card.Type type = allCards[0].CardType;
            int index = 1;
            while (index < allCards.Count && allCards[index].CardType == type) {
                index++;
            }
            if(index == allCards.Count) {
                return CalculateStraight(allCards) + valueFlush;
            }
            return CalculateStraight(allCards);
        }

        private int CalculateFull(List<Card> allCards) {//max = 168
            int valueFull = 140;

            List<Card.Rank> ranks = new List<Card.Rank>();
            List<Card> house = new List<Card>();
            ranks.Add(allCards[0].CardRank);
            house.Add(allCards[0]);
            int index = 1;
            while (index < allCards.Count) {
                bool isNew = true;
                foreach (var rank in ranks)
                {
                    if(allCards[index].CardRank == rank) {
                        isNew = false;
                    }
                }
                if (isNew) {
                    ranks.Add(allCards[index].CardRank);
                    house.Add(allCards[index]);
                }
            }
            if(ranks.Count == 2) {
                house.OrderBy(card => card.CardRank);
                return Card.Score[house[1].CardRank] + Card.Score[house[0].CardRank] + valueFull;
            }
            return CalculateFlush(allCards);
        }

        private int CalculateFour(List<Card> allCards) {//max = 182
            int valueFour = 169;
            
            
            for (int cardIndex = 0; cardIndex < allCards.Count - 2; cardIndex++)
            {
                for (int cardTested = cardIndex + 1; cardTested < allCards.Count - 1; cardTested++)
                {
                    for (int cardThird = cardTested + 1; cardThird < allCards.Count; cardThird++)
                    {
                        for (int cardFourth = cardThird + 1; cardFourth < allCards.Count; cardFourth++)
                        {
                            if (allCards[cardIndex].CardRank == allCards[cardTested].CardRank && allCards[cardIndex].CardRank == allCards[cardThird].CardRank && allCards[cardIndex].CardRank == allCards[cardFourth].CardRank) {
                                return Card.Score[allCards[cardIndex].CardRank] + valueFour;
                            }
                        }
                    }
                }
            }
            return CalculateFull(allCards);
        }

        private int CalculateStraightFlushRoyale(List<Card> allCards) {//max = 196
            int valueStraightFlush = 183;
            int valueFlush = 70;

            Card.Type type = allCards[0].CardType;
            int index = 1;
            while (index < allCards.Count && allCards[index].CardType == type) {
                index++;
            }
            if(index == allCards.Count) { //il est flush
            
                bool royal = false;
                allCards.OrderBy(card => card.CardRank);
                if(allCards[0].CardRank == Card.Rank.Ten) {
                    royal = true;
                }
                int precedent = Card.Score[allCards[0].CardRank];
                index = 1;
                while (index < allCards.Count && Card.Score[allCards[index].CardRank] == precedent + 1) {
                    precedent = Card.Score[allCards[index].CardRank];
                    index++;
                }
                if(index == allCards.Count) {//il est straight
                    if(royal) {
                        return 197;//value straight flush royal
                    }
                    return Card.Score[allCards[index - 1].CardRank] + valueStraightFlush;
                }
                return CalculateStraight(allCards) + valueFlush;
            }
            return CalculateFour(allCards);
        }

        public void Print(string name) {
            if (myCards.Count == 2) {
                Debug.Log(name + " having : " + myCards[0].GetName() + " and " + myCards[1].GetName());
            }
        }
    }
}