using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    public class Card
    {
        public enum Rank
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Joker,
            Queen,
            King,
            Ace
        }

        public enum Type
        {
            Hearts,     // Cœur
            Diamonds,   // Carreau
            Clubs,      // Trèfle
            Spades      // Pique
        }

        public static Dictionary<Rank, int> Map = new Dictionary<Rank, int>
        {
            { Rank.Two, 1 },
            { Rank.Three, 2 },
            { Rank.Four, 3 },
            { Rank.Five, 4 },
            { Rank.Six, 5 },
            { Rank.Seven, 6 },
            { Rank.Eight, 7 },
            { Rank.Nine, 8 },
            { Rank.Ten, 9 },
            { Rank.Joker, 10 },
            { Rank.Queen, 11 },
            { Rank.King, 12 },
            { Rank.Ace, 13 }
        };

        public Rank CardRank { get; } // récuperer le rang de la carte
        public Type CardType { get; } // récuperer le type de la carte
        public Dictionary<Rank, int> PowerRankMap { get; } // Map contenant les rangs de puissance

        public Card(Rank rank, Type type, Dictionary<Rank, int> powerRankMap)
        {
            CardRank = rank;
            CardType = type;
            PowerRankMap = powerRankMap;
        }

        public String GetName()
        {
            String name = "";

            switch (this.CardRank)
            {
                case Rank.Two:
                    name += "2";
                    break;

                case Rank.Three:
                    name += "3";
                    break;

                case Rank.Four:
                    name += "4";
                    break;

                case Rank.Five:
                    name += "5";
                    break;

                case Rank.Six:
                    name += "6";
                    break;

                case Rank.Seven:
                    name += "7";
                    break;

                case Rank.Eight:
                    name += "8";
                    break;

                case Rank.Nine:
                    name += "9";
                    break;

                case Rank.Ten:
                    name += "T";
                    break;

                case Rank.Joker:
                    name += "J";
                    break;

                case Rank.Queen:
                    name += "Q";
                    break;

                case Rank.King:
                    name += "K";
                    break;

                case Rank.Ace:
                    name += "A";
                    break;
            }

            switch (this.CardType)
            {
                case Type.Hearts:
                    name += "H";
                    break;

                case Type.Clubs:
                    name += "C";
                    break;

                case Type.Diamonds:
                    name += "D";
                    break;

                case Type.Spades:
                    name += "S";
                    break;
            }
            return name;
        }
    }
}
