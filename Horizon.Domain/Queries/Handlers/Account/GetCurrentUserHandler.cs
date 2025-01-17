﻿using Horizon.Shared.Messages;
using Horizon.Domain.Queries.Inputs.Account;
using Horizon.Domain.Repositories;
using Horizon.Shared.Contracts;
using Horizon.Shared.Outputs;
using MediatR;
using System.Net;

namespace Horizon.Domain.Queries.Handlers.Account;

public class GetCurrentUserHandler : IRequestHandler<GetCurrentUserQuery, IResult>
{
    private readonly IUserRepository _userRepository;

    public GetCurrentUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IResult> Handle(GetCurrentUserQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetByIdAsync(query.UserId);

            if (user == null) return new CommandResult(false, Messages.Error, (int)HttpStatusCode.BadRequest);

            return new CommandResult(true, string.Empty, (int)HttpStatusCode.OK, data: user);
        }
        catch(Exception ex)
        {
            return new CommandResult
            {
                Success = false,
                Message = Messages.Error,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Errors = ex.Message
            };
        }
    }
}
