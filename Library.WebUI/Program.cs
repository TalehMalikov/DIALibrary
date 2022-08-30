using System.Globalization;
using Library.WebUI.Services.Abstract;
using Library.WebUI.Services.Concrete;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#region Dependency injection
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<IExternalSourceService, ExternalSourceService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IActivityService, ActivityService>();
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IStudentService, StudentService>();
builder.Services.AddSingleton<IFacultyService, FacultyService>();
builder.Services.AddSingleton<ISectorService, SectorService>();
builder.Services.AddSingleton<ISpecialtyService, SpecialtyService>();
builder.Services.AddSingleton<IActivityService, ActivityService>();
builder.Services.AddSingleton<IUserService, UserService>();
#endregion

builder.Services.AddLocalization(p => { p.ResourcesPath = "Resources"; });
builder.Services.AddControllersWithViews().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("az-Latn-AZ"),
        new CultureInfo("en-US"),
        new CultureInfo("ru-RU")
    };
    options.DefaultRequestCulture = new RequestCulture(culture: "az-Latn-AZ", uiCulture: "az-Latn-AZ");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});
builder.Services.AddSingleton<IFileTypeService, FileTypeService>();


builder.Services.AddRazorPages(options =>
{
    options.Conventions.AddPageRoute("/Contact", "Category/GetNewAddedBooks/{id?}");
});

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(sessions =>
{
    sessions.IdleTimeout = TimeSpan.FromHours(1);
    sessions.Cookie.IsEssential = true;
    sessions.Cookie.HttpOnly = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
