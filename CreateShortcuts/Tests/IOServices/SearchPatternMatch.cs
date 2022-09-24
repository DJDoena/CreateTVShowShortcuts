using System;
using System.Diagnostics;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    [DebuggerDisplay("Pattern={Pattern}, Text={Text}, Position={Position}")]
    internal sealed partial class SearchPatternMatch
    {
        public String Pattern { get; private set; }

        public String Text { get; private set; }

        public StringPosition Position { get; private set; }

        public SearchPatternMatch(String pattern
            , String text
            , StringPosition position)
        {
            Pattern = pattern;
            Text = text;
            Position = position;
        }
    }
}