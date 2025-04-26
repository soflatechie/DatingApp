using API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.IdentityServices(builder.Configuration);

var app = builder.Build();

app.UseCors(
    x=>x.AllowAnyHeader()
    .AllowAnyMethod()
    .WithOrigins("http://localhost:4200","https://localhost:4200")
);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

/* try
{
    var context = services.GetRequiredService<DataContext>(); 
    await context.Database.MigrateAsync();
    await DbInitializer.SeedData(context);
}
catch(Exception  ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred during migration");
} */



app.Run();
