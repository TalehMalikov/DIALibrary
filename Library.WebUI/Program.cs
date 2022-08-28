using Library.WebUI.Services.Abstract;
using Library.WebUI.Services.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
