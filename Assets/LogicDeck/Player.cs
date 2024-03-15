using System;
using UnityEngine;

namespace Poker
{
    public class Player {
        public int tokens;
        public Hand hand;
        public int derniereMise;
        public Player(int tokens, Hand hand) {
            this.tokens = tokens;
            this.hand = hand;
            this.derniereMise = 0;
        }

        public void PlayRandom(Jeu jeu) {
            System.Random rng = new System.Random();

            Debug.Log("thinking very smert");

            int playedTokens = ( rng.Next() % (tokens + 2) ) - 1 ;
            playedTokens -= jeu.mise;

            if (playedTokens < 0) {
                jeu.Pass(this);
            } else {
                jeu.Bet(playedTokens);
                tokens -= playedTokens + jeu.mise;
                derniereMise = playedTokens + jeu.mise;
            }
            jeu.TourTermine();
        }
    }
}