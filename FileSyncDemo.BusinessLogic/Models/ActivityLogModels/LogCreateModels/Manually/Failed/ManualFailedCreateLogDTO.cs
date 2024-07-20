namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogCreateModels.Manually.Failed;

public class ManualFailedCreateLogDTO : ManualCreateLog
{
    public string? Message { get; set; }
    public string Status { get;} = "Fail";
}
