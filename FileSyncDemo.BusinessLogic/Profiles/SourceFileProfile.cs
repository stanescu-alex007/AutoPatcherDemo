using AutoMapper;
using FileSyncDemo.BusinessLogic.Models.SourceModels;
using FileSyncDemo.Core.Entities;

namespace FileSyncDemo.BusinessLogic.Profiles;

public class SourceFileProfile : Profile
{
    public SourceFileProfile()
    {
        CreateMap<SourceFile, SourceFileModelForCreation>().ReverseMap();
    }
}
