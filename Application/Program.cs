using Application.Data;
using Application.Interfaces;
using Application.Services;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddBlazorContextMenu();
//builder.Services.AddControllers();
//builder.Services.AddLogging();

builder.Services.AddSingleton<WeatherForecastService>();

// Add services to the container
builder.Services.AddSingleton<IBrowserService, BrowserService>();
builder.Services.AddSingleton<IContextMenuService, ContextMenuService>();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
builder.Services.AddSingleton<IDialogService, DialogService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<ILayoutService, LayoutService>();
builder.Services.AddSingleton<ISessionService, SessionService>();

string databasePath = DatabaseService.InitializeEnvironment(builder.Configuration);
builder.Services.AddDbContext<RootContext>(contextLifetime: ServiceLifetime.Singleton, optionsAction: _OptionsSetup);
builder.Services.AddDbContext<SessionContext>(contextLifetime: ServiceLifetime.Singleton, optionsAction: _OptionsSetup);
builder.Services.AddDbContext<WindowContext>(contextLifetime: ServiceLifetime.Singleton, optionsAction: _OptionsSetup);
builder.Services.AddDbContext<TabContext>(contextLifetime: ServiceLifetime.Singleton, optionsAction: _OptionsSetup);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
   app.UseExceptionHandler("/Error");
   // The default HSTS value is 30 days
   // You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts
   app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

return;

void _OptionsSetup(DbContextOptionsBuilder options)
{
   options.UseSqlite($"Data Source={databasePath};");
   options.EnableSensitiveDataLogging();
}
