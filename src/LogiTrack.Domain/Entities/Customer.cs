namespace LogiTrack.Domain.Entities;

public class Customer
{
    public Guid Id { get; init; } // I have used `init` here because I never need to change the id after once assigned.
    public string FullName { get; private set; } = string.Empty; 
    public string Email { get; private set; }  = string.Empty;
    public string? PhoneNumber { get; private set; } 
    public DateTime CreatedAt { get; init; }

    private Customer()
    {
    }

    public Customer(string fullName, string email, string phoneNumber)
    {
        if(string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty", nameof(fullName));
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
        if(string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("PhoneNumber cannot be empty", nameof(phoneNumber));
        Id = Guid.NewGuid();
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        CreatedAt = DateTime.UtcNow;
    }
    public void UpdateContactInfo(string email, string? phoneNumber)
    {
        if(string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
        if(string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("PhoneNumber cannot be empty", nameof(phoneNumber));
        Email = email;
        PhoneNumber = phoneNumber;
    }
}