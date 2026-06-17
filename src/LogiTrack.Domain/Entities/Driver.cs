using LogiTrack.Domain.Enums;

namespace LogiTrack.Domain.Entities;

public class Driver
{
    public Guid Id { get; init; }
    public string Name { get; private set; } = string.Empty;
    public string LicenseNumber { get; private set; }  = string.Empty;
    public DateTime DateOfBirth { get; private set; }
    public string PhoneNumber { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public DriverStatus  CurrentStatus { get; private set; }
    public DateTime CreatedAt { get; init; }

    private Driver() { }

    public Driver(string name, string licenseNumber, DateTime dateOfBirth, string phoneNumber, string? email = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name cannot be empty", nameof(name));
        if (string.IsNullOrWhiteSpace(licenseNumber)) throw new ArgumentException("License number cannot be empty", nameof(licenseNumber));
        if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException("Phone number cannot be empty", nameof(phoneNumber));

        Id = Guid.NewGuid();
        Name = name;
        LicenseNumber = licenseNumber;
        DateOfBirth = dateOfBirth;
        PhoneNumber = phoneNumber;
        Email = email;
        CurrentStatus = DriverStatus.Idle;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateContactInfo(string phoneNumber, string? email)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException("Phone number cannot be empty", nameof(phoneNumber));
        PhoneNumber = phoneNumber;
        Email = email;
    }
    public void UpdateStatus(DriverStatus status)
    {
        CurrentStatus = status;
    }
}