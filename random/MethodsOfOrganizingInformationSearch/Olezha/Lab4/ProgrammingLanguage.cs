using System.Text.Json.Serialization;

public class ProgrammingLanguage
{
    public string Id { get; set; } // Elasticsearch document id
    public string Name { get; set; } // keyword
    public int YearCreated { get; set; } // integer
    public string CreatedBy { get; set; } // keyword
    public string CurrentVersion { get; set; } // keyword
    public long Popularity { get; set; } // long
    public string Website { get; set; } // keyword
    
    // Text fields for full-text search (Lab4)
    [JsonPropertyName("description")]
    public string Description { get; set; } // text (standard analyzer) - опис мови
    
    [JsonPropertyName("documentation")]
    public string Documentation { get; set; } // text (english analyzer) - документація
    
    [JsonPropertyName("communityFeedback")]
    public string CommunityFeedback { get; set; } // text (custom analyzer) - відгуки спільноти
}
