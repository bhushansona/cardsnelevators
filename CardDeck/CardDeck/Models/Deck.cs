using System;
using System.Collections.Generic;
using System.Linq;

namespace CardDeck.Models
{
    public class Deck
    {
        public List<Card> Cards { get; }
        public Deck()
        {
            Cards = new List<Card>();
            foreach (var num in Enum.GetValues(typeof(CardNum)))
            {
                foreach (var sign in Enum.GetValues(typeof(CardSign)))
                {
                    Cards.Add(new Card
                    {
                        Num = (CardNum)num,
                        Sign = (CardSign)sign,
                        IsDistributed = false
                    });
                }
            }
        }

        public void DistributeCard(Player player)
        {
            var card = GetCard();
            card.IsDistributed = true;
            player.Hand.Add(card);
        }

        private Card GetCard()
        {
            var random = new Random();
            var availableCards = Cards.Where(x => !x.IsDistributed).ToList();
            var index = random.Next(availableCards.Count);
            return availableCards[index];
        }

    }
}
