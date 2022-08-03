using Core.Extensions;
using Library.Core.DependencyResolvers;
using Library.DataAccess.Factories;

var builder = WebApplication.CreateBuilder(args);

/*var x = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
var _configuration = x.Build();
var connectionstring = _configuration.GetConnectionString("DefaultConnection");
*/


string connectionstring = "Server=95.86.133.98;Port=5432;Database=DIALibrary;User Id=ehmed;Password=Ehmed642477;";
//builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
//builder.Host.ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new AutofacBusinessModule(connectionstring)));

builder.Services.AddSingleton((t) =>
{
    return DbFactory.Create(Library.Core.Domain.Enums.ServerType.Postgre,connectionstring);
});

builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
