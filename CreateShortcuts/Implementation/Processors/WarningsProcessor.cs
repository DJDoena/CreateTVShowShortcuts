using System.Collections.Generic;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.Processors
{
    internal sealed class WarningsProcessor : IWarningsProcessor
    {
        private List<IEnumerable<string>> Warnings;

        private readonly IObjectStorage ObjectStorage;

        public WarningsProcessor(IObjectStorage os)
        {
            ObjectStorage = os;
            Warnings = new List<IEnumerable<string>>(2);
        }

        public void AddWarnings(IEnumerable<string> warnings)
        {
            Warnings.Add(warnings);
        }

        public void Process()
        {
            if (Warnings.Count > 0)
            {
                List<string> warnings;

                warnings = Warnings.SelectMany(item => item).ToList();

                if (warnings.Count > 0)
                {
                    ILogger logger = ObjectStorage.Logger;

                    foreach (var warning in warnings)
                    {
                        logger.WriteLineForInput(warning);
                    }

                    logger.WriteLineForInput("Press <Enter> to continue.");
                    logger.ReadLine();
                }

                Warnings.Clear();
            }
        }
    }
}