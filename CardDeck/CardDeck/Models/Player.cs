using System.Collections.Generic;

namespace CardDeck.Models
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; set; }
    }
}
