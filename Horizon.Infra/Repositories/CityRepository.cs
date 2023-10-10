using Dapper;
using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Responses.Cities;
using Horizon.Domain.Repositories;
using Horizon.Infra.Context;

namespace Horizon.Infra.Repositories;

public class CityRepository : ICityRepository
{
    private readonly IDB _db;

    public CityRepository(IDB db)
    {
        _db = db;
    }

    public async Task<City> GetByIdAsync(string id)
    {
        try
        {
            var sql = @"
                SELECT
	                c.Id, c.Name, c.StateId, s.Name StateName, s.UF
                FROM cities c 
                INNER JOIN states s ON s.Id = c.StateId
                WHERE c.Id = @id;
            ";

            var res = await _db.Connection().QueryFirstAsync<GetCityByIdResponse>(sql, new { id });
            var city = new City(res.Id, res.Name);
            city.AddState(new State(res.StateId, res.StateName, res.UF));

            return city;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return null;
        }
    }
}
