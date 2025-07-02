using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.Web;

public class AssemblyLineApiClient(HttpClient httpClient)
{
    public async Task<AssemblyLineLog[]> GetAssemblyLineLogsAsync( CancellationToken cancellationToken = default)
    {
        var logs = await httpClient.GetFromJsonAsync<List<AssemblyLineLog>>("/api/v1/assemblyline", cancellationToken);
        return logs?.ToArray() ?? [];
    }

    public async Task<AssemblyLineLog?> GetAssemblyLineLogByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<AssemblyLineLog>($"/api/v1/assemblyline/{id}", cancellationToken);
    }

    public async Task<AssemblyLineLog?> CreateAssemblyLineLogAsync(AssemblyLineLog assemblyLineLog, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/v1/assemblyline", assemblyLineLog, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<AssemblyLineLog>(cancellationToken: cancellationToken);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nella creazione del log: {error}");
        }
    }
}