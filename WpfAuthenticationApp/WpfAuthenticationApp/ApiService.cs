using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ApiService
{
    private readonly string _baseUrl;
    private readonly string _token;

    public ApiService(string token)
    {
        _baseUrl = "https://localhost:7046/api";  // Replace with your API's base URL
        _token = token;
    }

    // GET request method
    public async Task<string> GetDataAsync(string endpoint)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var response = await client.GetAsync($"{_baseUrl}/{endpoint}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Failed to fetch data: {response.StatusCode}");
            }
        }
    }

    // POST request method
    public async Task<string> PostDataAsync(string endpoint, object payload)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            var jsonPayload = JsonConvert.SerializeObject(payload);
            var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{_baseUrl}/{endpoint}", content);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Failed to post data: {response.StatusCode}");
            }
        }
    }
}
