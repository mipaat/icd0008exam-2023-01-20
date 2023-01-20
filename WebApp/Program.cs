using System.Globalization;
using DAL;
using DAL.Filters;
using Microsoft.AspNetCore.Localization;
using WebApp.MyLibraries.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new CustomModelBinderProvider<PerformedJobCompletionFilter>());
    });
builder.Services.AddDbContext<AppDbContext>(AppDbContextFactory.ConfigureOptions);
builder.Services.AddScoped<RepositoryContext>();

// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

// builder.Services.AddScoped<IRepositoryContext, RepositoryContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Configure cultures support
var cultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("et"),
};

app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = cultures,
    SupportedUICultures = cultures
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();