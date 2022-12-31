using Durak.Game.Domain;

namespace Durak.Game.Web.Endpoints.CardsEndpoints.ViewModels
{
    public class CardViewModel
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public Suit Suit { get; set; }
    }
}
