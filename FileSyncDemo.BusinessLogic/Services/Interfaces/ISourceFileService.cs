using FileSyncDemo.BusinessLogic.Models.ReplicaModels;
using FileSyncDemo.BusinessLogic.Models.SourceModels;

namespace FileSyncDemo.BusinessLogic.Services.Interfaces;

public interface ISourceFileService
{
    Task<Guid> CreateAsync(SourceFileModelForCreation sourceFileModelForCreation);
    Task RemoveAsync(Guid id);
    


}
