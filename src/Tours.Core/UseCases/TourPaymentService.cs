using System.Text.Json;
using Tours.Core.UseCases.Interfaces;

namespace Tours.Core.UseCases;

public class TourPaymentService : ITourPaymentService
{
    private HttpClient client;
    private string baseUrl = Environment.GetEnvironmentVariable("PAYMENT_API_URL") ?? "http://localhost:8087/api/payment/purchaseToken";
    public TourPaymentService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<bool> HasUserBoughtTour(long userId, long tourId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}{tourId}");
        request.Headers.Add("X-User-Id", userId.ToString());
        
        var response = await client.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        var json = await response.Content.ReadAsStringAsync();
        if (bool.TryParse(json, out var result))
        {
            return result;
        }

        return false;
    }
}