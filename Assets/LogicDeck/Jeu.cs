using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Jeu
    {
        public Deck deck;
        public List<Player> AIs;
        public Player player;
        private int playerTurn;
        private int turn = 0;
        private int nbJoueurs;
        public int mise;
        public int bank;

        private Action<Card> callBack;

        public Jeu(int tokens, int nbJoueurs, Action<Card> callBack) {
            Debug.Log("New Game");
            deck = new Deck(nbJoueurs);
            deck.Shuffle();

            deck.Serve();

            player = new Player(tokens,deck.GetHand(0));
            AIs = new List<Player>();
            this.nbJoueurs = nbJoueurs;
            this.callBack = callBack;
            playerTurn = new System.Random().Next() % nbJoueurs;
            mise = 0;
            bank = 0;

            for (int i = 1; i < nbJoueurs; i++)
            {
                AIs.Add(new Player(tokens,deck.GetHand(i)));
            }

            deck.ServeDealer();
            deck.ServeDealer();
            deck.ServeDealer();

            deck.Print();
        }

        public void TourTermine() {
            turn = (turn + 1) % nbJoueurs;

            if( ( GetPlayer(turn).derniereMise == mise && mise > 0 ) || AIs.Count + ( (playerTurn < nbJoueurs) ? 1 : 0 ) == 1 ) {
                Debug.Log("Tour termine");
                if(deck.dealer.Count == 5 || AIs.Count + ( (playerTurn < nbJoueurs) ? 1 : 0 ) == 1) {
                    ShowWinner();
                    return;
                }
                deck.ServeDealer();
                callBack(deck.dealer[deck.dealer.Count - 1]);
                foreach (var ai in AIs)
                {
                    ai.derniereMise = 0;
                }
                player.derniereMise = 0;
                mise = 0;
            }
            if(turn != playerTurn) {
                Debug.Log("au tour de l'IA : " + turn);
                AIs[turn > playerTurn ? turn - 1 : turn].PlayRandom(this);
            } else {
                Debug.Log("au tour du joueur");
            }
        }

        public Player GetPlayer(int turn) {
            if(turn != playerTurn) {
                return AIs[turn > playerTurn ? turn - 1 : turn];
            }
            return player;
        }

        private void ShowWinner()
        {
            int max = 0;
            int winner = 0;
            if (playerTurn < nbJoueurs) {//verifier ici
                Debug.Log("player score : "+player.hand.CalculateScore(deck.dealer));
                max = player.hand.CalculateScore(deck.dealer);
                winner = -1;
            }
            for (int i = 0; i < AIs.Count; i++)
            {
                Debug.Log("AI" + i + " score : " + AIs[i].hand.CalculateScore(deck.dealer));
                if(AIs[i].hand.CalculateScore(deck.dealer) > max) {
                    max = AIs[i].hand.CalculateScore(deck.dealer);
                    winner = i;
                }
            }
            if (winner == -1) {
                player.tokens += bank;
            } else {
                AIs[winner].tokens += bank;
            }
            Debug.Log((winner == -1 ? "player" : ("AI" + winner) ) + " won the game with a score of "+ max );
        }

        public void Pass(Player player)
        {
            Debug.Log("couché");
            nbJoueurs -= 1;
            if( !AIs.Remove(player)) {
                playerTurn = nbJoueurs + 1;
            } else if (playerTurn == nbJoueurs) {
                playerTurn -= 1;
            }
        }

        public void Bet(int playedTokens)
        {
            if(playedTokens == 0) {
                Debug.Log("suit");
            } else {
                Debug.Log("relance de " + playedTokens);
            }
            mise += playedTokens;
            bank += mise;
            Debug.Log("mise est a : " + mise);
        }

        public static void Test(Jeu monJeu)
        {

            if ( monJeu.turn != monJeu.playerTurn ) {
                monJeu.TourTermine();
            }

        }
    }

}
