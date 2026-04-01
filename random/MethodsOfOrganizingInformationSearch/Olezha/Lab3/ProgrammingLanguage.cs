public class ProgrammingLanguage
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public int YearCreated { get; set; }
    public string CreatedBy { get; set; } = null!;
    public string CurrentVersion { get; set; } = null!;
    public long Popularity { get; set; }
    public string Website { get; set; } = null!;
}
