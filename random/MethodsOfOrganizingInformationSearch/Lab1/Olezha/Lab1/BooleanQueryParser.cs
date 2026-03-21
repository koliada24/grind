using System.Text.RegularExpressions;

class BooleanQueryParser
{
    public class Clause
    {
        public List<Literal> Literals { get; set; }
        public Clause()
        {
            Literals = new List<Literal>();
        }
    }

    public class Literal
    {
        public string Term { get; set; }
        public bool IsNegated { get; set; }

        public Literal(string term, bool isNegated = false)
        {
            Term = term;
            IsNegated = isNegated;
        }
    }

    public List<Clause> ParseKNF(string query, HashSet<string> validTerms)
    {
        query = query.Trim();
        
        query = ApplyDeMorgansLaw(query);

        var clauses = new List<Clause>();

        var andParts = SplitByTopLevelOperator(query, "AND");

        foreach (var part in andParts)
        {
            var clause = new Clause();
            var trimmedPart = part.Trim();

            if (trimmedPart.StartsWith("(") && trimmedPart.EndsWith(")"))
            {
                trimmedPart = trimmedPart.Substring(1, trimmedPart.Length - 2).Trim();
            }

            var orParts = SplitByTopLevelOperator(trimmedPart, "OR");

            foreach (var literal in orParts)
            {
                var lit = literal.Trim();
                bool isNegated = false;

                if (lit.StartsWith("NOT(") && lit.EndsWith(")"))
                {
                    isNegated = true;
                    lit = lit.Substring(4, lit.Length - 5).Trim();
                }
                else if (lit.StartsWith("NOT "))
                {
                    isNegated = true;
                    lit = lit.Substring(4).Trim();
                }

                if (validTerms.Contains(lit))
                {
                    clause.Literals.Add(new Literal(lit, isNegated));
                }
                else
                {
                    throw new Exception($"Invalid term: {lit}");
                }
            }

            if (clause.Literals.Count > 0)
            {
                clauses.Add(clause);
            }
        }

        if (clauses.Count == 0)
        {
            throw new Exception("Invalid query format");
        }

        return clauses;
    }

    private string ApplyDeMorgansLaw(string query)
    {
        var pattern = @"NOT\s*\(([^()]*)\)";
        
        while (Regex.IsMatch(query, pattern))
        {
            query = Regex.Replace(query, pattern, match =>
            {
                var innerExpression = match.Groups[1].Value.Trim();
                
                if (innerExpression.Contains(" OR "))
                {
                    var orParts = SplitByTopLevelOperator(innerExpression, "OR");
                    var transformed = string.Join(" AND ", orParts.Select(p => $"NOT {p.Trim()}"));
                    return transformed;
                }
                else if (innerExpression.Contains(" AND "))
                {
                    return $"NOT ({innerExpression})";
                }
                else if (innerExpression.Contains(" OR "))
                {
                    return $"NOT {innerExpression}";
                }

                throw new InvalidOperationException("Unknown operator");
            });
        }
        
        return query;
    }

    private List<string> SplitByTopLevelOperator(string query, string op)
    {
        var result = new List<string>();

        var opPattern = $@"\b{op}\b";
        var matches = Regex.Matches(query, opPattern);

        if (matches.Count == 0)
        {
            return new List<string> { query };
        }

        int lastIndex = 0;
        foreach (Match match in matches)
        {
            var part = query.Substring(lastIndex, match.Index - lastIndex);
            if (IsValidSplit(part))
            {
                result.Add(part);
                lastIndex = match.Index + match.Length;
            }
        }

        result.Add(query.Substring(lastIndex));
        return result;
    }

    private bool IsValidSplit(string part)
    {
        int depth = 0;
        foreach (var ch in part)
        {
            if (ch == '(') depth++;
            if (ch == ')') depth--;
        }
        return depth == 0;
    }
}
