public class VectorSpaceModel
{
    private List<Document> documents;
    private HashSet<string> vocabulary;
    private Dictionary<string, int> documentFrequency;

    public VectorSpaceModel()
    {
        documents = new List<Document>();
        vocabulary = new HashSet<string>();
        documentFrequency = new Dictionary<string, int>();
    }

    public void AddDocument(Document doc)
    {
        documents.Add(doc);
        
        var uniqueTermsInDoc = new HashSet<string>(doc.Terms, StringComparer.OrdinalIgnoreCase);
        foreach (var term in uniqueTermsInDoc)
        {
            vocabulary.Add(term.ToLower());
            
            if (!documentFrequency.ContainsKey(term.ToLower()))
            {
                documentFrequency[term.ToLower()] = 0;
            }
            documentFrequency[term.ToLower()]++;
        }
    }

    public List<Document> GetDocuments() => documents;

    public double CalculateTF(string term, Document doc)
    {
        int frequency = doc.GetTermFrequency(term);
        int totalTermsInDoc = doc.Terms.Count;
        
        if (totalTermsInDoc == 0)
            return 0;
        
        return (double)frequency / totalTermsInDoc;
    }

    public double CalculateIDF(string term)
    {
        int N = documents.Count;
        int n_t = 0;

        if (documentFrequency.ContainsKey(term.ToLower()))
            n_t = documentFrequency[term.ToLower()];

        return 1 + Math.Log((double)N / (1 + n_t));
    }

    public Dictionary<string, double> CalculateTFIDFVector(Document doc)
    {
        var vector = new Dictionary<string, double>();
        
        foreach (var term in vocabulary)
        {
            double tf = CalculateTF(term, doc);
            double idf = CalculateIDF(term);
            vector[term] = tf * idf;
        }

        return vector;
    }

    public Dictionary<string, double> CalculateQueryVector(string query)
    {
        var terms = query.ToLower().Split([' ', '\t', '\n', '\r'], StringSplitOptions.RemoveEmptyEntries);
        var queryTermFreq = new Dictionary<string, int>();

        foreach (var term in terms)
        {
            if (!queryTermFreq.ContainsKey(term))
            {
                queryTermFreq[term] = 0;
            }
            queryTermFreq[term]++;
        }

        var vector = new Dictionary<string, double>();
        
        int totalTermsInQuery = queryTermFreq.Values.Sum();
        
        foreach (var term in vocabulary)
        {
            double tf;
            if (queryTermFreq.ContainsKey(term))
            {
                // Formula for variant 14: tf(t,q) = f_{t,q} / Σ f_{t',q}
                tf = totalTermsInQuery > 0 ? (double)queryTermFreq[term] / totalTermsInQuery : 0;
            }
            else
            {
                tf = 0;
            }

            double idf = CalculateIDF(term);
            vector[term] = tf * idf;
        }

        return vector;
    }

    public double CalculateCosineSimilarity(Dictionary<string, double> vector1, Dictionary<string, double> vector2)
    {
        double dotProduct = 0;
        double magnitude1 = 0;
        double magnitude2 = 0;

        foreach (var term in vocabulary)
        {
            double v1 = vector1.ContainsKey(term) ? vector1[term] : 0;
            double v2 = vector2.ContainsKey(term) ? vector2[term] : 0;

            dotProduct += v1 * v2;
            magnitude1 += v1 * v1;
            magnitude2 += v2 * v2;
        }

        magnitude1 = Math.Sqrt(magnitude1);
        magnitude2 = Math.Sqrt(magnitude2);

        if (magnitude1 == 0 || magnitude2 == 0)
        {
            return 0;
        }

        return dotProduct / (magnitude1 * magnitude2);
    }

    public List<(Document doc, double similarity)> SearchDocuments(string query, double threshold = 0.0)
    {
        if (!documents.Any())
        {
            return new List<(Document, double)>();
        }

        var queryVector = CalculateQueryVector(query);
        var results = new List<(Document, double)>();

        foreach (var doc in documents)
        {
            var docVector = CalculateTFIDFVector(doc);
            double similarity = CalculateCosineSimilarity(queryVector, docVector);

            if (similarity >= threshold)
            {
                results.Add((doc, similarity));
            }
        }

        results = results.OrderByDescending(r => r.Item2).ToList();

        return results;
    }
}
