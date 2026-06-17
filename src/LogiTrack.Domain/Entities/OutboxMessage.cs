namespace LogiTrack.Domain.Entities;

public class OutboxMessage
{
    public Guid Id { get; init; }
    public string MessageType { get; init; } = string.Empty;
    public string Payload { get; private set; } = string.Empty; 
    public DateTime OccurredOn { get; private set; }
    public DateTime? ProcessedOn { get; private set; }
    public string? Error { get; private set; }
    
    private OutboxMessage() { }
    public OutboxMessage(string messageType, string payload)
    {
        Id = Guid.NewGuid();
        MessageType = messageType;
        Payload = payload;
        OccurredOn = DateTime.UtcNow;
    }
    
    public void MarkAsProcessed()
    {
        ProcessedOn = DateTime.UtcNow;
    }
    public void MarkAsFailed(string error)
    {
        Error = error;
    }
}