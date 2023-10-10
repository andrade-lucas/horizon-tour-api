using Dapper;
using Horizon.Domain.Entities;
using Horizon.Domain.Enums;
using Horizon.Domain.Repositories;
using Horizon.Infra.Context;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Horizon.Infra.Repositories;

public class PlaceRepository : IPlaceRepository
{
    private readonly IDB _db;

    public PlaceRepository(IDB db)
    {
        _db = db;
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
