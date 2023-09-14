using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Implementation;
using DoenaSoft.CreateShortcuts.Implementation.IOServices;
using DoenaSoft.CreateShortcuts.Implementation.Processors;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;
using DoenaSoft.CreateShortcuts.Logger;
using DoenaSoft.CreateShortcuts.Tests.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.ObjectStorage
{
    internal sealed class TestObjectStorage : IObjectStorage
    {
        public IEnumerable<string> Arguments { get; private set; }

        public IWarningsProcessor WarningsProcessor { get; private set; }

        public IArgumentsProcessor ArgumentsProcessor { get; private set; }

        public IProcessor ShortcutFolderProcessor { get; private set; }

        public IProcessor VideoFolderProcessor { get; private set; }

        public IShortcutCreator ShortcutCreator { get; private set; }

        public IProgram Program { get; private set; }

        public IIOServices IOServices { get; private set; }

        public ILogger Logger { get; private set; }

        public TestObjectStorage(IProgram program
            , IEnumerable<string> arguments
            , FileSystemMock fileSystemMock
            , IEnumerable<SearchPatternMatch> searchPatternMatches
            , IObjectFactory of)
        {
            this.Program = program;
            this.Arguments = arguments;
            this.WarningsProcessor = of.CreateWarningsProcessor(this);
            this.ArgumentsProcessor = new ArgumentsProcessor(this);
            this.ShortcutFolderProcessor = new ShortcutFolderProcessor(this);
            this.VideoFolderProcessor = new VideoFolderProcessor(this);
            this.Helper = new Helper();
            this.ShortcutCreator = new ShortcutCreator(this, of);
            this.IOServices = new TestIOServices(fileSystemMock, searchPatternMatches, this);
            this.Logger = new DebugLogger();
        }

        public IArticleProcessor GetArticleProcessor(string seriesName
            , bool articleIsPrefix)
        {
            return new ArticleProcessor(seriesName, articleIsPrefix, this);
        }

        public IHelper Helper { get; private set; }

        public ITuple GetTuple(string article
            , bool articleIsPrefix)
        {
            return new Tuple(article, articleIsPrefix);
        }

        public void Dispose()
        {
            this.Program = null;
            this.Arguments = null;
            this.WarningsProcessor = null;
            this.ArgumentsProcessor = null;
            this.ShortcutFolderProcessor = null;
            this.VideoFolderProcessor = null;
            this.Helper = null;
            this.ShortcutCreator = null;
            this.IOServices = null;

            this.Logger.Dispose();
            this.Logger = null;
        }
    }
}