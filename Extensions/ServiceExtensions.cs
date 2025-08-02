using CompanyPortal.Configurations;
using CompanyPortal.Data;
using CompanyPortal.Data.Entities;
using CompanyPortal.Repositories.Abstractions;
using CompanyPortal.Repositories.Implementaion;
using CompanyPortal.Services.Abstractions;
using CompanyPortal.Services.Implementaion;
using CompanyPortal.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CompanyPortal.Extentions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<CompanySignUpDtoValidator>();
            return services;
        }
        public static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IOtpServcie, OtpServcie>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<ICompanyService, CompanyService>();

            services.AddScoped<PasswordHasher<User>>();

            return services;
        }
        public static IServiceCollection AddSwaggerWithJwt(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Add JWT Authentication to Swagger
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme.  
                    Enter 'Bearer' [space] and then your token in the text input below.  
                    Example: `Bearer eyJhbGciOiJI...`",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2", // Note: this value doesn't affect anything in ApiKey scheme
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // add jwt settings configuration
            services.Configure<JwtSettings>(configuration.GetSection("JWT"));
            // add jwt authentication
            var jwtSection = configuration.GetSection("Jwt");
            var key = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

            services.AddAuthentication(options =>
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
                               ValidateIssuer = false,
                               ValidateAudience = false,
                               ValidIssuer = jwtSection["JWT:Issuer"],
                               ValidAudience = jwtSection["JWT:Audience"],
                               IssuerSigningKey = new SymmetricSecurityKey(key)
                           };

                       });

            return services;
        }
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ??
       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


            services.AddDbContext<AppDbContext>(options =>
        options.UseNpgsql(connectionString));

            return services;
        }

    }
}
