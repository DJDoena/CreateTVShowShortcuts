using System;
using System.Collections.Generic;

namespace DoenaSoft.CreateShortcuts.Interfaces.Processors
{
    public interface IWarningsProcessor : IProcessor
    {
        void AddWarnings(IEnumerable<String> warnings);
    }
}