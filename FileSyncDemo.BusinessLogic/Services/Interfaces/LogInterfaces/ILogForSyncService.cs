namespace FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;

public interface ILogForSyncService
{
    Task LogManualSyncSuccessAsync(string message);
    Task LogManualSyncFailureAsync(string message);

    Task LogAutomaticSyncSuccessAsync(string message);
    Task LogAutomaticSyncFailureAsync(string message);

    Task LogAutomaticSyncStartAsync();
    Task LogAutomaticSyncStopAsync();

    Task LogManualAutoSyncStartAsync();
    Task LogManualAutoSyncStopAsync();
}