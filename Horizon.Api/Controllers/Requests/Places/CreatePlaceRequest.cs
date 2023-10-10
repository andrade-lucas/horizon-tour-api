using Horizon.Domain.Enums;

namespace Horizon.Api.Controllers.Requests.Places;

public class CreatePlaceRequest
{
    public string Name { get; set; }
    public EPlaceStatus Status { get; set; }
    public string CityId { get; set; }
    public string Street { get; set; }
    public string Number { get; set; }
    public string ZipCode { get; set; }
    public string Neighborhood { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string PresentationImageBase64 { get; set; }
    public EAutomaticOpen AutomaticOpen { get; set; }
    public string Description { get; set; }
    public EPlaceType Type { get; set; }
}
