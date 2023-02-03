using CompanyApplicationAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSqlServer(builder.Configuration);
builder.Services.AddDependancy(builder.Configuration);
builder.Services.ConfigureCors();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.AddExceptionHandler();

app.UseHttpsRedirection();

app.AddCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
