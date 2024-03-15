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
            for (int i = 0; i < allCards.Count; i++)
            {
                for (int j = 0; j < allCards.Count; j++)
                {
                    if (Card.Score[allCards[j].CardRank] < Card.Score[allCards[i].CardRank]) {
                        Card tmp = allCards[i];
                        allCards[i] = allCards [j];
                        allCards[j] = tmp;
                    }
                }
            }
            return CalculateStraightFlushRoyale(allCards);
        }

        private int CalculateHighCard(List<Card> allCards) {//max = 13
            //Debug.Log("carte max");
            return Card.Score[allCards[0].CardRank];
        }

        private int CalculatePairs(List<Card> allCards) {//max = 41
            int valuePair = 14;

            for (int cardIndex = 0; cardIndex < allCards.Count - 1; cardIndex++)
            {
                if (allCards[cardIndex].CardRank == allCards[cardIndex + 1].CardRank) {
                    for (int pairIndex = 0; pairIndex < allCards.Count - 1; pairIndex++) {
                        if (pairIndex != cardIndex && pairIndex != cardIndex + 1 && allCards[pairIndex].CardRank == allCards[pairIndex + 1].CardRank) {
                            //Debug.Log("double paire");
                            return Card.Score[allCards[cardIndex].CardRank] + Card.Score[allCards[pairIndex].CardRank] + valuePair;
                        }
                    }
                    //Debug.Log("paire");
                    return Card.Score[allCards[cardIndex].CardRank] + valuePair + CalculateHighCard(allCards);
                }
            }
            return CalculateHighCard(allCards);
        }

        private int CalculateBrelan(List<Card> allCards) {//max = 55
            int valueBrelan = 42;
            
            for (int cardIndex = 0; cardIndex < allCards.Count - 2; cardIndex++)
            {
                int cardTested = cardIndex + 1;
                int cardThird = cardTested + 1;
                if (allCards[cardIndex].CardRank == allCards[cardTested].CardRank && allCards[cardIndex].CardRank == allCards[cardThird].CardRank) {
                    //Debug.Log("Brelan");
                    return Card.Score[allCards[cardIndex].CardRank] + valueBrelan;
                }
            }
            return CalculatePairs(allCards);
        }

        private int CalculateStraight(List<Card> allCards) {//max = 69
            int valueStraight = 56;
            
            for (int cardIndex = 0; cardIndex < allCards.Count - 4; cardIndex++)
            {
                if (
                    Card.Score[allCards[cardIndex].CardRank] + 1 == Card.Score[allCards[cardIndex + 1].CardRank] &&
                    Card.Score[allCards[cardIndex].CardRank] + 2 == Card.Score[allCards[cardIndex + 2].CardRank] &&
                    Card.Score[allCards[cardIndex].CardRank] + 3 == Card.Score[allCards[cardIndex + 3].CardRank] &&
                    Card.Score[allCards[cardIndex].CardRank] + 4 == Card.Score[allCards[cardIndex + 4].CardRank]) {
                    //Debug.Log("Straight");
                    return Card.Score[allCards[cardIndex].CardRank] + valueStraight;
                }
            }
            return CalculateBrelan(allCards);
        }

        private int CalculateFlush(List<Card> allCards) {//max = 139
            int valueFlush = 70;

            List<Card> allCardsFlush = new List<Card>();
            foreach (var card in allCards)
            {
                allCardsFlush.Add(card);
            }
            allCardsFlush.OrderBy(card => card.CardType);

            for (int i = 0; i < allCards.Count - 4; i++)
            {
                if (
                    allCards[i].CardType == allCards[i + 1].CardType &&
                    allCards[i].CardType == allCards[i + 2].CardType &&
                    allCards[i].CardType == allCards[i + 3].CardType &&
                    allCards[i].CardType == allCards[i + 4].CardType)
                {
                    //Debug.Log("Flush");
                    return CalculateStraight(allCards) + valueFlush;
                }
            }
            return CalculateStraight(allCards);
        }

        private int CalculateFull(List<Card> allCards) {//max = 168
            int valueFull = 140;
            int valueBrelan = 42;

            for (int cardIndex = 0; cardIndex < allCards.Count - 2; cardIndex++)
            {
                if (allCards[cardIndex].CardRank == allCards[cardIndex + 1].CardRank && allCards[cardIndex].CardRank == allCards[cardIndex + 2].CardRank) {
                    for (int pairIndex = 0; pairIndex < allCards.Count - 1; pairIndex++) {
                        if (pairIndex != cardIndex && pairIndex != cardIndex + 1 && pairIndex != cardIndex + 2 && allCards[pairIndex].CardRank == allCards[pairIndex + 1].CardRank) {
                            //Debug.Log("full");
                            return Card.Score[allCards[cardIndex].CardRank] + Card.Score[allCards[pairIndex].CardRank] + valueFull;//a changer
                        }
                    }
                    //Debug.Log("Brelan");
                    return Card.Score[allCards[cardIndex].CardRank] + valueBrelan;
                }
            }
            return CalculateFlush(allCards);
        }

        private int CalculateFour(List<Card> allCards) {//max = 182
            int valueFour = 169;
            
            for (int cardIndex = 0; cardIndex < allCards.Count - 3; cardIndex++)
            {
                if (
                    allCards[cardIndex].CardRank == allCards[cardIndex + 1].CardRank &&
                    allCards[cardIndex].CardRank == allCards[cardIndex + 2].CardRank &&
                    allCards[cardIndex].CardRank == allCards[cardIndex + 3].CardRank) {
                    //Debug.Log("Four");
                    return Card.Score[allCards[cardIndex].CardRank] + valueFour;
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
                        //Debug.Log("Straight flush royale");
                        return 197;//value straight flush royal
                    }
                    //Debug.Log("Straight flush");
                    return Card.Score[allCards[index - 1].CardRank] + valueStraightFlush;
                }
                //Debug.Log("Straight flush");
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