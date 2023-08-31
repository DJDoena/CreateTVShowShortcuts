using System;
using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Implementation;
using DoenaSoft.CreateShortcuts.Implementation.IOServices;
using DoenaSoft.CreateShortcuts.Implementation.Processors;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;
using DoenaSoft.CreateShortcuts.Tests.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.ObjectStorage
{
    internal sealed class TestObjectStorage : IObjectStorage
    {
        public TestObjectStorage(IProgram program
            , IEnumerable<String> arguments
            , FileSystemMock fileSystemMock
            , IEnumerable<SearchPatternMatch> searchPatternMatches
            , String logFileName
            , IObjectFactory of)
        {
            Program = program;
            Arguments = arguments;
            WarningsProcessor = of.CreateWarningsProcessor(this);
            ArgumentsProcessor = new ArgumentsProcessor(this);
            ShortcutFolderProcessor = new ShortcutFolderProcessor(this);
            VideoFolderProcessor = new VideoFolderProcessor(this);
            Helper = new Helper();
            ShortcutCreator = new ShortcutCreator(this, of);
            IOServices = new TestIOServices(fileSystemMock, searchPatternMatches, this);
            Logger = new DualLogger(logFileName);
        }

        public IEnumerable<String> Arguments { get; private set; }

        public IWarningsProcessor WarningsProcessor { get; private set; }

        public IArgumentsProcessor ArgumentsProcessor { get; private set; }

        public IProcessor ShortcutFolderProcessor { get; private set; }

        public IProcessor VideoFolderProcessor { get; private set; }

        public IArticleProcessor GetArticleProcessor(String seriesName
            , Boolean articleIsPrefix)
        {
            return (new ArticleProcessor(seriesName, articleIsPrefix, this));
        }

        public IHelper Helper { get; private set; }

        public ITuple GetTuple(String article
            , Boolean articleIsPrefix)
        {
            return (new Implementation.Tuple(article, articleIsPrefix));
        }

        public IShortcutCreator ShortcutCreator { get; private set; }

        public IProgram Program { get; private set; }

        public IIOServices IOServices { get; private set; }

        public ILogger Logger { get; private set; }

        public void Dispose()
        {
            Program = null;
            Arguments = null;
            WarningsProcessor = null;
            ArgumentsProcessor = null;
            ShortcutFolderProcessor = null;
            VideoFolderProcessor = null;
            Helper = null;
            ShortcutCreator = null;
            IOServices = null;

            Logger.Dispose();
            Logger = null;
        }
    }
}