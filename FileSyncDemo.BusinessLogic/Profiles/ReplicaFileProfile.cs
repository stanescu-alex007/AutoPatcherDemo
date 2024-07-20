using AutoMapper;
using FileSyncDemo.BusinessLogic.Models.ReplicaModels;
using FileSyncDemo.Core.Entities;

namespace FileSyncDemo.BusinessLogic.Profiles;

public class ReplicaFileProfile : Profile
{

    public ReplicaFileProfile()
    {
        CreateMap<ReplicaFile, ReplicaFileModelForCreation>().ReverseMap();
    }

}
