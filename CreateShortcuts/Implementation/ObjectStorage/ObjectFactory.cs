using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.AbstractionLayer.IOServices.Implementations;
using DoenaSoft.CreateShortcuts.Implementation.IOServices;
using DoenaSoft.CreateShortcuts.Implementation.Processors;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.ObjectStorage;

public sealed class ObjectFactory : IObjectFactory
{
    public IWarningsProcessor CreateWarningsProcessor(IObjectStorage os)
        => new WarningsProcessor(os);

    public IArgumentsProcessor CreateArgumentsProessor(IObjectStorage os)
        => new ArgumentsProcessor(os);

    public IProcessor CreateShortcutFolderProcessor(IObjectStorage os)
        => new ShortcutFolderProcessor(os);

    public IProcessor CreateVideoFolderProcessor(IObjectStorage os)
        => new VideoFolderProcessor(os, this);

    public IArticleProcessor CreateArticleProcessor(string seriesName, bool articleIsPrefix, IObjectStorage os)
        => new ArticleProcessor(seriesName, articleIsPrefix, os);

    public IHelper CreateHelper()
        => new Helper();

    public IObjectStorage CreateObjectStorage(IProgram program, IEnumerable<string> args)
        => new ObjectStorage(program, args, this);

    public ITuple CreateTuple(string article, bool articleIsPrefix)
        => new Tuple(article, articleIsPrefix);

    public IShortcutCreator CreateShortcutCreator(IObjectStorage os)
        => new ShortcutCreator(os, this);

    public IIOServices CreateIOServices(IObjectStorage os)
        => new AbstractionLayer.IOServices.IOServices(os.Logger);

    public IShortcut CreateShortcut(string linkFileName
        , IObjectStorage os)
        => new Shortcut(linkFileName, os.Logger);

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