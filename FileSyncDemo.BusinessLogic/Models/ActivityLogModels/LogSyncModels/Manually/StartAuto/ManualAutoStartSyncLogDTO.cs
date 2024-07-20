namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Manually.StartAuto;

public class ManualAutoStartSyncLogDTO : ManualSyncLog
{
    public string Message { get; } = "Automatic Sync Started";
    public string Status { get; } = "Success";
}
