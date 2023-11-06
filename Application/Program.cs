using Application.Interfaces;
using Application.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IBrowserService, BrowserService>();
builder.Services.AddSingleton<IContextMenuService, ContextMenuService>();
builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
builder.Services.AddSingleton<IDialogService, DialogService>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<ILayoutService, LayoutService>();
builder.Services.AddSingleton<ISessionService, SessionService>();
builder.Services.AddBlazorContextMenu();

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
