namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Manually.Failed;

public class ManualFailedDeleteLogDTO : ManualDeleteLog
{
    public string? Message { get; set; }
    public string Status { get;} = "Fail";
}
