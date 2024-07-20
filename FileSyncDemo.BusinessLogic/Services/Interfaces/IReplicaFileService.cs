using FileSyncDemo.BusinessLogic.Models.ReplicaModels;
using System.Collections;

namespace FileSyncDemo.BusinessLogic.Services.Interfaces;

public interface IReplicaFileService
{
    Task<Guid> CreateAsync(ReplicaFileModelForCreation replicaFileModelForCreation);
    Task RemoveAsync(Guid id);
}
