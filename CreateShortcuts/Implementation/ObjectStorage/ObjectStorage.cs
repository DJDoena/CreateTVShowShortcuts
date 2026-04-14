using System.Collections.Generic;
using System.Diagnostics;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.ObjectStorage;

internal sealed class ObjectStorage : IObjectStorage
{
    private readonly IProgram _program;

    private readonly IObjectFactory _objectFactory;

    private IWarningsProcessor _warningsProcessor;

    private IArgumentsProcessor _argumentsProcessor;

    private IProcessor _shortcutFolderProcessor;

    private IProcessor _videoFolderProcessor;

    private IHelper _helper;

    private IShortcutCreator _shortcutCreator;

    private IIOServices _ioServices;

    private ILogger _logger;

    public IEnumerable<string> Arguments { get; private set; }

    private IArticleProcessorStorage ArticleProcessorStorage { get; set; }

    private ITupleStorage TupleStorage { get; set; }


    public ObjectStorage(IProgram program, IEnumerable<string> arguments, IObjectFactory of)
    {
        _program = program;
        this.Arguments = arguments;
        _objectFactory = of;
    }

    public IWarningsProcessor WarningsProcessor
    {
        get
        {
            if (_warningsProcessor == null)
            {
                _warningsProcessor = _objectFactory.CreateWarningsProcessor(this);
            }

            return _warningsProcessor;
        }
    }

    public IArgumentsProcessor ArgumentsProcessor
    {
        get
        {
            if (_argumentsProcessor == null)
            {
                _argumentsProcessor = _objectFactory.CreateArgumentsProessor(this);
            }

            return _argumentsProcessor;
        }
    }

    public IProcessor ShortcutFolderProcessor
    {
        get
        {
            if (_shortcutFolderProcessor == null)
            {
                _shortcutFolderProcessor = _objectFactory.CreateShortcutFolderProcessor(this);
            }

            return _shortcutFolderProcessor;
        }
    }

    public IProcessor VideoFolderProcessor
    {
        get
        {
            if (_videoFolderProcessor == null)
            {
                _videoFolderProcessor = _objectFactory.CreateVideoFolderProcessor(this);
            }

            return _videoFolderProcessor;
        }
    }

    public IArticleProcessor GetArticleProcessor(string seriesName, bool articleIsPrefix)
    {
        if (this.ArticleProcessorStorage == null)
        {
            this.ArticleProcessorStorage = new ArticleProcessorStorage(this, _objectFactory);
        }

        var articleProcessor = this.ArticleProcessorStorage.GetArticleProcessor(seriesName, articleIsPrefix);

        return articleProcessor;
    }

    public IHelper Helper
    {
        get
        {
            if (_helper == null)
            {
                _helper = _objectFactory.CreateHelper();
            }

            return _helper;
        }
    }

    public ITuple GetTuple(string article, bool articleIsPrefix)
    {
        if (this.TupleStorage == null)
        {
            this.TupleStorage = new TupleStorage(this, _objectFactory);
        }

        var tuple = this.TupleStorage.GetTuple(article, articleIsPrefix);

        return tuple;
    }

    public IShortcutCreator ShortcutCreator
    {
        get
        {
            if (_shortcutCreator == null)
            {
                _shortcutCreator = _objectFactory.CreateShortcutCreator(this);
            }

            return _shortcutCreator;
        }
    }

    IProgram IObjectStorage.Program
    {
        [DebuggerStepThrough]
        get
        {
            return _program;
        }
    }

    public IIOServices IOServices
    {
        get
        {
            if (_ioServices == null)
            {
                _ioServices = _objectFactory.CreateIOServices(this);
            }

            return _ioServices;
        }
    }

    public ILogger Logger
    {
        get
        {
            if (_logger == null)
            {
                _logger = _objectFactory.CreateLogger(this);
            }

            return _logger;
        }
    }

    public void Dispose()
    {
        this.Arguments = null;
        _warningsProcessor = null;
        _argumentsProcessor = null;
        _shortcutFolderProcessor = null;
        _videoFolderProcessor = null;
        _helper = null;
        this.ArticleProcessorStorage = null;
        this.TupleStorage = null;
        _shortcutCreator = null;
        _ioServices = null;

        if (_logger != null)
        {
            _logger.Dispose();
            _logger = null;
        }
    }
}