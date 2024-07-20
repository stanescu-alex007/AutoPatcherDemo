namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically;

public abstract class AutomaticSyncLog : SyncLogModel
{
    public string ExecutionType { get;} = "Automatic";
}
