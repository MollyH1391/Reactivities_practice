using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope(); //clean up when the job is finished
var services = scope.ServiceProvider;
try
{
    //create the database
    var context = services.GetRequiredService<DataContext>();
    //Will create the database if it does not already exist.
    await context.Database.MigrateAsync();
    await Seed.SeedData(context);
}
catch (Exception ex)
{
    //if the create database command fails, we will catch an exception
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();