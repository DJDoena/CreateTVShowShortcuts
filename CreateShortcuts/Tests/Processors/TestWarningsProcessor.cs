using DoenaSoft.CreateShortcuts.Interfaces.Processors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DoenaSoft.CreateShortcuts.Tests.Processors
{
    internal sealed class TestWarningsProcessor : IWarningsProcessor
    {
        private List<IEnumerable<String>> Warnings;

        private IEnumerable<String> ExpectedWarnings;

        public TestWarningsProcessor(IEnumerable<String> expectedWarnings)
        {
            ExpectedWarnings = expectedWarnings;
            Warnings = new List<IEnumerable<String>>(2);
        }

        public void AddWarnings(IEnumerable<String> warnings)
        {
            Warnings.Add(warnings);
        }

        public void Process()
        {
            List<String> warnings;
            List<String> expectedWarnings;

            Debug.Assert(Warnings.Count > 0);

            warnings = Warnings.SelectMany(item => item).ToList();

            expectedWarnings = ExpectedWarnings.ToList();

            Debug.Assert(warnings.Count == expectedWarnings.Count
                , String.Format("Count is wrong. Expected: {0}, Actual: {1}", expectedWarnings.Count, warnings.Count));

            for (Int32 i = 0; i < warnings.Count; i++)
            {
                Debug.Assert(warnings[i] == expectedWarnings[i]
                    , String.Format("Warning is wrong. Expected: {0}, Actual: {1}", expectedWarnings[i], warnings[i]));
            }
        }
    }
}