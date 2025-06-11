using System.Net;
using Test.App.Exceptions;
using Test.Domain;
using Test.Persistence;

namespace Test.API.Exceptions;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, TestDbContext dbContext)
    {
        try
        {
            await _next(context);
        }
        catch (SecureException ex)
        {
            await HandleSecureExceptionAsync(context, dbContext, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, dbContext, ex);
        }
    }

    private async Task HandleSecureExceptionAsync(HttpContext context, TestDbContext dbContext, Exception ex)
    {
        long eventId = await SaveException(context, dbContext, ex, ExceptionType.Secure);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new ExceptionResponse
        {
            Type = ExceptionType.Secure.ToString(),
            Id = eventId,
            Data = new Data { Message = ex.Message }
        };

        await context.Response.WriteAsJsonAsync(response);
    }
    private async Task HandleExceptionAsync(HttpContext context, TestDbContext dbContext, Exception ex)
    {
        long eventId = await SaveException(context, dbContext, ex, ExceptionType.Exception);

        var response = new ExceptionResponse
        {
            Type = ExceptionType.Exception.ToString(),
            Id = eventId,
            Data = new Data { Message = $"Internal server error ID = {eventId}" }
        };

        await context.Response.WriteAsJsonAsync(response);
    }
    private static async Task<long> SaveException(HttpContext context, TestDbContext dbContext, Exception ex, ExceptionType type)
    {
        var eventId = DateTime.UtcNow.Ticks;
        var exceptionLog = new ExceptionLog
        {
            Timestamp = DateTime.UtcNow,
            QueryParameters = context.Request.Method + " " + context.Request.Path + " " + context.Request.QueryString,
            BodyParameters = await new StreamReader(context.Request.Body).ReadToEndAsync(),
            StackTrace = type == ExceptionType.Secure ? ex.Message : ex.StackTrace
        };

        dbContext.ExceptionLogs.Add(exceptionLog);
        await dbContext.SaveChangesAsync();
        return eventId;
    }
}
public enum ExceptionType
{
    None = 0,
    Secure = 1,
    Exception = 2,
}