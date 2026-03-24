class BooleanQueryEvaluator
{
    private InvertedIndex index;

    public BooleanQueryEvaluator(InvertedIndex index)
    {
        this.index = index;
    }

    public HashSet<int> Evaluate(List<BooleanQueryParser.Clause> clauses)
    {
        if (clauses.Count == 0)
            return new HashSet<int>();

        var result = EvaluateClause(clauses[0]);

        for (int i = 1; i < clauses.Count; i++)
        {
            var clauseResult = EvaluateClause(clauses[i]);
            result.UnionWith(clauseResult);
        }

        return result;
    }

    private HashSet<int> EvaluateClause(BooleanQueryParser.Clause clause)
    {
        if (clause.Literals.Count == 0)
            return new HashSet<int>();

        var result = EvaluateLiteral(clause.Literals[0]);

        for (int i = 1; i < clause.Literals.Count; i++)
        {
            var litResult = EvaluateLiteral(clause.Literals[i]);
            result.IntersectWith(litResult);
        }

        return result;
    }

    private HashSet<int> EvaluateLiteral(BooleanQueryParser.Literal literal)
    {
        var docs = index.GetDocumentsForTerm(literal.Term);

        if (literal.IsNegated)
        {
            var complement = index.GetAllDocumentIds();
            complement.ExceptWith(docs);
            return complement;
        }

        return docs;
    }
}
