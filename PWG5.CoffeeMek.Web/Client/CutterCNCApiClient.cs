using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.Web;

public class CutterCNCApiClient(HttpClient httpClient)
{
    public async Task<CutterCNCLog[]> GetCutterCNCLogsAsync(CancellationToken cancellationToken = default)
    {

        var logs = await httpClient.GetFromJsonAsync<List<CutterCNCLog>>("/api/v1/cuttercnc", cancellationToken);
        return logs?.ToArray() ?? [];
    }

    public async Task<CutterCNCLog?> GetCutterCNCLogByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<CutterCNCLog>($"/api/v1/cuttercnc/{id}", cancellationToken: cancellationToken);
    }

    public async Task<CutterCNCLog?> CreateCutterCNCLogAsync(CutterCNCLog cutterCNCLog, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/v1/cuttercnc", cutterCNCLog, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<CutterCNCLog>(cancellationToken: cancellationToken);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nella creazione del log Cutter CNC: {error}");
        }
    }
}
