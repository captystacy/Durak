using Calabonga.OperationResults;

namespace Durak.Game.Domain
{
    public class Round : Identity
    {
        public bool ThrowersCanThrow { get; set; }
        public Match Match { get; }
        public Guid MatchId { get; set; }
        public List<Move> Moves { get; set; } = null!;
        public List<Card> Cards { get; set; }

        public Round(Match match) : this()
        {
            Match = match;
        }

        private Round()
        {
        }

        public Round NextRound()
        {
            Deal();

            var attacker = Match.Players.Find(x => x.Role == PlayerRole.Defender)!;

            Match.ClearRoles();

            Match.NextRoles(attacker);

            return new Round(Match);
        }

        private void Deal()
        {
            var attackerIndex = Match.Players.FindIndex(x => x.Role == PlayerRole.Attacker);
            var defenderIndex = Match.Players.FindIndex(x => x.Role == PlayerRole.Defender);

            Match.Deal(attackerIndex);

            for (var i = 0; i < Match.Players.Count; i++)
            {
                if (i == attackerIndex || i == defenderIndex)
                {
                    continue;
                }

                Match.Deal(Match.Players[i]);
            }

            Match.Deal(defenderIndex);
        }

        public OperationResult<Move> AddMove(Move move)
        {
            var operation = OperationResult.CreateResult<Move>();

            var executeOperation = move.Execute();

            if (!executeOperation.Ok)
            {
                operation.AddError(executeOperation.GetMetadataMessages());
                return operation;
            }

            Moves.Add(move);

            operation.Result = move;

            return operation;
        }
    }
}
