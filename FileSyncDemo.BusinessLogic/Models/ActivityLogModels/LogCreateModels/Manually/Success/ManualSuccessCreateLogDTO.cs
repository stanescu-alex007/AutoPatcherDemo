namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogCreateModels.Manually.Success;

public class ManualSuccessCreateLogDTO : ManualCreateLog
{
    public string? Message { get; set; }
    public string Status { get;} = "Success";

}
