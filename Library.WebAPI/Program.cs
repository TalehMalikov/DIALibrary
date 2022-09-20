using Autofac;
using Autofac.Extensions.DependencyInjection;
using Library.Business.DependencyResolvers.Autofac;
using Library.Core.DependencyResolvers;
using Library.Core.Extensions;
using Library.Entities.Concrete;
using Library.WebAPI.IdentityServer;
using Library.WebAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

var x = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var configuration = x.Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new AutofacBusinessModule(connectionString)));

// Add services to the container.
builder.Services.AddDependencyResolvers(new ICoreModule[]
{
    new CoreModule()
});


builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddIdentity<Account, Role>().AddDefaultTokenProviders();

builder.Services.AddSingleton<IPasswordHasher<Account>, CustomPasswordHasher>();
builder.Services.AddSingleton<IUserStore<Account>, AccountStore>();
builder.Services.AddSingleton<IRoleStore<Role>, RoleStore>();

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("xecretKeywqejane")),
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc($"v1", new OpenApiInfo
    {
        Title = "Eşq olsun Şəbnəmə",
        Version = "best",
        Description = "Şəbnəmin API-ı",
    });

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    c.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                      { jwtSecurityScheme, Array.Empty<string>() }
                });
});

var app = builder.Build();


app.UseMiddleware<HttpLoggerMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
