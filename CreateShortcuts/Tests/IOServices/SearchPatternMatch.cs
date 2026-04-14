using System.Diagnostics;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices;

[DebuggerDisplay("Pattern={Pattern}, Text={Text}, Position={Position}")]
internal sealed partial class SearchPatternMatch
{
    public string Pattern { get; private set; }

    public string Text { get; private set; }

    public StringPosition Position { get; private set; }

    public SearchPatternMatch(string pattern
        , string text
        , StringPosition position)
    {
        this.Pattern = pattern;
        this.Text = text;
        this.Position = position;
    }
}