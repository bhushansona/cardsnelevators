using CardDeck.Models;
using System;
using System.IO;
using System.Linq;
namespace CardDeck
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var game = new Game.Game();
            game.AddPlayers(5);

            var deck = new Deck();
            game.Start(deck, 5);

            var filePath = Path.Combine("C:\\Bhushan\\CardDeck\\CardDeck", "Output", "game.txt");
            if (!Directory.Exists("C:\\Bhushan\\CardDeck\\CardDeck\\Output"))
                Directory.CreateDirectory("C:\\Bhushan\\CardDeck\\CardDeck\\Output");

            foreach (var gamePlayer in game.Players)
            {
                File.AppendAllText(filePath, "Player #" + gamePlayer.Name + ": " + string.Join("-", gamePlayer.Hand.OrderBy(x => x.Num).Select(x => x.Name)) + Environment.NewLine);
            }
            File.AppendAllText(filePath, Environment.NewLine);
            Console.WriteLine("Completed Successfully. Check '" + filePath + "' for results.");
            Console.ReadLine();
        }
    }
}
