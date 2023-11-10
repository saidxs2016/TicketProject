using IdentityService.Application.DTO.Exceptions;
using IdentityService.Application.DTO.ResultType;
using IdentityService.Core.DTO.Enums;
using FluentValidation;
using System.Diagnostics;
using System.Net;

namespace IdentityService.API.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }

        catch (HttpRequestException ex)
        {
            var error_code = ex.StatusCode;
            if (ex.StatusCode == null)
                error_code = System.Net.HttpStatusCode.ServiceUnavailable;

            httpContext.Response.StatusCode = (int)error_code;
            _ = Task.Run(() => _logger.LogError(ex, ex.Message));



            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(new Result<object>(IsSuccess: false, Message: ex.Message));
        }
        catch (BadHttpRequestException ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            _ = Task.Run(() => _logger.LogError(ex, ex.Message));


            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(new Result<object>(IsSuccess: false, Message: ex.Message));
        }        

        
        catch (ValidationException ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            var errors = string.Join(", ", ex.Errors.Select(w => w.ErrorMessage).ToArray());
            _ = Task.Run(() => _logger.LogError(ex, ex.Message));


            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(new Result<object>(IsSuccess: false, Message: errors));
        }

        catch (ModelException ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            _ = Task.Run(() => _logger.LogError(ex, ex.Message));


            //await httpContext.Response.WriteAsync(ex.Message);

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(ex.Result);
        }

        catch (Exception ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            _ = Task.Run(() => _logger.LogError(ex, ex.Message));

            //await httpContext.Response.WriteAsync(ex.Message);

            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsJsonAsync(new Result<object>(IsSuccess: false, Message: ex.Message));
        }
        finally
        {
        }

    }

}

public static class ExceptionMiddleware
{
    public static WebApplication UseExceptionMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        return app;
    }
}
