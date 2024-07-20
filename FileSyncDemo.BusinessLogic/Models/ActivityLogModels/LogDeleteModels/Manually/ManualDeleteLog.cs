namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Manually;

public abstract class ManualDeleteLog : DeleteLogModel
{
    public string ExecutionType { get;} = "Manual";

}
