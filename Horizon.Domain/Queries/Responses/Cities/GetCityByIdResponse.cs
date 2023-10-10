namespace Horizon.Domain.Queries.Responses.Cities;

public class GetCityByIdResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string StateId { get; set; }
    public string StateName { get; set; }
    public string UF { get; set; }
}
