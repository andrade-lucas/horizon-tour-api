namespace Horizon.Domain.ValueObjects;

public class LatLong
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }

    public LatLong(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }
}
