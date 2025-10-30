using Microsoft.EntityFrameworkCore;
using UserVault.Data;
using UserVault.DependencyExtensions;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register DbContext
//builder.Services.AddDbContext<EFDbContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnectionString")));

builder.Services.AddDbContext<EFDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString")));

// ✅ Bind only the "AppSettings" section
var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();

// Register AppSettings in DI for direct use (singleton)
builder.Services.AddSingleton(appSettings);

// Also register for IOptions<AppSettings> pattern if needed
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// ✅ Register other application services (with appSettings if needed)
builder.Services.AddServices(appSettings);

// ✅ Add controllers, Swagger, AutoMapper
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// ✅ Configure HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
