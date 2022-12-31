namespace Durak.Game.Domain
{
    public class Match : Auditable
    {
        public List<Card> Deck { get; set; }
        public Suit TrumpSuit { get; private set; }
        public List<Player> Players { get; }
        public List<Round> Rounds { get; set; } = null!;

        public Match(List<Player> players) : this()
        {
            Players = players;
        }

        private Match()
        {
        }

        public Round Start()
        {
            ShuffleDeck();

            TrumpSuit = Deck.Last().Suit;

            var attacker = GetFirstPlayer();
            NextRoles(attacker);

            var round = new Round(this);

            Rounds = new List<Round> { round };

            return round;
        }

        public void NextRoles(Player attacker)
        {
            attacker.Role = PlayerRole.Attacker;
            attacker.IsNearestToDefender = true;

            var defender = Players.GetNextItem(attacker);
            defender.Role = PlayerRole.Defender;

            Players.GetNextItem(defender).IsNearestToDefender = true;
        }

        public void ClearRoles()
        {
            foreach (var player in Players)
            {
                player.ClearRoles();
            }
        }

        public void Deal()
        {
            foreach (var player in Players)
            {
                Deal(player);
            }
        }

        public void Deal(int playerIndex)
        {
            Deal(Players[playerIndex]);
        }

        public void Deal(Player player)
        {
            var queue = new Queue<Card>(Deck);

            for (var i = player.Cards.Count; i < Constants.FullHandCount; i++)
            {
                var card = queue.Dequeue();
                player.Cards.Add(card);
            }

            Deck = new List<Card>(queue);
        }

        private void ShuffleDeck()
        {
            Deck = Constants.Deck.ToList();

            Deck.Shuffle();
        }

        private Player GetFirstPlayer() =>
            Players.MinBy(x => x.GetLowestCard(TrumpSuit)?.Value) ?? Players.First();
    }
} 