using FreelanceApp.Interfaces;
using dotenv.net;
using FreelanceApp.Models;
using FreelanceApp.Repository;

var builder = WebApplication.CreateBuilder(args);

DotEnv.Load();
var fe_url = Environment.GetEnvironmentVariable("FE_URL");
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<CdnMainContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure CORS
if (!string.IsNullOrEmpty(fe_url))
{
    app.UseCors(options =>
    {
        options.WithOrigins(fe_url)
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
}
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


// app.UseHttpsRedirection();
app.MapControllers();
app.Run();

