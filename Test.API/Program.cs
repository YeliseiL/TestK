using Test.API.Exceptions;
using Test.API.ServiceCollection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddProjectDbContexts(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomCors();
builder.Services.AddAuthentication();
var app = builder.Build();
app.Lifetime.ApplicationStarted.Register(app.DatabaseMigrate);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI()
        .UseCors(Test.API.ServiceCollection.CorsPolicy.DEFAULT);
}
app.UseCertificateForwarding();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Use(async (context, next) =>
{
    context.Request.EnableBuffering();
    await next();
});
app.MapControllers();

await app.RunAsync();
