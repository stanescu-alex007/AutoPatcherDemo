namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Manually;

public abstract class ManualSyncLog : SyncLogModel
{
    public string ExecutionType { get;} = "Manual";
}
