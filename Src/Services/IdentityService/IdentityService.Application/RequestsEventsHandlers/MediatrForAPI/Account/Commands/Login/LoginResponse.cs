using IdentityService.Application.DTO.DataObjects;
using IdentityService.Application.DTO.Models;

namespace IdentityService.Application.RequestsEventsHandlers.MediatrForAPI.Account.Commands.Login;

public class LoginResponse : ResponseModel
{
    public string? Token { get; set; }
}

