class Program
{
    static async Task Main(string[] args)
    {
        System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };

        Console.WriteLine("=== Theatre Full-Text Search (Elasticsearch) ===");
        Console.WriteLine("Lab 4: Variant 14 - Domain: Theatre, Full-Text Search Implementation");
        Console.WriteLine();

        string esUrl = "https://localhost:9201";
        string esUser = "elastic";
        string esPass = "*T*4DCnmL5q1*GYpc1Lk";
        string index = "theatre";

        var es = new ElasticsearchService(esUrl, index, esUser, esPass);

        string mapping = @"{
            ""settings"": {
                ""analysis"": {
                    ""analyzer"": {
                        ""custom_synopsis_analyzer"": {
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
                ""title"": { ""type"": ""keyword"" },
                ""author"": { ""type"": ""keyword"" },
                ""yearPremiere"": { ""type"": ""integer"" },
                ""director"": { ""type"": ""keyword"" },
                ""mainActor"": { ""type"": ""keyword"" },
                ""rating"": { ""type"": ""integer"" },
                ""website"": { ""type"": ""keyword"" },
                ""description"": { ""type"": ""text"", ""analyzer"": ""standard"" },
                ""castReviews"": { ""type"": ""text"", ""analyzer"": ""english"" },
                ""synopsis"": { ""type"": ""text"", ""analyzer"": ""custom_synopsis_analyzer"" }
                }
            }
        }";

        await es.RecreateIndexAsync(mapping);

        while (true)
        {
            Console.WriteLine("\n=== Lab 4: Full-Text Search ===");
            Console.WriteLine("1. Add document");
            Console.WriteLine("2. Delete document");
            Console.WriteLine("3. Search (term)");
            Console.WriteLine("4. Search (range)");
            Console.WriteLine("5. Search (wildcard)");
            Console.WriteLine("6. Search (full-text: description)");
            Console.WriteLine("7. Search (full-text: cast reviews)");
            Console.WriteLine("8. Search (full-text: synopsis)");
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
                        await SearchCastReviews(es);
                        break;
                    case "8":
                        await SearchSynopsis(es);
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
        var doc = new Theatre();
        Console.Write("Title: ");
        doc.Title = Console.ReadLine() ?? "";
        Console.Write("Author: ");
        doc.Author = Console.ReadLine() ?? "";
        Console.Write("Year premiere: ");
        doc.YearPremiere = int.TryParse(Console.ReadLine(), out int y) ? y : 0;
        Console.Write("Director: ");
        doc.Director = Console.ReadLine() ?? "";
        Console.Write("Main actor: ");
        doc.MainActor = Console.ReadLine() ?? "";
        Console.Write("Rating (1-10): ");
        doc.Rating = int.TryParse(Console.ReadLine(), out int r) ? r : 0;
        Console.Write("Website: ");
        doc.Website = Console.ReadLine() ?? "";
        Console.Write("Description (text): ");
        doc.Description = Console.ReadLine() ?? "";
        Console.Write("Cast reviews (text): ");
        doc.CastReviews = Console.ReadLine() ?? "";
        Console.Write("Synopsis (text): ");
        doc.Synopsis = Console.ReadLine() ?? "";
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
        Console.WriteLine("Field (Title, Author, Director, MainActor, Website): ");
        var field = Console.ReadLine() ?? "title";
        Console.Write("Value: ");
        var value = Console.ReadLine() ?? "";
        var query = $"{{ \"query\": {{ \"term\": {{ \"{field}\": \"{value}\" }} }} }}";
        var res = await es.SearchAsync(query);
        PrintList(res);
    }

    static async Task SearchRange(ElasticsearchService es)
    {
        Console.WriteLine("Field (yearPremiere, rating): ");
        var field = Console.ReadLine() ?? "yearPremiere";
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
        Console.WriteLine("Field (Title, Author, Director, MainActor, Website): ");
        var field = Console.ReadLine() ?? "title";
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

    static async Task SearchCastReviews(ElasticsearchService es)
    {
        Console.Write("Search in cast reviews (query): ");
        var query_text = Console.ReadLine() ?? "";
        var query = $"{{ \"query\": {{ \"match\": {{ \"castReviews\": {{ \"query\": \"{query_text}\" }} }} }} }}";
        var res = await es.SearchAsync(query);
        PrintList(res);
    }

    static async Task SearchSynopsis(ElasticsearchService es)
    {
        Console.Write("Search in synopsis (query): ");
        var query_text = Console.ReadLine() ?? "";
        var query = $"{{ \"query\": {{ \"match\": {{ \"synopsis\": {{ \"query\": \"{query_text}\" }} }} }} }}";
        var res = await es.SearchAsync(query);
        PrintList(res);
    }

    static void PrintList(List<Theatre> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("No results.");
            return;
        }
        foreach (var doc in list)
        {
            Console.WriteLine($"Id: {doc.Id}\n  Title: {doc.Title}\n  Author: {doc.Author}\n  Year premiere: {doc.YearPremiere}\n  Director: {doc.Director}\n  Main actor: {doc.MainActor}\n  Rating: {doc.Rating}\n  Website: {doc.Website}\n  Description: {doc.Description}\n  Cast Reviews: {doc.CastReviews}\n  Synopsis: {doc.Synopsis}\n");
        }
    }
}
