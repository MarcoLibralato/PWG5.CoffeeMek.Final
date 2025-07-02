using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.Web;

public class LotApiClient(HttpClient httpClient)
{
    public async Task<Lot[]> GetLotsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        var lots = await httpClient.GetFromJsonAsync<List<Lot>>("/api/v1/lot", cancellationToken);
        return lots?.ToArray() ?? [];
    }

    public async Task<Lot?> GetLotByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<Lot>($"/api/v1/lot/{id}", cancellationToken: cancellationToken);
    }

    public async Task<Lot?> CreateLotAsync(Lot lot, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/v1/lot", lot, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Lot>(cancellationToken: cancellationToken);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nella creazione del lot: {error}");
        }
    }

    public async Task UpdateLotAsync(string id, Lot lot, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/v1/lot/{id}", lot, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nell'aggiornamento del lot: {error}");
        }
    }

    public async Task DeleteLotAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/api/v1/lot/{id}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nell'eliminazione del lot: {error}");
        }
    }
}
