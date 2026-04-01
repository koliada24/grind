public class ProgrammingLanguage
{
    public string Id { get; set; } // Elasticsearch document id
    public string Name { get; set; } // keyword
    public int YearCreated { get; set; } // integer
    public string CreatedBy { get; set; } // keyword
    public string CurrentVersion { get; set; } // keyword
    public long Popularity { get; set; } // long
    public string Website { get; set; } // keyword
}
