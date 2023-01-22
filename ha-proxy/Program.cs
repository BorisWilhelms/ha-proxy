using System.Net.Http.Headers;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var configurationPath = builder.Configuration["ConfigurationPath"];
if (!String.IsNullOrWhiteSpace(configurationPath) && Directory.Exists(configurationPath))
{
    builder.Configuration.AddKeyPerFile(configurationPath);
}
    
builder.Services.AddRazorPages();
builder.Services.Configure<HomeAssistantOptions>(builder.Configuration.GetSection("HomeAssistant"));
builder.Services.AddHttpClient<HomeAssistant>((provider, client) =>
{
    var options = provider.GetRequiredService<IOptions<HomeAssistantOptions>>();
    client.BaseAddress = new Uri(options.Value.BaseUrl);
    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", options.Value.AccessToken);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();
app.MapRazorPages();

app.Run();