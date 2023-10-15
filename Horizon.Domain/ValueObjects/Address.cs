using Horizon.Domain.Entities;

namespace Horizon.Domain.ValueObjects;

public class Address
{
    public string? Street { get; private set; }
    public string? Number { get; private set; }
    public string? ZipCode { get; private set; }
    public string? Neighborhood { get; private set; }
    public LatLong? LatLong { get; private set; }
    public City City { get; private set; }

    public Address(City city)
    {
        City = city;
    }

    public void AddStreet(string street) => Street = street;

    public void AddNumber(string number) => Number = number;

    public void AddZipCode(string zipCode) => ZipCode = zipCode;

    public void AddNeighborhood(string neighborhood) => Neighborhood = neighborhood;

    public void AddLatLong(LatLong latLong) => LatLong = latLong;
}
