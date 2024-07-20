using AutoMapper;
using FileSyncDemo.BusinessLogic.Models.SourceModels;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using FileSyncDemo.BusinessLogic.Services.Interfaces.LogInterfaces;
using FileSyncDemo.Core.Entities;
using FileSyncDemo.Core.Persistance;

namespace FileSyncDemo.BusinessLogic.Services.Implementations;

public class SourceFileService : ISourceFileService
{
    private readonly IRepository<SourceFile> _fileRepository;
    private IMapper _mapper { get; set; }

    private readonly ILogForCreateService _generateCreateLog;
    private readonly ILogForDeleteService _generateDeleteLog;

    public SourceFileService(IRepository<SourceFile> fileRepository, IMapper mapper, ILogForCreateService logForCreateService, ILogForDeleteService logForDeleteService)
    {
        _fileRepository = fileRepository;
        _mapper = mapper;
        _generateCreateLog = logForCreateService;
        _generateDeleteLog = logForDeleteService;
    }

    //TODO: In this stage, I can only have one SourceFile and all replica are related to this one.
    //      But I still can create many Sources, so this is not good now. Later, when I'll have many, it will do.
    public async Task<Guid> CreateAsync(SourceFileModelForCreation sourceFileModelForCreation)
    {

        try
        {
            if (sourceFileModelForCreation == null)
            {
                var errorMessage = "Received a null SourceFileModelForCreation.";
                Console.WriteLine(errorMessage);
                await _generateCreateLog.LogManualCreateFailureAsync(errorMessage);
                throw new ArgumentNullException(nameof(sourceFileModelForCreation));
            }

            var tEntityMapped = _mapper.Map<SourceFile>(sourceFileModelForCreation);
            var fullFileName = tEntityMapped.FileName + "." + tEntityMapped.FileExtension;
            var responseEntity = await _fileRepository.CreateAsync(tEntityMapped);

            await File.WriteAllTextAsync(responseEntity.FilePath!, sourceFileModelForCreation.FileContent!);
            await _fileRepository.SaveChangesAsync();
            var message = $"SourceFile {fullFileName} created.";
            await _generateCreateLog.LogManualCreateSuccessAsync(message);

            return responseEntity.Id;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while trying to create the file {sourceFileModelForCreation.FileName}.");
            await _generateCreateLog.LogManualCreateFailureAsync(ex.Message);
            throw new Exception($"An error occurred while trying to create the file {sourceFileModelForCreation.FileName}.", ex);
        }
        

    }

    public async Task RemoveAsync(Guid id)
    {

        var existingSourceFile = await _fileRepository.GetByIdAsync(id);

        var fullFileName = existingSourceFile?.FileName + "." + existingSourceFile?.FileExtension;
        var filePath = existingSourceFile?.FilePath!;

        if(existingSourceFile == null)
        {
            var errorMessage = $"SourceFile {fullFileName} does not exist";
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

                Console.WriteLine("Inside Task.Run: attempting to delete file...");
                File.Delete(filePath);
                
                await _fileRepository.DeleteAsync(existingSourceFile);
                await _fileRepository.SaveChangesAsync();
                var message = $"SourceFile {fullFileName} was deleted.";
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
