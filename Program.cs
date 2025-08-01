using CompanyPortal.Data;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Repositories.Implementaion;
using CompanyPortal.Services.Abstractions;
using CompanyPortal.Services.Implementaion;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // add unit of work and repository pattern
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

    // add services
    builder.Services.AddScoped<ICompanySignUpService, CompanySignUpService>();

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
        app.UseSwagger().UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}