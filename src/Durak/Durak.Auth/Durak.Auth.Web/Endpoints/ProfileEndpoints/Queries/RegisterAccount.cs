using Calabonga.OperationResults;
using Durak.Auth.Web.Application.Services;
using Durak.Auth.Web.Endpoints.ProfileEndpoints.ViewModels;
using MediatR;

namespace Durak.Auth.Web.Endpoints.ProfileEndpoints.Queries
{
    /// <summary>
    /// Request: Register new account
    /// </summary>
    public class RegisterAccountRequest : IRequest<OperationResult<UserProfileViewModel>>
    {
        public RegisterAccountRequest(RegisterViewModel model) => Model = model;

        public RegisterViewModel Model { get; }
    }

    /// <summary>
    /// Response: Register new account
    /// </summary>
    public class RegisterAccountRequestHandler : IRequestHandler<RegisterAccountRequest, OperationResult<UserProfileViewModel>>
    {
        private readonly IAccountService _accountService;

        public RegisterAccountRequestHandler(IAccountService accountService)
            => _accountService = accountService;

        public Task<OperationResult<UserProfileViewModel>> Handle(RegisterAccountRequest request, CancellationToken cancellationToken)
            => _accountService.RegisterAsync(request.Model, cancellationToken);
    }
}