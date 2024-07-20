namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels;

public abstract class SyncLogModel : LogModel
{
    public string ActionType { get;} = "Synchronization";

}
