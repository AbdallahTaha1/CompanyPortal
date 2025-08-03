using CompanyPortal.Extentions;
using Microsoft.Extensions.FileProviders;


var builder = WebApplication.CreateBuilder(args);
{

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Modular service registration
    builder.Services.AddFluentValidationServices();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddRepositoryServices();
    builder.Services.AddDomainServices();
    builder.Services.AddDatabase(builder.Configuration);
    builder.Services.AddSwaggerWithJwt();
    builder.Services.AddJwtAuthentication(builder.Configuration);
    builder.Services.AddCorsServices();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
        app.UseSwagger().UseSwaggerUI();

    app.UseHttpsRedirection();

    app.UseCors("AllowAngularApp");

    app.UseStaticFiles();

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "Uploads")),
        RequestPath = "/Uploads"
    });

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}