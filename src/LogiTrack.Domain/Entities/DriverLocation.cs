namespace LogiTrack.Domain.Entities;

public class DriverLocation
{
    public Guid Id { get; init; }
    public Guid DriverId { get; private set; }
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public decimal? Heading { get; private set; } // Heading - in degrees, nullable if not available
    public DateTime Timestamp { get; private set; }
    
    private DriverLocation() { }

    public DriverLocation(Guid driverId, double latitude, double longitude, decimal? heading)
    {
        Id = Guid.NewGuid();
        DriverId = driverId;
        Latitude = latitude;
        Longitude = longitude;
        Heading = heading;
        Timestamp = DateTime.UtcNow;
    }
}