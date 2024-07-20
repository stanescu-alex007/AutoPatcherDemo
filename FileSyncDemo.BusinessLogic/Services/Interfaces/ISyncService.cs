namespace FileSyncDemo.BusinessLogic.Services.Interfaces;

public interface ISyncService
{
    Task<string> SynchronizeFileAsync(bool isManual);
    Task<string> SynchronizeFileAutomaticallyAsync();
    Task<string> SynchronizeFileManuallyAsync();
}
