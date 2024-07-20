namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Success;

public class AutomaticSuccessSyncLogDTO : AutomaticSyncLog
{
    public string? Message { get; set; }
    public string Status { get;} = "Success";
}
