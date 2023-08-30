using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data;

namespace Horizon.Infra.Context;

public class MySqlDb : IDB
{
    private MySqlConnection _db;
    private IConfiguration _configuration;

    public MySqlDb(IConfiguration configuration) => _configuration = configuration;

    public IDbConnection Connection()
    {
        _db = new MySqlConnection(_configuration.GetConnectionString("Default"));
        return _db;
    }

    public void Dispose()
    {
        if (_db.State != ConnectionState.Closed)
            _db.Close();
    }
}
