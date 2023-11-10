using AutoMapper;
using IdentityService.Application.DTO.DataObjects;
using IdentityService.Application.DTO.Exceptions;
using IdentityService.Application.DTO.ResultType;
using IdentityService.Core.Security.Jwt;
using IdentityService.DAL.MainDB.Entities;
using IdentityService.DAL.MainDB.Repositories.Interfaces;
using MediatR;

namespace IdentityService.Application.RequestsEventsHandlers.MediatrForAPI.Account.Commands.Login;

public class LoginHandler : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    private readonly IMapper _mapper;
    private readonly IAdminRepository _adminRepository;
    private readonly IJwtHelper _jwtHelper;



    public LoginHandler(IMapper mapper, IAdminRepository adminRepository, IJwtHelper jwtHelper)
    {
        _mapper = mapper;
        _adminRepository = adminRepository;
        _jwtHelper = jwtHelper;
    }
    public async Task<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = new Result<LoginResponse>(IsSuccess: true, Message: "", ResponseLogging: true);
        var model = new LoginResponse();
        try
        {

            var entity = await _adminRepository.GetAsFirstOrDefaultAsync(w => w.Username == request.Username && w.Password == request.Password, cancellationToken) ?? new Admin();
          
            var claims = new
            {
                FullName = entity.Name + " " + entity.Surname,
                UserName = entity.Username
            };

            var token = _jwtHelper.CreateToken(claims);

            //var dto = _mapper.Map<AdminDO>(entity);
            //dto.RefreshToken = token.ReToken;
            //dto.RefreshTokenExpiration = token.ReTokenExpiration;
            //dto.Token = token.Token;

            model.Token = token.Token;
            result.Data = model;
            result.Message = "Token Oluşturuldu.";
            result.IsSuccess = true;
        }
        catch (Exception ex)
        {
            throw new ModelException(new Result<object> { IsSuccess = false, ResultType = ResultTypeEnum.Warning, Data = request, Message = ex.Message });
        }
        return result;
    }

 


   
}

