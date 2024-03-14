using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck(); // Création d'un nouveau jeu de cartes
            deck.Shuffle(); // Mélange des cartes dans le jeu de cartes

            List<Card> playerHand = new List<Card>(); // Liste pour la main du joueur
            List<Card> dealerHand = new List<Card>(); // Liste pour la main du croupier

            // Distribuer deux cartes à chaque joueur
            for (int i = 0; i < 2; i++)
            {
                playerHand.Add(deck.DrawCard()); // Ajout d'une carte à la main du joueur
                dealerHand.Add(deck.DrawCard()); // Ajout d'une carte à la main du croupier
            }

            // Afficher les mains du joueur et du croupier dans la console
            Console.WriteLine("Main du joueur:");
            foreach (var card in playerHand)
            {
                Console.WriteLine($"{card.CardRank} of {card.CardType}");
                Console.WriteLine($"{card.GetName()}");
            }

            Console.WriteLine("\nMain du croupier:");
            foreach (var card in dealerHand)
            {
                Console.WriteLine($"{card.CardRank} of {card.CardType}");
                Console.WriteLine($"{card.GetName()}");
            }

        }
    }

}
