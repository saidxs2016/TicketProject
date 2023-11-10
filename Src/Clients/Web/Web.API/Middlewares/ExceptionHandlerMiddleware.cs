namespace Web.API.Middlewares;

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
        }
        catch (BadHttpRequestException ex)
        {
           
        }        
        //catch (ValidationException ex)
        //{
        //}

        //catch (ModelException ex)
        //{
           
        //}

        catch (Exception ex)
        {
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
