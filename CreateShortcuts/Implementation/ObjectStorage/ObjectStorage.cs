using System.Collections.Generic;
using System.Diagnostics;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.ObjectStorage
{
    internal sealed class ObjectStorage : IObjectStorage
    {
        private readonly IProgram Program;

        private readonly IObjectFactory ObjectFactory;

        public IEnumerable<string> Arguments { get; private set; }

        private IWarningsProcessor m_WarningsProcessor;

        private IArgumentsProcessor m_ArgumentsProcessor;

        private IProcessor m_ShortcutFolderProcessor;

        private IProcessor m_VideoFolderProcessor;

        private IHelper m_Helper;

        private IArticleProcessorStorage ArticleProcessorStorage { get; set; }

        private ITupleStorage TupleStorage { get; set; }

        private IShortcutCreator m_ShortcutCreator;

        private IIOServices m_IOServices;

        private ILogger m_Logger;

        public ObjectStorage(IProgram program, IEnumerable<string> arguments, IObjectFactory of)
        {
            Program = program;
            Arguments = arguments;
            ObjectFactory = of;
        }

        public IWarningsProcessor WarningsProcessor
        {
            get
            {
                if (m_WarningsProcessor == null)
                {
                    m_WarningsProcessor = ObjectFactory.CreateWarningsProcessor(this);
                }

                return m_WarningsProcessor;
            }
        }

        public IArgumentsProcessor ArgumentsProcessor
        {
            get
            {
                if (m_ArgumentsProcessor == null)
                {
                    m_ArgumentsProcessor = ObjectFactory.CreateArgumentsProessor(this);
                }

                return m_ArgumentsProcessor;
            }
        }

        public IProcessor ShortcutFolderProcessor
        {
            get
            {
                if (m_ShortcutFolderProcessor == null)
                {
                    m_ShortcutFolderProcessor = ObjectFactory.CreateShortcutFolderProcessor(this);
                }

                return m_ShortcutFolderProcessor;
            }
        }

        public IProcessor VideoFolderProcessor
        {
            get
            {
                if (m_VideoFolderProcessor == null)
                {
                    m_VideoFolderProcessor = ObjectFactory.CreateVideoFolderProcessor(this);
                }

                return m_VideoFolderProcessor;
            }
        }

        public IArticleProcessor GetArticleProcessor(string seriesName, bool articleIsPrefix)
        {
            if (ArticleProcessorStorage == null)
            {
                ArticleProcessorStorage = new ArticleProcessorStorage(this, ObjectFactory);
            }

            var articleProcessor = ArticleProcessorStorage.GetArticleProcessor(seriesName, articleIsPrefix);

            return articleProcessor;
        }

        public IHelper Helper
        {
            get
            {
                if (m_Helper == null)
                {
                    m_Helper = ObjectFactory.CreateHelper();
                }

                return m_Helper;
            }
        }

        public ITuple GetTuple(string article, bool articleIsPrefix)
        {
            if (TupleStorage == null)
            {
                TupleStorage = new TupleStorage(this, ObjectFactory);
            }

            var tuple = TupleStorage.GetTuple(article, articleIsPrefix);

            return tuple;
        }

        public IShortcutCreator ShortcutCreator
        {
            get
            {
                if (m_ShortcutCreator == null)
                {
                    m_ShortcutCreator = ObjectFactory.CreateShortcutCreator(this);
                }

                return m_ShortcutCreator;
            }
        }

        IProgram IObjectStorage.Program
        {
            [DebuggerStepThrough]
            get
            {
                return Program;
            }
        }

        public IIOServices IOServices
        {
            get
            {
                if (m_IOServices == null)
                {
                    m_IOServices = ObjectFactory.CreateIOServices(this);
                }

                return m_IOServices;
            }
        }

        public ILogger Logger
        {
            get
            {
                if (m_Logger == null)
                {
                    m_Logger = ObjectFactory.CreateLogger(this);
                }

                return m_Logger;
            }
        }

        public void Dispose()
        {
            Arguments = null;
            m_WarningsProcessor = null;
            m_ArgumentsProcessor = null;
            m_ShortcutFolderProcessor = null;
            m_VideoFolderProcessor = null;
            m_Helper = null;
            ArticleProcessorStorage = null;
            TupleStorage = null;
            m_ShortcutCreator = null;
            m_IOServices = null;

            if (m_Logger != null)
            {
                m_Logger.Dispose();
                m_Logger = null;
            }
        }
    }
}