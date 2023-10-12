using Dapper;
using Horizon.Domain.Entities;
using Horizon.Domain.Queries.Inputs;
using Horizon.Domain.Queries.Responses.Places;
using Horizon.Domain.Repositories;
using Horizon.Infra.Context;
using Horizon.Shared.Helpers;
using Horizon.Shared.Outputs;

namespace Horizon.Infra.Repositories;

public class PlaceRepository : IPlaceRepository
{
    private readonly IDB _db;

    public PlaceRepository(IDB db)
    {
        _db = db;
    }

    public async Task<PaginationResult<GetPlacesResponse>> GetByUser(string userId, QueryPaginate queryPaginate)
    {
        var offset = PaginationHelper.GetOffset(queryPaginate.Page, queryPaginate.PageSize);
        var filterSql = queryPaginate.Filter != null
            ? "AND p.Name LIKE @filter"
            : "";
        var sql = $@"
            SELECT COUNT(*) AS TOTAL_ROWS FROM places p
            INNER JOIN users_places up ON up.PlaceId = p.Id
            WHERE up.UserId = @userId AND p.DeletedAt IS NULL {filterSql};


            SELECT p.Id, p.Name, p.Status, p.IsOpen, p.PresentationImageUrl, CONCAT(u.FirstName, ' ', u.LastName) AS OWNER FROM places p
            INNER JOIN users_places up ON up.PlaceId = p.Id
            LEFT JOIN users u on u.Id = p.OwnerId
            WHERE up.UserId = @userId
            AND p.DeletedAt IS NULL
            {filterSql}
            LIMIT @offset, @perPage;
        ";

        var multi = await _db.Connection().QueryMultipleAsync(sql, new
        {
            userId,
            offset,
            perPage = queryPaginate.PageSize,
            filter = $"%${queryPaginate.Filter}%"
        });
        var totalRows = multi.Read<int>().Single();
        var rows = multi.Read<GetPlacesResponse>().ToList();

        return new PaginationResult<GetPlacesResponse>(totalRows, rows.Count, rows);
    }

    public async Task CreateAsync(Place place)
    {
        try
        {
            var sql = @"
                INSERT INTO places
                    (Id, Name, OwnerId, Status, CityId, Street, Number, 
                    ZipCode, Neighborhood, Latitude, Longitude, PresentationImageUrl,
                    AutomaticOpen, IsOpen, Description, Type, CreatedAt)
                VALUES 
                    (@id, @name, @ownerId, @status, @cityId, @street, @number,
                    @zipCode, @neighborhood, @latitude, @longitude, @presentationImageUrl,
                    @automaticOpen, @isOpen, @description, @type, @createdAt);
            ";

            await _db.Connection().ExecuteAsync(sql, new
            {
                id = place.Id,
                name = place.Name,
                ownerId = place.Owner?.Id,
                status = place.Status,
                cityId = place.Address?.City.Id,
                street = place.Address?.Street,
                number = place.Address?.Number,
                zipCode = place.Address?.ZipCode,
                neighborhood = place.Address?.Neighborhood,
                latitude = place.Address?.LatLong?.Latitude,
                longitude = place.Address?.LatLong?.Longitude,
                presentationImageUrl = place.PresentationImageUrl,
                automaticOpen = place.AutomaticOpen,
                isOpen = place.IsOpen,
                description = place.Description,
                type = place.Type,
                createdAt = place.CreatedAt
            });
        }
        catch
        {
            throw;
        }
    }
}
