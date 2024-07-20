namespace FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;

public interface ILogForDeleteService
{
    Task LogManualDeleteSuccessAsync(string message);
    Task LogManualDeleteFailureAsync(string message);
    Task LogAutoDeleteSuccessAsync(string message);
}
