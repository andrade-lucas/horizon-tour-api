﻿using Horizon.Shared.Contracts;
using MediatR;

namespace Horizon.Auth.Command.Inputs;

public record RegisterUserCommand(
    string FirstName,
    string LastName,
    string NickName,
    string Email,
    string Password
) : IRequest<IResult>;
