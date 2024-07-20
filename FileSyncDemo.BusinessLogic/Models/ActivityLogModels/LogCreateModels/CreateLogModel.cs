namespace FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogCreateModels;

public abstract class CreateLogModel : LogModel
{

    public string ActionType { get;} = "Create";

}
