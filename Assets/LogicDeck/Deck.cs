using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Deck
    {
        public List<Card> cards;

        public Deck()
        {
            cards = new List<Card>(); // Initialisation de la liste de cartes
            foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
            {
                foreach (Card.Type type in Enum.GetValues(typeof(Card.Type)))
                {
                    // Création d'une nouvelle map pour chaque combinaison de rang et type
                    Dictionary<Card.Rank, int> powerRankMap = new Dictionary<Card.Rank, int>();
                    foreach (Card.Rank innerRank in Enum.GetValues(typeof(Card.Rank)))
                    {
                        // Assignation d'une valeur arbitraire pour chaque rang dans la map
                        powerRankMap.Add(innerRank, (int)innerRank);
                    }
                    cards.Add(new Card(rank, type, powerRankMap));
                }
            }
        }

        // Méthode pour mélanger les cartes dans le jeu de cartes
        public void Shuffle()
        {
            Random rng = new Random();
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

        // Méthode pour retirer une carte du jeu de cartes
        public Card DrawCard()
        {
            Card drawnCard = cards[0];
            cards.RemoveAt(0);
            return drawnCard;
        }
    }
}
