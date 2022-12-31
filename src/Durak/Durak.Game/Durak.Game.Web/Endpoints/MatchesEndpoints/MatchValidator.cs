using Durak.Game.Web.Endpoints.MatchesEndpoints.ViewModels;
using FluentValidation;

namespace Durak.Game.Web.Endpoints.MatchesEndpoints
{
    public class MatchCreateRequestValidator : AbstractValidator<MatchCreateViewModel>
    {
        public MatchCreateRequestValidator() => RuleSet("default", () =>
        {
            RuleFor(x => x.Players).NotEmpty().NotNull();
        });
    }
}
