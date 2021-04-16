﻿using DacpacDiff.Core.Model;
using System;

namespace DacpacDiff.Core.Diff
{
    public class DiffRefCreate : IDifference
    {
        public FieldRefModel Ref { get; }
        public IModel Model => Ref;

        public string Title => "Create reference";
        public string Name => $"{Ref.Field.FullName}:[{(Ref.IsSystemNamed ? "*" : Ref.Name)}]";

        public DiffRefCreate(FieldRefModel @ref)
        {
            Ref = @ref ?? throw new ArgumentNullException(nameof(@ref));
        }
    }
}
