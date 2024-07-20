namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Automatically.Success;

public class AutomaticSuccessDeleteLogDTO : AutomaticDeleteLog
{

    public string? Message { get; set; }
    public string Status { get; } = "Success";

}
