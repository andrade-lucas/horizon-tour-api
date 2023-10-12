using Horizon.Domain.Enums;

namespace Horizon.Domain.Queries.Responses.Places;

public class GetPlaceByIdResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string OwnerId { get; set; }
    public EPlaceStatus Status { get; set; }
    public string CityId { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string ZipCode { get; set; }
    public string Neighborhood { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string PresentationImageUrl { get; set; }
    public EAutomaticOpen AutomaticOpen { get; set; }
    public bool IsOpen { get; set; }
    public string Description { get; set; }
    public string OwnerFirstName { get; set; }
    public string OwnerLastName { get; set; }
    public string OwnerEmail { get; set; }
    public string CityName { get; set; }
    public EPlaceType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
