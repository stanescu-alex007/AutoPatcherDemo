using AutoMapper;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogCreateModels.Manually.Failed;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogCreateModels.Manually.Success;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Automatically.Success;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Manually.Failed;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogDeleteModels.Manually.Success;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Failed;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Started;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Stopped;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Automatically.Success;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Manually.StartAuto;
using FileSyncDemo.BusinessLogic.Models.ActivityLogModels.LogSyncModels.Manually.StopAuto;
using FileSyncDemo.Core.Entities;

namespace FileSyncDemo.BusinessLogic.Profiles;

public class ActivityLogProfile : Profile
{
    public ActivityLogProfile() 
    {
        //TODO: Check for a way to simplify this, maybe a generic mapping?
        // Or consider changing LogModels in something else. 
        CreateMap<AutomaticSuccessSyncLogDTO, ActivityLog>(); 
        CreateMap<AutomaticFailedSyncLogDTO, ActivityLog>(); 
        CreateMap<AutomaticStopSyncLogDTO, ActivityLog>();
        CreateMap<AutomaticStartSyncLogDTO, ActivityLog>();
        CreateMap<ManualSuccessSyncLogDTO, ActivityLog>();
        CreateMap<ManualFailedSyncLogDTO, ActivityLog>();

        CreateMap<ManualSuccessCreateLogDTO, ActivityLog>();
        CreateMap<ManualFailedCreateLogDTO, ActivityLog>();

        CreateMap<ManualSuccessDeleteLogDTO, ActivityLog>();
        CreateMap<ManualFailedDeleteLogDTO, ActivityLog>();
        CreateMap<AutomaticSuccessDeleteLogDTO, ActivityLog>();

        CreateMap<ManualAutoStartSyncLogDTO, ActivityLog>();
        CreateMap<ManualAutoStopSyncLogDTO, ActivityLog>();

        //CreateMap<LogModel, ActivityLog>().IncludeAllDerived();


    }
}
