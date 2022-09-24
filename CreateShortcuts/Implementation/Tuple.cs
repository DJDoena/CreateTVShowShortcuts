using System.Diagnostics;
using DoenaSoft.CreateShortcuts.Interfaces;

namespace DoenaSoft.CreateShortcuts.Implementation
{
    [DebuggerDisplay("Source={Source}, Target={Target})")]
    internal sealed class Tuple : ITuple
    {
        public string Source { get; private set; }

        public string Target { get; private set; }

        public Tuple(string article, bool articleIsPrefix)
        {
            Source = article + " ";
            Target = ", " + article;

            if (articleIsPrefix == false)
            {
                var temp = Source;
                Source = Target;
                Target = temp;
            }
        }
    }
}