using System.Text.Json.Serialization;

public class Theatre
{
    public string Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public int YearPremiere { get; set; }
    public string Director { get; set; }
    public string MainActor { get; set; }
    public int Rating { get; set; }
    public string Website { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("castReviews")]
    public string CastReviews { get; set; }
    
    [JsonPropertyName("synopsis")]
    public string Synopsis { get; set; }
}
