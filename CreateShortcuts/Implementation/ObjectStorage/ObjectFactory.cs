using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.AbstractionLayer.IOServices.Implementations;
using DoenaSoft.CreateShortcuts.Implementation.IOServices;
using DoenaSoft.CreateShortcuts.Implementation.Processors;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.ObjectStorage
{
    public sealed class ObjectFactory : IObjectFactory
    {
        public IWarningsProcessor CreateWarningsProcessor(IObjectStorage os)
        {
            var processor = new WarningsProcessor(os);

            return processor;
        }

        public IArgumentsProcessor CreateArgumentsProessor(IObjectStorage os)
        {
            var processor = new ArgumentsProcessor(os);

            return processor;
        }

        public IProcessor CreateShortcutFolderProcessor(IObjectStorage os)
        {
            var processor = new ShortcutFolderProcessor(os);

            return processor;
        }

        public IProcessor CreateVideoFolderProcessor(IObjectStorage os)
        {
            var processor = new VideoFolderProcessor(os);

            return processor;
        }

        public IArticleProcessor CreateArticleProcessor(string seriesName, bool articleIsPrefix, IObjectStorage os)
        {
            var processor = new ArticleProcessor(seriesName, articleIsPrefix, os);

            return processor;
        }

        public IHelper CreateHelper()
        {
            var helper = new Helper();

            return helper;
        }

        public IObjectStorage CreateObjectStorage(IProgram program, IEnumerable<string> args)
        {
            var storage = new ObjectStorage(program, args, this);

            return storage;
        }

        public ITuple CreateTuple(string article, bool articleIsPrefix)
        {
            var tuple = new Tuple(article, articleIsPrefix);

            return tuple;
        }

        public IShortcutCreator CreateShortcutCreator(IObjectStorage os)
        {
            var shortcutCreator = new ShortcutCreator(os, this);

            return shortcutCreator;
        }

        public IIOServices CreateIOServices(IObjectStorage os)
            => new AbstractionLayer.IOServices.IOServices(os.Logger);

        public IShortcut CreateShortcut(string linkFileName
            , IObjectStorage os)
        {
            var shortcut = new Shortcut(linkFileName, os.Logger);

            return shortcut;
        }

        public ILogger CreateLogger(IObjectStorage os)
        {
            var argumentsProcessor = os.ArgumentsProcessor;

            var logFile = argumentsProcessor.LogFile;

            ILogger logger;
            if (argumentsProcessor.DualLog)
            {
                logger = new DualLogger(logFile);
            }
            else
            {
                logger = new ConsoleLogger();

                if (string.IsNullOrEmpty(logFile) == false)
                {
                    logger = new FileLogger(logFile, logger);
                }
            }

            return logger;
        }
    }
}