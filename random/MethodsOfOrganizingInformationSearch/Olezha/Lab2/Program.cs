class Program
{
    static void Main(string[] args)
    {
        var model = new VectorSpaceModel();

        Console.WriteLine("=== Vector-Space Model Information Retrieval System ===");
        Console.WriteLine("Variant 11: TF = log(1 + f_{t,d}), IDF = 1 + log(N / (1 + n_t))");
        Console.WriteLine();

        InputDocumentCollection(model);

        ExecuteQueries(model);
    }

    static void InputDocumentCollection(VectorSpaceModel model)
    {
        Console.WriteLine("--- Stage 1: Document Collection Input ---");
        ManualDocumentInput(model);

        Console.WriteLine($"\nLoaded {model.GetDocuments().Count} document(s).\n");

        if (model.GetDocuments().Count == 0)
        {
            Console.WriteLine("Error: At least one document is required!");
            Environment.Exit(1);
        }
    }

    static void ManualDocumentInput(VectorSpaceModel model)
    {
        int docCount = 0;

        while (true)
        {
            docCount++;
            Console.WriteLine($"\n--- Document #{docCount} ---");
            Console.Write("Enter document content (terms separated by spaces, lowercase only, or empty to stop): ");
            string content = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(content))
            {
                docCount--;
                break;
            }

            string title = $"Document {docCount}";
            var doc = new Document(docCount, title, content);
            model.AddDocument(doc);
        }
    }


    static void ExecuteQueries(VectorSpaceModel model)
    {
        Console.WriteLine("--- Stage 2: Query Execution ---");
        Console.WriteLine("Enter search queries (type 'exit' to quit)");
        Console.WriteLine();

        double threshold = 0.0;
        Console.Write("Enter similarity threshold [0,0-1,0] (default 0,0): ");
        string thresholdInput = Console.ReadLine() ?? "0,0";
        if (double.TryParse(thresholdInput, out double t) && t >= 0.0 && t <= 1.0)
        {
            threshold = t;
        }

        Console.WriteLine();

        while (true)
        {
            Console.Write("Enter query: ");
            string query = Console.ReadLine() ?? "";

            if (query.ToLower() == "exit")
            {
                break;
            }

            if (string.IsNullOrWhiteSpace(query))
            {
                Console.WriteLine("Query cannot be empty.\n");
                continue;
            }

            var results = model.SearchDocuments(query, threshold);

            Console.WriteLine($"\nResults for query: \"{query}\"");
            if (results.Count == 0)
            {
                Console.WriteLine("No matching documents found.\n");
            }
            else
            {
                Console.WriteLine($"Found {results.Count} document(s):\n");
                for (int i = 0; i < results.Count; i++)
                {
                    var (doc, similarity) = results[i];
                    Console.WriteLine($"{i + 1}. Title: {doc.Title}");
                    Console.WriteLine($"   Similarity: {similarity:F4}");
                    Console.WriteLine($"   Content preview: {(doc.Content.Length > 60 ? doc.Content.Substring(0, 60) + "..." : doc.Content)}");
                    Console.WriteLine();
                }
            }
        }

        Console.WriteLine("\nThank you for using Vector-Space Model IR System!");
    }
}
