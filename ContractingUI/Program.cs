using Infrastructure.Data.Services;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ContractingUI;
using System.Security.Cryptography;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.Configuration["baseUrls:apiBase"]) });
builder.Services.AddScoped<ContractSearchService>();
builder.Services.AddScoped<ContractFlexSearchService>();
builder.Services.AddTransient<DemoTasks>();
builder.Services.AddDbContext<SearchContext>
    (opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PubDB")));
builder.Services.AddDbContext<ContractContext>
    (opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("PubDB")));


var app = builder.Build();


void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DemoTasks>();
        service.Seed();
    }
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    SeedData(app);

}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
