var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddRazorPages();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapRazorPages();

app.Run();