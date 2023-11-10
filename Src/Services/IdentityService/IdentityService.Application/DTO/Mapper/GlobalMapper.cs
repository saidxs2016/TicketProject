using IdentityService.Application.DTO.DataObjects;
using AutoMapper;
using IdentityService.DAL.MainDB.Entities;

namespace IdentityService.Application.DTO.Mapper;

public partial class GlobalMapper : Profile
{
    public GlobalMapper()
    {
        CreateMap<AdminDO, Admin>();
        CreateMap<Admin, AdminDO>()
            .ForMember(d => d.Password, m => m.Ignore());
    }


}
