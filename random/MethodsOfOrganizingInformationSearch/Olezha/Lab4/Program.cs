class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Programming Languages Full-Text Search (Elasticsearch) ===");
        Console.WriteLine("Lab 4: Variant 11 - Domain: Programming, Full-Text Search Implementation");
        Console.WriteLine();

        string esUrl = "https://localhost:9201";
        string esUser = "elastic";
        string esPass = "*T*4DCnmL5q1*GYpc1Lk";
        string index = "programming_languages";

        var es = new ElasticsearchService(esUrl, index, esUser, esPass);

        string mapping = @"{
            ""settings"": {
                ""analysis"": {
                    ""analyzer"": {
                        ""custom_feedback_analyzer"": {
                            ""type"": ""custom"",
                            ""char_filter"": [""html_strip""],
                            ""tokenizer"": ""standard"",
                            ""filter"": [""lowercase"", ""stop""]
                        }
                    }
                }
            },
            ""mappings"": {
                ""properties"": {
                ""name"": { ""type"": ""keyword"" },
                ""yearCreated"": { ""type"": ""integer"" },
                ""createdBy"": { ""type"": ""keyword"" },
                ""currentVersion"": { ""type"": ""keyword"" },
                ""popularity"": { ""type"": ""long"" },
                ""website"": { ""type"": ""keyword"" },
                ""description"": { ""type"": ""text"", ""analyzer"": ""standard"" },
                ""documentation"": { ""type"": ""text"", ""analyzer"": ""english"" },
                ""communityFeedback"": { ""type"": ""text"", ""analyzer"": ""custom_feedback_analyzer"" }
                }
            }
        }";

        await es.CreateIndexIfNotExistsAsync(mapping);

        while (true)
        {
            Console.WriteLine("\n=== Lab 4: Full-Text Search ===");
            Console.WriteLine("1. Add document");
            Console.WriteLine("2. Delete document");
            Console.WriteLine("3. Search (term)");
            Console.WriteLine("4. Search (range)");
            Console.WriteLine("5. Search (wildcard)");
            Console.WriteLine("6. Search (full-text: description)");
            Console.WriteLine("7. Search (full-text: documentation)");
            Console.WriteLine("8. Search (full-text: community feedback)");
            Console.WriteLine("9. List all");
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
                        await SearchDescription(es);
                        break;
                    case "7":
                        await SearchDocumentation(es);
                        break;
                    case "8":
                        await SearchCommunityFeedback(es);
                        break;
                    case "9":
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
        Console.Write("Description (text): ");
        doc.Description = Console.ReadLine() ?? "";
        Console.Write("Documentation (text): ");
        doc.Documentation = Console.ReadLine() ?? "";
        Console.Write("Community feedback (text): ");
        doc.CommunityFeedback = Console.ReadLine() ?? "";
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
        Console.WriteLine("Field (Name, YearCreated, CreatedBy, Website): ");
        var field = Console.ReadLine() ?? "Name";
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
        Console.WriteLine("Field (Name, YearCreated, CreatedBy, Website): ");
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

    static async Task SearchDescription(ElasticsearchService es)
    {
        Console.Write("Search in description (query): ");
        var query_text = Console.ReadLine() ?? "";
        var query = $"{{ \"query\": {{ \"match\": {{ \"description\": {{ \"query\": \"{query_text}\" }} }} }} }}";
        var res = await es.SearchAsync(query);
        PrintList(res);
    }

    static async Task SearchDocumentation(ElasticsearchService es)
    {
        Console.Write("Search in documentation (query): ");
        var query_text = Console.ReadLine() ?? "";
        var query = $"{{ \"query\": {{ \"match\": {{ \"documentation\": {{ \"query\": \"{query_text}\" }} }} }} }}";
        var res = await es.SearchAsync(query);
        PrintList(res);
    }

    static async Task SearchCommunityFeedback(ElasticsearchService es)
    {
        Console.Write("Search in community feedback (query): ");
        var query_text = Console.ReadLine() ?? "";
        var query = $"{{ \"query\": {{ \"match\": {{ \"communityFeedback\": {{ \"query\": \"{query_text}\" }} }} }} }}";
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
            Console.WriteLine($"Id: {doc.Id}\n  Name: {doc.Name}\n  Year: {doc.YearCreated}\n  Created by: {doc.CreatedBy}\n  Version: {doc.CurrentVersion}\n  Popularity: {doc.Popularity}\n  Website: {doc.Website}\n  Description: {doc.Description}\n  Documentation: {doc.Documentation}\n  Community Feedback: {doc.CommunityFeedback}\n");
        }
    }
}
