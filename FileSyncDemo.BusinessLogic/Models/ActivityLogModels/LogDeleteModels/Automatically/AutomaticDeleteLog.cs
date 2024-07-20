namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Automatically;

public class AutomaticDeleteLog : DeleteLogModel
{
    public string ExecutionType { get; } = "Automatic";

}
