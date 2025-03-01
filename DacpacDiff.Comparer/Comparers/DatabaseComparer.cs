﻿using DacpacDiff.Core.Diff;
using DacpacDiff.Core.Model;
using DacpacDiff.Core.Utility;

namespace DacpacDiff.Comparer.Comparers;

public class DatabaseComparer : IModelComparer<DatabaseModel>
{
    private readonly IModelComparerFactory _comparerFactory;

    public DatabaseComparer(IModelComparerFactory comparerFactory)
    {
        _comparerFactory = comparerFactory;
    }

    public IEnumerable<IDifference> Compare(DatabaseModel? lft, DatabaseModel? rgt)
    {
        // TODO: others

        // Schemas
        var keys = (lft?.Schemas.Keys ?? Array.Empty<string>())
            .Union(rgt?.Schemas.Keys ?? Array.Empty<string>())
            .Distinct();
        var schCompr = _comparerFactory.GetComparer<SchemaModel>();
        var diffs1 = keys.SelectMany(k =>
            schCompr.Compare(lft?.Schemas.Get(k), rgt?.Schemas.Get(k))
        );

        return diffs1.ToArray();
    }
}
