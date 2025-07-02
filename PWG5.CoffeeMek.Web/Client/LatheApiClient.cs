using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.Web;

public class LatheApiClient(HttpClient httpClient)
{
    public async Task<LatheLog[]> GetLatheLogsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        var logs = await httpClient.GetFromJsonAsync<List<LatheLog>> ("/api/v1/lathe", cancellationToken);
        return logs?.ToArray() ?? [];
    }

    public async Task<LatheLog?> GetLatheLogByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<LatheLog>($"/api/v1/lathe/{id}", cancellationToken: cancellationToken);
    }

    public async Task<LatheLog?> CreateLatheLogAsync(LatheLog latheLog, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/v1/lathe", latheLog, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<LatheLog>(cancellationToken: cancellationToken);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nella creazione del log tornio (Lathe): {error}");
        }
    }
}
