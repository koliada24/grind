class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Boolean Information Retrieval System ===");
        Console.WriteLine("Variant: Disjunctive Normal Form (ДНФ) - OR of ANDs\n");

        var terms = InputIndexedTerms();

        var documents = InputDocuments(terms);

        var index = new InvertedIndex();
        foreach (var doc in documents)
        {
            index.AddDocument(doc);
        }

        Console.WriteLine("\n=== Loaded Data ===");
        Console.WriteLine($"Terms: {string.Join(", ", terms.OrderBy(t => t))}");
        Console.WriteLine($"Documents ({documents.Count}):");
        foreach (var doc in documents)
        {
            Console.WriteLine($"  {doc.Name} - {{{string.Join(", ", doc.Terms)}}}");
        }

        index.DisplayIndex(terms);

        ExecuteQueries(index, documents, terms);
    }

    static HashSet<string> InputIndexedTerms()
    {
        Console.WriteLine("STAGE A: Input Indexed Terms");
        Console.WriteLine("Enter terms (one per line, empty line to finish):");
        
        var terms = new HashSet<string>();
        var counter = 1;
        while (true)
        {
            Console.Write($"Term{counter}: ");
            var input = Console.ReadLine()?.Trim().ToLower();
            if (string.IsNullOrEmpty(input))
            {
                if (terms.Count == 0)
                {
                    Console.WriteLine("At least one term should be provided.");
                    continue;
                }
                else
                {
                    break;
                }
            }
            terms.Add(input);
            counter++;
        }

        return terms;
    }

    static List<Document> InputDocuments(HashSet<string> validTerms)
    {
        Console.WriteLine("\nSTAGE B: Input Documents");
        Console.WriteLine("Enter documents (one per line in format: 'name: term1 term2 term3...')");
        Console.WriteLine("Empty line to finish:");
        
        var documents = new List<Document>();
        int docId = 1;

        while (true)
        {
            var docName = $"Doc{docId}";

            Console.Write(docName + ": ");
            var input = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(input))
            {
                if (documents.Count == 0)
                {
                    Console.WriteLine("At least one document should be provided.");
                    continue;
                }
                else
                {
                    break;
                }
            }

            var docTerms = new HashSet<string>(
                input.ToLower()
                    .Split([' ', '\n', '\r', '\t'], StringSplitOptions.RemoveEmptyEntries)
                    .Where(validTerms.Contains)
            );

            if (docTerms.Count > 0)
            {
                documents.Add(new Document(docId, docName, docTerms));
                docId++;
            }
        }

        return documents;
    }

    static void ExecuteQueries(InvertedIndex index, List<Document> documents, HashSet<string> validTerms)
    {
        Console.WriteLine("\nSTAGE C: Execute Queries");
        Console.WriteLine("Enter queries in ДНФ (Disjunctive Normal Form)");
        Console.WriteLine("Format: (term1 AND term2) OR (term3 AND NOT(term4)) OR ...");
        Console.WriteLine("Type 'exit' to quit.\n");

        var evaluator = new BooleanQueryEvaluator(index);
        var parser = new BooleanQueryParser();

        while (true)
        {
            Console.Write("Enter query: ");
            var query = Console.ReadLine()?.Trim();

            if (query == "exit")
                break;

            if (string.IsNullOrEmpty(query))
                continue;

            try
            {
                var clauses = parser.ParseDNF(query, validTerms);
                var results = evaluator.Evaluate(clauses);

                Console.WriteLine("\nQuery parsed as ДНФ:");
                for (int i = 0; i < clauses.Count; i++)
                {
                    var clause = clauses[i];
                    var literals = clause.Literals.Select(l => (l.IsNegated ? "NOT(" : "") + l.Term + (l.IsNegated ? ")" : ""));
                    Console.WriteLine($"  Clause {i + 1}: {string.Join(" AND ", literals)}");
                }

                Console.WriteLine($"\nResults ({results.Count} documents):");
                if (results.Count == 0)
                {
                    Console.WriteLine("  No matching documents");
                }
                else
                {
                    foreach (var docId in results.OrderBy(x => x))
                    {
                        var doc = documents.First(d => d.Id == docId);
                        Console.WriteLine($"  Doc {docId}: {doc.Name} - {{{string.Join(", ", doc.Terms)}}}");
                    }
                }
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}\n");
            }
        }

        Console.WriteLine("\nProgram ended. Thank you!");
    }
}
