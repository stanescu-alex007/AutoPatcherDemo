namespace FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;

public interface ILogForCreateService
{

    Task LogManualCreateSuccessAsync(string message);
    Task LogManualCreateFailureAsync(string message);

}
