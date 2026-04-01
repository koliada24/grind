using System.Text.Json.Serialization;

public class Theatre
{
    public string Id { get; set; } // Elasticsearch document id
    public string Title { get; set; } // keyword (назва вистави)
    public string Author { get; set; } // keyword (автор п'єси)
    public int YearPremiere { get; set; } // integer (рік прем'єри)
    public string Director { get; set; } // keyword (режисер)
    public string MainActor { get; set; } // keyword (головний актор)
    public int Rating { get; set; } // integer (рейтинг 1-10)
    public string Website { get; set; } // keyword (веб-сайт театру)
    
    // Text fields for full-text search (Lab4)
    [JsonPropertyName("description")]
    public string Description { get; set; } // text (standard analyzer) - опис вистави
    
    [JsonPropertyName("castReviews")]
    public string CastReviews { get; set; } // text (english analyzer) - рецензії акторів
    
    [JsonPropertyName("synopsis")]
    public string Synopsis { get; set; } // text (custom analyzer) - короткий сюжет
}
