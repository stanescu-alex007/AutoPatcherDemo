namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogCreateModels.Manually;

 public abstract class ManualCreateLog : CreateLogModel
{
    public string ExecutionType { get;} = "Manual";

}
