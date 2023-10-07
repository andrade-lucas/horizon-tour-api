using Horizon.Domain.ValueObjects;
using Horizon.Shared.Entities;

namespace Horizon.Domain.Entities;

public class City : Entity
{
    public string Name { get; private set; }
    public State State { get; private set; }
    public string TimeZone { get; private set; }
    public LatLong LatLong { get; private set; }

    public City(string name)
    {
        Name = name;
    }

    public void AddState(State state) => State = state;

    public void AddTimeZone(string timezone) => TimeZone = timezone;

    public void AddLatLong(LatLong latLong) => LatLong = latLong;
}
