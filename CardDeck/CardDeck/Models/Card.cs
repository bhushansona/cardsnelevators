namespace CardDeck.Models
{
    public class Card
    {
        public CardSign Sign { get; set; }
        public CardNum Num { get; set; }

        public bool IsDistributed { get; set; }
        public string Name => ((int)Num <=10 ? ((int)Num).ToString() : Num.ToString()[0].ToString()) + Sign.ToString()[0];
    }
}
