public class Document
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<string> Terms { get; set; }

    public Document(int id, string title, string content)
    {
        Id = id;
        Title = title;
        Content = content;
        Terms = new List<string>();
        ParseTerms();
    }

    private void ParseTerms()
    {
        Terms.Clear();

        if (string.IsNullOrWhiteSpace(Content))
        {
            return;
        }

        var terms = Content.ToLower().Split([' ', '\t', '\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
        Terms.AddRange(terms);
    }

    public int GetTermFrequency(string term)
    {
        return Terms.Count(t => t.Equals(term, StringComparison.OrdinalIgnoreCase));
    }
}
