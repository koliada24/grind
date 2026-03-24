class Document
{
    public int Id { get; set; }
    public string Name { get; set; }
    public HashSet<string> Terms { get; set; }

    public Document(int id, string name, HashSet<string> terms)
    {
        Id = id;
        Name = name;
        Terms = terms;
    }
}
