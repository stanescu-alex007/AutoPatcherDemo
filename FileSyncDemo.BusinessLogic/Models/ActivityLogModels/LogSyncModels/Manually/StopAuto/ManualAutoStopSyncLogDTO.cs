namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Manually.StopAuto;

public class ManualAutoStopSyncLogDTO : ManualSyncLog
{
    public string Message { get; } = "Automatic Sync Stopped";
    public string Status { get; } = "Success";
}
