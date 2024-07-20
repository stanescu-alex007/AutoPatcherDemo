namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels;

public abstract class DeleteLogModel : LogModel
{
    public string ActionType { get;} = "Delete";

}
