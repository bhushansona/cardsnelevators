using CardDeck.Models;
using System.Collections.Generic;

namespace CardDeck.Game
{
    public class Game
    {
        public List<Player> Players { get; set; }

        public void AddPlayers(int noOfPlayers)
        {
            Players = new List<Player>();
            for (var i = 0; i < noOfPlayers; i++)
            {
                Players.Add(new Player
                {
                    Name = (i+1).ToString(),
                    Hand = new List<Card>()
                });
            }
        }

        public void Start(Deck deck, int noOfHands)
        {
            for (var i = 0; i <= noOfHands; i++)
            {
                foreach (var gamePlayer in Players)
                {
                    deck.DistributeCard(gamePlayer);
                }
            }
        }
    }
}
