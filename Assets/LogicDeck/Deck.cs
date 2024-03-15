using System;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace Poker
{
    public class Deck
    {
        public List<Card> cards;

        public List<Card> dealer;

        public List<Hand> players;

        public Deck(int nbPlayers)
        {
            cards = new List<Card>(); // Initialisation de la liste de cartes
            dealer = new List<Card>();
            players = new List<Hand>();

            for (int player = 0; player < nbPlayers; player++)
            {
                players.Add(new Hand());
            }
            foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
            {
                foreach (Card.Type type in Enum.GetValues(typeof(Card.Type)))
                {
                    //ajout des cartes
                    cards.Add(new Card(rank, type));
                }
            }
        }

        // Méthode pour mélanger les cartes dans le jeu de cartes
        public void Shuffle()
        {
            System.Random rng = new System.Random();
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Card value = cards[k];
                cards[k] = cards[n];
                cards[n] = value;
            }
        }

        public void Serve() {
            for (int i = 0; i < 2; i++) {
                foreach (Hand hand in players) {
                    hand.addCard(DrawCard());
                }
            }
        }

        internal void ServeDealer()
        {
            dealer.Add(DrawCard());
        }

        public Hand GetHand(int index) {
            if (index < players.Count) {
                return players[index];
            }
            return new Hand();
        }

        public void Print() {
            UnityEngine.Debug.Log("poker : " + players.Count + " players");
            foreach (Hand hand in players)
            {
                hand.Print("hand");
            }
            string dealerCards = "";
            foreach (var card in dealer)
            {
                dealerCards += card.GetName() + " ";
            }
            UnityEngine.Debug.Log("dealer : " + dealerCards);
        }

        // Méthode pour retirer une carte du jeu de cartes
        public Card DrawCard()
        {
            Card drawnCard = cards[0];
            cards.RemoveAt(0);
            return drawnCard;
        }
    }
}
