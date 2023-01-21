public class HomeAssistant
{
    private readonly HttpClient _client;

    public HomeAssistant(HttpClient client)
    {
        _client = client;
    }

    public async Task<Entity?> Get(string entity)
    {
        return await _client.GetFromJsonAsync<Entity>($"/api/states/{entity}");
    }

    public async Task<bool> Service(string service, string action, object data)
    {
        var response = await _client.PostAsJsonAsync($"api/services/{service}/{action}", data);
        return response.IsSuccessStatusCode;
    }
}