class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Programming Languages Metadata Search (Elasticsearch) ===");
        Console.WriteLine("Variant 11: Domain — Programming, Complex Query — Wildcard");
        Console.WriteLine();

        string esUrl = "http://localhost:9200";
        string esUser = "elastic";
        string esPass = "O9jYdcC29=6sOdkSOrfb";
        string index = "programming_languages";

        var es = new ElasticsearchService(esUrl, index, esUser, esPass);

        string mapping = @"{
            ""mappings"": {
                ""properties"": {
                ""name"": { ""type"": ""keyword"" },
                ""yearCreated"": { ""type"": ""integer"" },
                ""createdBy"": { ""type"": ""keyword"" },
                ""currentVersion"": { ""type"": ""keyword"" },
                ""popularity"": { ""type"": ""long"" },
                ""website"": { ""type"": ""keyword"" }
                }
            }
        }";

        await es.CreateIndexIfNotExistsAsync(mapping);

        while (true)
        {
            Console.WriteLine("\n1. Add document");
            Console.WriteLine("2. Delete document");
            Console.WriteLine("3. Search (term)");
            Console.WriteLine("4. Search (range)");
            Console.WriteLine("5. Search (wildcard)");
            Console.WriteLine("6. List all");
            Console.WriteLine("0. Exit");
            Console.Write("Select: ");
            var choice = Console.ReadLine();
            if (choice == "0") break;
            try
            {
                switch (choice)
                {
                    case "1":
                        await AddDoc(es);
                        break;
                    case "2":
                        await DeleteDoc(es);
                        break;
                    case "3":
                        await SearchTerm(es);
                        break;
                    case "4":
                        await SearchRange(es);
                        break;
                    case "5":
                        await SearchWildcard(es);
                        break;
                    case "6":
                        await ListAll(es);
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    static async Task AddDoc(ElasticsearchService es)
    {
        var doc = new ProgrammingLanguage();
        Console.Write("Name: ");
        doc.Name = Console.ReadLine() ?? "";
        Console.Write("Year created: ");
        doc.YearCreated = int.TryParse(Console.ReadLine(), out int y) ? y : 0;
        Console.Write("Created by: ");
        doc.CreatedBy = Console.ReadLine() ?? "";
        Console.Write("Current version: ");
        doc.CurrentVersion = Console.ReadLine() ?? "";
        Console.Write("Popularity (users): ");
        doc.Popularity = long.TryParse(Console.ReadLine(), out long p) ? p : 0;
        Console.Write("Website: ");
        doc.Website = Console.ReadLine() ?? "";
        var id = await es.AddDocumentAsync(doc);
        Console.WriteLine($"Added with id: {id}");
    }

    static async Task DeleteDoc(ElasticsearchService es)
    {
        Console.Write("Enter document id: ");
        var id = Console.ReadLine() ?? "";
        var ok = await es.DeleteDocumentAsync(id);
        Console.WriteLine(ok ? "Deleted." : "Not found or error.");
    }

    static async Task SearchTerm(ElasticsearchService es)
    {
        Console.WriteLine("Field (name, createdBy, currentVersion, website): ");
        var field = Console.ReadLine() ?? "name";
        Console.Write("Value: ");
        var value = Console.ReadLine() ?? "";
        var query = $"{{ \"query\": {{ \"term\": {{ \"{field}\": {{ \"value\": \"{value}\" }} }} }} }}";
        var res = await es.SearchAsync(query);
        PrintList(res);
    }

    static async Task SearchRange(ElasticsearchService es)
    {
        Console.WriteLine("Field (yearCreated, popularity): ");
        var field = Console.ReadLine() ?? "yearCreated";
        Console.Write("From: ");
        var from = Console.ReadLine() ?? "";
        Console.Write("To: ");
        var to = Console.ReadLine() ?? "";
        var query = $"{{ \"query\": {{ \"range\": {{ \"{field}\": {{ \"gte\": {from}, \"lte\": {to} }} }} }} }}";
        var res = await es.SearchAsync(query);
        PrintList(res);
    }

    static async Task SearchWildcard(ElasticsearchService es)
    {
        Console.WriteLine("Field (name, createdBy, currentVersion, website): ");
        var field = Console.ReadLine() ?? "name";
        Console.Write("Pattern (use * and ?): ");
        var pattern = Console.ReadLine() ?? "";
        var query = $"{{ \"query\": {{ \"wildcard\": {{ \"{field}\": {{ \"value\": \"{pattern}\", \"case_insensitive\": true }} }} }} }}";
        var res = await es.SearchAsync(query);
        PrintList(res);
    }

    static async Task ListAll(ElasticsearchService es)
    {
        var query = "{ \"query\": { \"match_all\": {} } }";
        var res = await es.SearchAsync(query);
        PrintList(res);
    }

    static void PrintList(List<ProgrammingLanguage> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("No results.");
            return;
        }
        foreach (var doc in list)
        {
            Console.WriteLine($"Id: {doc.Id}\n  Name: {doc.Name}\n  Year: {doc.YearCreated}\n  Created by: {doc.CreatedBy}\n  Version: {doc.CurrentVersion}\n  Popularity: {doc.Popularity}\n  Website: {doc.Website}\n");
        }
    }
}
