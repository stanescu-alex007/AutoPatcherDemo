using FileSyncDemo.BusinessLogic.Models.ActivityLogModels;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Success;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;
using FileSyncDemo.Core.Entities;
using FileSyncDemo.Core.Persistance;
using System.Collections;
using System.Reflection;
using System.Security.Cryptography;

namespace FileSyncDemo.BusinessLogic.Services.Implementations;

// This synchronize only first ReplicaFile, so in order to sync all replicas, it should receive a list by calling repository's GetAllAsync
// and make it somehow inside a foreach loop, and update logs
// also can implement a status for "All files syncronized" if everything is syncronized and set it to false if not
public class SyncService : ISyncService
{
    private readonly IRepository<SourceFile> _sourceFileRepository;
    private readonly IRepository<ReplicaFile> _replicaFileRepository;
    private readonly ILogForSyncService _generateSyncLog;
    
    public SyncService(IRepository<SourceFile> sourceFileRepository, IRepository<ReplicaFile> replicaFileRepository, ILogForSyncService logForSyncService)
    {
        _sourceFileRepository = sourceFileRepository;
        _replicaFileRepository = replicaFileRepository;
        _generateSyncLog = logForSyncService;
    }

    public async Task<string> SynchronizeFileManuallyAsync()
    {
        var response = await SynchronizeFileAsync(isManual: true);
        return response;
    }

    public async Task<string> SynchronizeFileAutomaticallyAsync()
    {
        var response = await SynchronizeFileAsync(isManual: false);
        return response;
    }

    public async Task<string> SynchronizeFileAsync(bool isManual)
    {
        try{
            
            var currentSourceFile = await _sourceFileRepository.FirstOrDefaultAsync();
            var currentReplicaFile = await _replicaFileRepository.FirstOrDefaultAsync();

            if (currentSourceFile == null)
            {   
                var errorMessage = "The source file is missing!";
                throw new FileNotFoundException(errorMessage);
            }

            if (currentReplicaFile == null)
            {
                var errorMessage = "The replica file is missing!";          
                throw new FileNotFoundException(errorMessage);
            }

            var currentSourceFilePath = currentSourceFile!.FilePath;
            var currentReplicaFilePath = currentReplicaFile!.FilePath;

            if (!FilesAreEqual(currentSourceFilePath!, currentReplicaFilePath!))
            {
                CopyFileContent(currentSourceFilePath!, currentReplicaFilePath!);

                var successMessage = "Files have been synchronized";
                if (isManual)                                  
                    await _generateSyncLog.LogManualSyncSuccessAsync(successMessage);               
                else               
                    await _generateSyncLog.LogAutomaticSyncSuccessAsync(successMessage);   
                return successMessage;
            }
            else
            {
                var successMessage = "Files are already synchronized";
                if (isManual)                                   
                    await _generateSyncLog.LogManualSyncSuccessAsync(successMessage);               
                else                                 
                    await _generateSyncLog.LogAutomaticSyncSuccessAsync(successMessage);
                return successMessage;
            }

            

        }catch(FileNotFoundException ex)
        {
            if (isManual)
            {
                await _generateSyncLog.LogManualSyncFailureAsync(ex.Message);
            }
            else
            {
                await _generateSyncLog.LogAutomaticSyncFailureAsync(ex.Message);
            }
            return ex.Message;
        }
        catch(Exception ex)
        {
            if (isManual)
            {
                await _generateSyncLog.LogManualSyncFailureAsync(ex.Message);                
            }
            else
            {
                await _generateSyncLog.LogAutomaticSyncFailureAsync(ex.Message);
            }
            return ex.Message;
        }
    }


    public static bool FilesAreEqual(string sourceFilePath, string replicaFilePath)
    {
        using var sourceFileStream = File.OpenRead(sourceFilePath);
        using var replicaFileStream = File.OpenRead(replicaFilePath);

        using var sha512 = SHA512.Create();

        var sourceFileHash = sha512.ComputeHash(sourceFileStream);
        var replicaFileHash = sha512.ComputeHash(replicaFileStream);

        return StructuralComparisons.StructuralEqualityComparer.Equals(sourceFileHash, replicaFileHash);
    }

    public static void CopyFileContent(string sourceFilePath, string replicaFilePath)
    {
        var sourceFileContent = File.ReadAllText(sourceFilePath);

        File.WriteAllText(replicaFilePath, sourceFileContent);
    }
    
}
    
