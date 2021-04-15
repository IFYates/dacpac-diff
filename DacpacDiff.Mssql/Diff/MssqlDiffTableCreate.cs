﻿using DacpacDiff.Core.Diff;
using DacpacDiff.Core.Output;
using System;
using System.Linq;

namespace DacpacDiff.Mssql.Diff
{
    public class MssqlDiffTableCreate : BaseMssqlDiffBlock<DiffTableCreate>
    {
        public MssqlDiffTableCreate(DiffTableCreate diff)
            : base(diff)
        { }

        protected override void GetFormat(ISqlFileBuilder sb)
        {
            sb.AppendLine($"CREATE TABLE {_diff.Table.FullName}")
                .Append("(");

            var first = true;
            foreach (var fld in _diff.Table.Fields.OrderBy(f => f.Order))
            {
                sb.AppendIf(",", !first)
                    .AppendLine()
                    .Append("    ").Append(fld.GetTableFieldSql());
                first = false;
            }

            if (_diff.Table.PrimaryKey.Length > 0)
            {
                sb.AppendLine(",")
                    .Append($"    PRIMARY KEY {(_diff.Table.IsPrimaryKeyUnclustered ? "NONCLUSTERED " : "")}([{String.Join("], [", _diff.Table.PrimaryKey)}])");
            }

            if (_diff.Table.Temporality != null)
            {
                sb.AppendLine(",")
                    .AppendLine($"    PERIOD FOR SYSTEM_TIME ([{_diff.Table.Temporality.PeriodFieldFrom}], [{_diff.Table.Temporality.PeriodFieldTo}])")
                    .Append(") WITH (SYSTEM_VERSIONING = ON")
                    .AppendIf($" (HISTORY_TABLE = {_diff.Table.Temporality.HistoryTable})", (_diff.Table.Temporality.HistoryTable?.Length ?? 0) > 0)
                    .Append(')');
            }
            else
            {
                sb.AppendLine()
                    .Append(')');
            }

            // TODO: refs?
        }
    }
}
