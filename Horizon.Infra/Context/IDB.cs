using System;
using System.Data;

namespace Horizon.Infra.Context;

public interface IDB : IDisposable
{
    IDbConnection Connection();
}
