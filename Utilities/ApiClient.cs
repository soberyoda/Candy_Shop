using System.Text.Json;

namespace Candy_Shop.Utilities;

public static class ApiClient {
  private static HttpClient client = new() {
    BaseAddress = new Uri("http://localhost:5112/")
  };

  public static async Task<T?> Get<T>(string url, Options? options = null) {
    var request = PrepareMessage(url, HttpMethod.Get, options);
    
    var response = await client.SendAsync(request);
    return await ParseResponse<T>(response);
  }

  public static async Task<T?> Post<T>(string url, IFormCollection? form = null, Options? options = null) {
    var request = PrepareMessage(url, HttpMethod.Post, options);

    if (form is not null) {
      request.Content = FormToMultipart(form);
    }
  
    var response = await client.SendAsync(request);
    return await ParseResponse<T>(response);
  }
  public static async Task<bool> Delete(string url, Options? options = null) {
    var request = PrepareMessage(url, HttpMethod.Delete, options);
    var response = await client.SendAsync(request);
    return response.IsSuccessStatusCode;
  }


  private static HttpRequestMessage PrepareMessage(string url, HttpMethod method, Options? options = null) {
    var request = new HttpRequestMessage(method, url);

    if (options?.ApiKey is not null) {
      request.Headers.Add("x-api-key", options.ApiKey);
    }

    return request;
  }

  private static async Task<T?> ParseResponse<T>(HttpResponseMessage response) {
    if (!response.IsSuccessStatusCode) return default;
    string jsonString = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<T>(jsonString);
  }

  private static MultipartFormDataContent FormToMultipart(IFormCollection form) {
    MultipartFormDataContent content = new();

    foreach (string key in form.Keys) {
      var value = form[key];

      if (value.Count == 1) {
        content.Add(new StringContent(value[0] ?? ""), key);
      } else {
        foreach (string? item in value) {
          content.Add(new StringContent(item ?? ""), key);
        }
      }
    }

    return content;
  }

  public record Options {
    public string? ApiKey;
  }
}
