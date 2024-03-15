using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        public static void Test()
        {
            Deck deck = new Deck(4); // Création d'un nouveau jeu de cartes
            deck.Shuffle(); // Mélange des cartes dans le jeu de cartes

            deck.Serve();

            Hand player = deck.GetHand(0);

            deck.ServeDealer();
            deck.ServeDealer();
            deck.ServeDealer();

            player.Print("player");

            deck.Print();

        }
    }

}
