using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Tests.Processors;

internal sealed class TestWarningsProcessor : IWarningsProcessor
{
    private readonly List<IEnumerable<string>> _warnings;

    private readonly IEnumerable<string> _expectedWarnings;

    public TestWarningsProcessor(IEnumerable<string> expectedWarnings)
    {
        _expectedWarnings = expectedWarnings;
        _warnings = new List<IEnumerable<string>>(2);
    }

    public void AddWarnings(IEnumerable<string> warnings)
    {
        _warnings.Add(warnings);
    }

    public void Process()
    {
        Debug.Assert(_warnings.Count > 0);

        var warnings = _warnings.SelectMany(item => item).ToList();

        var expectedWarnings = _expectedWarnings.ToList();

        Debug.Assert(warnings.Count == expectedWarnings.Count
            , string.Format("Count is wrong. Expected: {0}, Actual: {1}", expectedWarnings.Count, warnings.Count));

        for (var i = 0; i < warnings.Count; i++)
        {
            Debug.Assert(warnings[i] == expectedWarnings[i]
                , string.Format("Warning is wrong. Expected: {0}, Actual: {1}", expectedWarnings[i], warnings[i]));
        }
    }
}