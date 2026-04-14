using System.Collections.Generic;
using System.Linq;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.Processors;

internal sealed class WarningsProcessor : IWarningsProcessor
{
    private readonly List<IEnumerable<string>> _warnings;

    private readonly IObjectStorage _objectStorage;

    public WarningsProcessor(IObjectStorage os)
    {
        _objectStorage = os;
        _warnings = new List<IEnumerable<string>>(2);
    }

    public void AddWarnings(IEnumerable<string> warnings)
    {
        _warnings.Add(warnings);
    }

    public void Process()
    {
        if (_warnings.Count > 0)
        {
            List<string> warnings;

            warnings = _warnings.SelectMany(item => item).ToList();

            if (warnings.Count > 0)
            {
                var logger = _objectStorage.Logger;

                foreach (var warning in warnings)
                {
                    logger.WriteLineForInput(warning);
                }

                logger.WriteLineForInput("Press <Enter> to continue.");
                logger.ReadLine();
            }

            _warnings.Clear();
        }
    }
}