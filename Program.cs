using CompanyPortal.Configurations;
using CompanyPortal.Data;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Repositories.Implementaion;
using CompanyPortal.Services.Abstractions;
using CompanyPortal.Services.Implementaion;
using CompanyPortal.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // add fluent validation
    builder.Services.AddValidatorsFromAssemblyContaining<CompanySignUpDtoValidator>();

    // add unit of work and repository pattern
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
    builder.Services.AddScoped<IUserRepository, UserRepository>();
    builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();

    // add services
    builder.Services.AddScoped<ICompanySignUpService, CompanySignUpService>();
    builder.Services.AddScoped<IOtpServcie, OtpServcie>();
    builder.Services.AddScoped<IPasswordService, PasswordService>();
    builder.Services.AddScoped<ILoginService, LoginService>();
    builder.Services.AddScoped<IJwtService, JwtService>();

    // add jwt settings configuration
    builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JWT"));

    // add jwt authentication
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
               .AddJwtBearer(options =>
               {
                   options.RequireHttpsMetadata = false;
                   options.SaveToken = false;
                   options.TokenValidationParameters = new TokenValidationParameters()
                   {
                       ValidateIssuerSigningKey = true,
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidIssuer = builder.Configuration["JWT:Issuer"],
                       ValidAudience = builder.Configuration["JWT:Audience"],
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!))
                   };

               });

    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
        app.UseSwagger().UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}