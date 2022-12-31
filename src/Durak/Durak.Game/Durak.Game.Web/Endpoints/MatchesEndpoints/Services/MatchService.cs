using AutoMapper;
using Azure.Core;
using Azure;
using Calabonga.OperationResults;
using Calabonga.UnitOfWork;
using Durak.Game.Domain;
using Durak.Game.Web.Endpoints.RoundsEndpoints.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Durak.Game.Web.Endpoints.MatchesEndpoints.ViewModels;
using FluentValidation;

namespace Durak.Game.Web.Endpoints.MatchesEndpoints.Services
{
    public class MatchService : IMatchService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<MatchCreateViewModel> _validator;
        private readonly IMapper _mapper;

        public MatchService(IUnitOfWork unitOfWork, IValidator<MatchCreateViewModel> validator, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<OperationResult<RoundViewModel>> StartAsync(MatchCreateViewModel model, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult<RoundViewModel>();

            var validationResult = await _validator.ValidateAsync(model, cancellationToken);

            if (!validationResult.IsValid)
            {
                operation.AddError(validationResult.ToString());
                return operation;
            }

            var match = _mapper.Map<Match>(model);

            var round = match.Start();

            await _unitOfWork.GetRepository<Match>().InsertAsync(match, cancellationToken);

            match.Deal();

            operation.Result = _mapper.Map<RoundViewModel>(round);

            return operation;
        }
    }
}
