using AutoMapper;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels;
using FileSyncDemo.BusinessLogic.Services.Interfaces;
using FileSyncDemo.Core.Entities;
using FileSyncDemo.Core.Persistance;

namespace FileSyncDemo.BusinessLogic.Services.Implementations;

public class ActivityLogService : IActivityLogService
{
    private readonly IRepository<ActivityLog> _fileRepository;
    private readonly IMapper _mapper;

    public ActivityLogService(IRepository<ActivityLog> fileRepository, IMapper mapper)
    {
        _fileRepository = fileRepository;
        _mapper = mapper;
    }

    public async Task LogAsync<T>(T logModel) where T : LogModel
    {
        var currentLogModel = _mapper.Map<ActivityLog>(logModel);
        
        await _fileRepository.CreateAsync(currentLogModel);
        await _fileRepository.SaveChangesAsync();
    }
}
