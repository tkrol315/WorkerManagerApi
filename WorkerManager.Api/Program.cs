using WorkerManager.Api;
using WorkerManager.Application;
using WorkerManager.Infrastructure;
using WorkerManager.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddShared();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.ApplyMigrations();
    app.Seed();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseShared();
app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();
