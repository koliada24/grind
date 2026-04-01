using System.Text.Json.Serialization;

public class ProgrammingLanguage
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int YearCreated { get; set; }
    public string CreatedBy { get; set; }
    public string CurrentVersion { get; set; }
    public long Popularity { get; set; }
    public string Website { get; set; }
    
    [JsonPropertyName("description")]
    public string Description { get; set; }
    
    [JsonPropertyName("documentation")]
    public string Documentation { get; set; }
    
    [JsonPropertyName("communityFeedback")]
    public string CommunityFeedback { get; set; }
}
