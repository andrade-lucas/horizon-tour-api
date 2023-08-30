using Horizon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Horizon.Auth.Services.Contracts;

public interface ITokenService
{
    string GenerateToken(User user);
}
