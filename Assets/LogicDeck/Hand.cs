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

        public void Print(string name) {
            if (myCards.Count == 2) {
                Debug.Log(name + " having : " + myCards[0].GetName() + " and " + myCards[1].GetName());
            }
        }
    }
}