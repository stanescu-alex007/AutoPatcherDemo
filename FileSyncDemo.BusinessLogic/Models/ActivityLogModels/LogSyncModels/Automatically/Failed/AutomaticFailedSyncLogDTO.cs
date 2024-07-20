namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Failed;

public class AutomaticFailedSyncLogDTO : AutomaticSyncLog
{
    public string? Message { get; set; }
    public string Status { get;} = "Fail";
}
