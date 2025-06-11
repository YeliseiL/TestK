namespace Test.App.Infrastructure;
using AutoMapper;
using Test.App.Trees;
using Test.Domain;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateTreeCommand, Tree>()
            .ForMember(x => x.Id, opt => opt.Ignore())
            .ForMember(x => x.Name, opt => opt.MapFrom(src => src.Name));
    }
}

