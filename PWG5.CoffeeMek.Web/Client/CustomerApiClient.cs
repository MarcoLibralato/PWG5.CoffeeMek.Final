using PWG5.CoffeeMek.Data.Models;

namespace PWG5.CoffeeMek.Web;

public class CustomerApiClient(HttpClient httpClient)
{
    public async Task<Customer[]> GetCustomersAsync( CancellationToken cancellationToken = default)
    {
        var customers = await httpClient.GetFromJsonAsync<List<Customer>>("/api/v1/customer", cancellationToken);
        return customers?.ToArray() ?? [];
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<Customer>($"/api/v1/customer/{id}", cancellationToken: cancellationToken);
    }

    public async Task<Customer?> CreateCustomerAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/v1/customer", customer, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<Customer>(cancellationToken: cancellationToken);
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nella creazione del customer: {error}");
        }
    }

    public async Task UpdateCustomerAsync(int id, Customer customer, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/v1/customer/{id}", customer, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nell'aggiornamento del customer: {error}");
        }
    }

    public async Task DeleteCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/api/v1/customer/{id}", cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new Exception($"Errore nell'eliminazione del customer: {error}");
        }
    }
}
