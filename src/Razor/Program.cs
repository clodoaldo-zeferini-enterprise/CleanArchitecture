using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Razor.Services;

var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables()
    .AddCommandLine(args)
    .Build();

var teste = Configuration.GetValue("APIBaseAddress", "");

builder.Services.AddHttpClient("Member", HttpClient =>
{
    HttpClient.BaseAddress = new Uri(Configuration.GetValue("APIBaseAddress",""));
}
);

// Add services to the container.
builder.Services.AddScoped<IMemberService, MemberService>();

builder.Services.AddRazorPages();

/*
builder.Services.AddDbContext<FrontEndContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("FrontEndContext") ?? throw new InvalidOperationException("Connection string 'FrontEndContext' not found.")));
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
