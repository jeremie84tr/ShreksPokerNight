using System;
using System.Collections.Generic;
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

            Debug.Log("thinking very smert");

            System.Random rng = new System.Random();

            List<int> randoms = new List<int>();

            int size = rng.Next() % 5000000;

            for (int element = 0; element < size; element++)
            {
                randoms.Add( rng.Next() % 10 );
            }

            int playedTokens = randoms[rng.Next() % size];
            playedTokens = playedTokens > tokens ? tokens : playedTokens;
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