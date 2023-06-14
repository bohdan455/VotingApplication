using DataAccess;
using WebApi.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<VotingApplicationContext>(options =>
{
    var config = builder.Configuration;
    options.UseSqlServer(config.GetConnectionString("Default")!);
});
builder.Services.AddWebRepositories();
builder.Services.AddWebServices();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
