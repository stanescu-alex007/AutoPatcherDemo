using AutoMapper;
using FileSyncDemo.BusinessLogic.Models.ReplicaModels;
using FileSyncDemo.BusinessLogic.Models.SourceModels;
using FileSyncDemo.BusinessLogic.Services.Implementations.LogImplementations;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;
using FileSyncDemo.Core.Entities;
using FileSyncDemo.Core.Persistance;
using System.Collections;

namespace FileSyncDemo.BusinessLogic.Services.Implementations;

public class ReplicaFileService : IReplicaFileService
{
    private IMapper _mapper;
    private readonly IRepository<ReplicaFile> _replicaFileRepository;
    private readonly IRepository<SourceFile> _sourceFileRepository;
    private readonly ILogForCreateService _generateCreateLog;
    private readonly ILogForDeleteService _generateDeleteLog;

    public ReplicaFileService( IMapper mapper, IRepository<ReplicaFile> replicaFileRepository,IRepository<SourceFile> sourceFileRepository, ILogForCreateService logForCreateService, ILogForDeleteService logForDeleteService)
    {
        _mapper = mapper;
        _replicaFileRepository = replicaFileRepository;
        _sourceFileRepository = sourceFileRepository;
        _generateCreateLog = logForCreateService;
        _generateDeleteLog = logForDeleteService;
    }

    //TODO: -> (DONE)1.Watch how exceptions are thrown and handle  errorMessages based on where the error came from.
    //      -> (DONE)2.Check how that every single ReplicaFile is deleted from database if I delete the SourceFile.
    //      -> 3.Check ReplicaFiles(from 2.) to delete from desktop too, because if I delete the SourceFile,
    //           they are deleted just from database and not from desktop too, it's something auto anyways.
    //      -> 4.Maybe reorganize ActivityLogs in something simple.
    //      -> 5.Create the automatic check files background service that checks the existence of every file
    //           that is stored inside database, and if they are in database but not on the path/desktop, delete them from db too.
    public async Task<Guid> CreateAsync(ReplicaFileModelForCreation replicaFileModelForCreation)
    {

        try
        {
            if (replicaFileModelForCreation == null)
            {
                throw new ArgumentNullException();
            }

            var sourceFile = await _sourceFileRepository.FirstOrDefaultAsync();

            if (sourceFile == null)
            {
                throw new InvalidOperationException();
            }

            var tEntityMapped = _mapper.Map<ReplicaFile>(replicaFileModelForCreation);
            tEntityMapped.SourceFileId = sourceFile.Id;
            var fullFileName = tEntityMapped.FileName + "." + tEntityMapped.FileExtension;
            var responseEntity = await _replicaFileRepository.CreateAsync(tEntityMapped);

            await File.WriteAllTextAsync(responseEntity.FilePath!, replicaFileModelForCreation.FileContent!);
            await _replicaFileRepository.SaveChangesAsync();
            var message = $"ReplicaFile {fullFileName} created.";
            await _generateCreateLog.LogManualCreateSuccessAsync(message);
           
            return responseEntity.Id;
        }
        catch(ArgumentNullException ex)
        {

            var errorMessage = "Received a null ReplicaFileModelForCreation.";
            Console.WriteLine(errorMessage);
            await _generateCreateLog.LogManualCreateFailureAsync(errorMessage);
            throw new Exception($"An error occurred while trying to create the file {replicaFileModelForCreation.FileName}.", ex);
        }
        catch(InvalidOperationException ex)
        {

            var errorMessage = "No SourceFile found.";
            Console.WriteLine(errorMessage);
            await _generateCreateLog.LogManualCreateFailureAsync(errorMessage);
            throw new Exception($"An error occurred while trying to create the file {replicaFileModelForCreation.FileName}.", ex);
        }
        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred while trying to create the file {replicaFileModelForCreation.FileName}.");
            await _generateCreateLog.LogManualCreateFailureAsync(ex.Message);
            throw new Exception($"An error occurred while trying to create the file {replicaFileModelForCreation.FileName}.", ex);
        }
        

    }

    public async Task RemoveAsync(Guid id)
    {

        var existingReplicaFile = await _replicaFileRepository.GetByIdAsync(id);
        var fullFileName = existingReplicaFile?.FileName + "." + existingReplicaFile?.FileExtension;
        var filePath = existingReplicaFile?.FilePath!;

        if (existingReplicaFile == null)
        {
            var errorMessage = $"ReplicaFile {fullFileName} does not exist";
            await _generateDeleteLog.LogManualDeleteFailureAsync(errorMessage);
            throw new FileNotFoundException();
        }

        try
        {
            Console.WriteLine($"Attempting to delete file at: {filePath}");

            if (File.Exists(filePath))
            {
                var fileInfo = new FileInfo(filePath);
                if ((fileInfo.Attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    fileInfo.Attributes &= ~FileAttributes.ReadOnly;
                }

                await Task.Run(() =>
                {
                    Console.WriteLine("Inside Task.Run: attempting to delete file...");
                    File.Delete(filePath);
                });

                await _replicaFileRepository.DeleteAsync(existingReplicaFile);
                await _replicaFileRepository.SaveChangesAsync();
                var message = $"ReplicaFile {fullFileName} was deleted.";
                await _generateDeleteLog.LogManualDeleteSuccessAsync(message);

                Console.WriteLine($"File {filePath} has been deleted.");

            }
            else
            {
                Console.WriteLine($"File {filePath} does not exist.");
                await _generateDeleteLog.LogManualDeleteFailureAsync($"The file {fullFileName} does not exist");
                throw new FileNotFoundException($"The file {fullFileName} does not exist on the desktop.");
            }


        }
        catch (UnauthorizedAccessException uex)
        {
            Console.WriteLine($"Unauthorized access to the file {filePath}: {uex.Message}");
            await _generateDeleteLog.LogManualDeleteFailureAsync($"Unauthorized access to the file {filePath}: {uex.Message}");
            throw new UnauthorizedAccessException($"Unauthorized access to the file {fullFileName}. Please check your permissions.", uex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            await _generateDeleteLog.LogManualDeleteFailureAsync(ex.Message);
            throw new Exception($"An error occurred while trying to delete the file {fullFileName}.", ex);
        }

    }

   
}
