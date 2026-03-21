class InvertedIndex
{
    private Dictionary<string, HashSet<int>> index;
    private int totalDocuments => index.Count;

    public InvertedIndex()
    {
        index = new Dictionary<string, HashSet<int>>();
    }

    public void AddDocument(Document document)
    {
        foreach (var term in document.Terms)
        {
            if (!index.ContainsKey(term))
            {
                index[term] = new HashSet<int>();
            }
            index[term].Add(document.Id);
        }
    }

    public HashSet<int> GetDocumentsForTerm(string term)
    {
        return index.ContainsKey(term) ? [..index[term]] : new HashSet<int>();
    }

    public HashSet<int> GetAllDocumentIds()
    {
        var allDocs = new HashSet<int>();
        for (int i = 0; i < totalDocuments; i++)
        {
            allDocs.Add(i);
        }
        return allDocs;
    }

    public void DisplayIndex(HashSet<string> terms)
    {
        Console.WriteLine("\n=== Inverted Index ===");
        foreach (var term in terms.OrderBy(t => t))
        {
            var docs = GetDocumentsForTerm(term);
            Console.WriteLine($"{term}: [{string.Join(", ", docs.OrderBy(d => d))}]");
        }
        Console.WriteLine();
    }
}
