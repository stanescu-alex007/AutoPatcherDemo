namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Manually.Success;

public class ManualSuccessDeleteLogDTO : ManualDeleteLog
{
    public string? Message { get; set; }
    public string Status { get;} = "Success";
}
