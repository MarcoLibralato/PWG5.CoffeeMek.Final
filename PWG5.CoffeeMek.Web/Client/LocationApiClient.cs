using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.Web;

public class LocationApiClient(HttpClient httpClient)
{
    public async Task<Location[]> GetLocationsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        var locations = await httpClient.GetFromJsonAsync<List<Location>>("/api/v1/location", cancellationToken);
        return locations?.ToArray() ?? [];
    }

    public async Task<Location?> GetLocationByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<Location>($"/api/v1/location/{id}", cancellationToken: cancellationToken);
    }
}
