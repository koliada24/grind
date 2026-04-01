using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ElasticsearchService
{
    private readonly HttpClient _client;
    private readonly string _baseUrl;
    private readonly string _index;
    private readonly string _authHeader;
    private readonly JsonSerializerOptions _jsonOptions;

    public ElasticsearchService(string baseUrl, string index, string username, string password)
    {
        _baseUrl = baseUrl.TrimEnd('/');
        _index = index;
        
        _jsonOptions = new JsonSerializerOptions 
        { 
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        
        var handler = new HttpClientHandler();
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
        
        _client = new HttpClient(handler);
        var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}"));
        _authHeader = $"Basic {auth}";
        _client.DefaultRequestHeaders.Add("Authorization", _authHeader);
    }

    public async Task<bool> RecreateIndexAsync(string mappingJson)
    {
        try
        {
            await _client.DeleteAsync($"{_baseUrl}/{_index}");
        }
        catch { }
        
        var resp = await _client.PutAsync($"{_baseUrl}/{_index}", new StringContent(mappingJson, Encoding.UTF8, "application/json"));
        return resp.IsSuccessStatusCode;
    }

    public async Task<bool> CreateIndexIfNotExistsAsync(string mappingJson)
    {
        var resp = await _client.PutAsync($"{_baseUrl}/{_index}", new StringContent(mappingJson, Encoding.UTF8, "application/json"));
        return resp.IsSuccessStatusCode || resp.StatusCode == System.Net.HttpStatusCode.BadRequest;
    }

    public async Task<string> AddDocumentAsync(Theatre doc)
    {
        var json = JsonSerializer.Serialize(doc, _jsonOptions);
        var resp = await _client.PostAsync($"{_baseUrl}/{_index}/_doc", new StringContent(json, Encoding.UTF8, "application/json"));
        var respJson = await resp.Content.ReadAsStringAsync();
        using var docObj = JsonDocument.Parse(respJson);
        var id = docObj.RootElement.GetProperty("_id").GetString();
        
        // Refresh index to ensure document is immediately searchable
        await _client.PostAsync($"{_baseUrl}/{_index}/_refresh", null);
        
        return id;
    }

    public async Task<bool> DeleteDocumentAsync(string id)
    {
        var resp = await _client.DeleteAsync($"{_baseUrl}/{_index}/_doc/{id}");
        return resp.IsSuccessStatusCode;
    }

    public async Task<List<Theatre>> SearchAsync(string queryJson)
    {
        var resp = await _client.PostAsync($"{_baseUrl}/{_index}/_search", new StringContent(queryJson, Encoding.UTF8, "application/json"));
        var respJson = await resp.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(respJson);
        var list = new List<Theatre>();
        foreach (var hit in doc.RootElement.GetProperty("hits").GetProperty("hits").EnumerateArray())
        {
            var src = hit.GetProperty("_source").GetRawText();
            var theatre = JsonSerializer.Deserialize<Theatre>(src, _jsonOptions);
            theatre.Id = hit.GetProperty("_id").GetString();
            list.Add(theatre);
        }
        return list;
    }
}
