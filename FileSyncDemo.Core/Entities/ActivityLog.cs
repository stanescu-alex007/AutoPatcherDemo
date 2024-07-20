namespace FileSyncDemo.Core.Entities;

public class ActivityLog : Entity
{
    public DateTime Timestamp { get; set; }
    public string? ActionType { get; set; }
    public string? ExecutionType{ get; set; }
    public string? Message { get; set; }
    public string? Status { get; set; }
    
}
