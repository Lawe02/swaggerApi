using System.Reflection;
using api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc ("v1", new OpenApiInfo 
    {
         Title = "My Web API",
         Version = "v1",
         Contact = new OpenApiContact{
            Name = "Lawe Zangena",
            Email = "lawza02@gmail.com"
         } 
    });

    // Needed for XML documentation to work
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

});

builder.Services.AddDbContext<ApplicationDbContext>(options => 
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<DataInitializer>();


var app = builder.Build();

// Do the MigrateDAta

using( var scope = app.Services.CreateScope())
{
    scope.ServiceProvider.GetService<DataInitializer>().MigrateData();
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Web API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
 
app.Run();
