﻿using CommandLine;
using DacpacDiff.Core.Output;
using DacpacDiff.Core.Parser;

namespace DacpacDiff.Comparer
{
    public class Options : IOutputOptions, IParserOptions
    {
        [Value(index: 0, MetaName = "Start schema", Required = true, HelpText = "The path of the dacpac file for the current scheme.")]
        public string? StartSchemeFile { get; init; }

        [Value(index: 1, MetaName = "Target schema", Required = true, HelpText = "The path of the dacpac file for the desired scheme.")]
        public string? TargetSchemeFile { get; init; }

        [Option(shortName: 'o', longName: "output", HelpText = "The file to write the result to.")]
        public string? OutputFile { get; init; }

        public bool PrettyPrint { get; init; }
        public bool DisableDatalossCheck { get; init; }
    }
}
