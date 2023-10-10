﻿using Horizon.Auth.Command.Handlers;
using Horizon.Auth.Command.Inputs;
using Horizon.Domain.Commands.Handlers.Account;
using Horizon.Domain.Commands.Handlers.Places;
using Horizon.Domain.Commands.Handlers.Users;
using Horizon.Domain.Commands.Inputs.Account;
using Horizon.Domain.Commands.Inputs.Places;
using Horizon.Domain.Commands.Inputs.Users;
using Horizon.Domain.Queries.Handlers.Account;
using Horizon.Domain.Queries.Inputs.Account;
using Horizon.Shared.Commands;

namespace Horizon.Api.Configuration.Extensions;

public static class CommandsExtension
{
    public static void ConfigureCommands(this IServiceCollection services)
    {
        services.AddTransient<ICommandHandler<LoginCommand>, LoginHandler>();
        services.AddTransient<ICommandHandler<RegisterUserCommand>, RegisterUserHandler>();
        services.AddTransient<ICommandHandler<GetCurrentUserCommand>, GetCurrentUserHandler>();
        services.AddTransient<ICommandHandler<UpdateAccountCommand>, UpdateAccountHandler>();
        services.AddTransient<ICommandHandler<ChangeProfilePictureCommand>, ChangeProfilePictureHandler>();
        services.AddTransient<ICommandHandler<DeleteUserCommand>, DeleteUserHandler>();
        services.AddTransient<ICommandHandler<CreatePlaceCommand>, CreatePlaceHandler>();
    }
}
