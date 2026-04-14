using System.Collections.Generic;

namespace DoenaSoft.CreateShortcuts.Interfaces.Processors;

public interface IWarningsProcessor : IProcessor
{
    void AddWarnings(IEnumerable<string> warnings);
}