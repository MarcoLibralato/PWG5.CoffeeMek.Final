using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.Web;

public class TestLineApiClient(HttpClient httpClient)
{
    public async Task<TestLineLog[]> GetTestLineLogsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        var logs = await httpClient.GetFromJsonAsync<List<TestLineLog>>("/api/v1/testline", cancellationToken);
        return logs?.ToArray() ?? [];
    }

    public async Task<TestLineLog?> GetTestLineLogByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<TestLineLog>($"/api/v1/testline/{id}", cancellationToken: cancellationToken);
    }

    public async Task<TestLineLog?> CreateTestLineLogAsync(TestLineLog testLineLog, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/v1/testline", testLineLog, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TestLineLog>(cancellationToken: cancellationToken);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nella creazione del test line log: {error}");
        }
    }
}
