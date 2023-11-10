using AutoMapper;
using TicketService.Application.DTO.DataObjects;
using TicketService.DAL.MainDB.Entities;

namespace TicketService.Application.DTO.Mapper;

public partial class GlobalMapper : Profile
{
    public GlobalMapper()
    {
        CreateMap<TicketDO, Ticket>().ReverseMap();

    }


}
