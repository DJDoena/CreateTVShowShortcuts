using System;
using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces;
using DoenaSoft.CreateShortcuts.Interfaces.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;
using DoenaSoft.CreateShortcuts.Tests.IOServices;

namespace DoenaSoft.CreateShortcuts.Tests.ObjectStorage;

internal sealed class TestObjectFactory : IObjectFactory
{
    private readonly IWarningsProcessor WarningsProcessor;

    private readonly FileSystemMock FileSystemMock;

    private readonly IEnumerable<SearchPatternMatch> SearchPatternMatches;

    public TestObjectFactory(IWarningsProcessor warningsProcessor
        , IEnumerable<SearchPatternMatch> searchPatternMatches)
    {
        WarningsProcessor = warningsProcessor;
        FileSystemMock = new FileSystemMock();
        SearchPatternMatches = searchPatternMatches;
    }

    public IWarningsProcessor CreateWarningsProcessor(IObjectStorage os)
    {
        return WarningsProcessor;
    }

    public IArgumentsProcessor CreateArgumentsProessor(IObjectStorage os)
    {
        throw new NotImplementedException();
    }

    public IProcessor CreateShortcutFolderProcessor(IObjectStorage os)
    {
        throw new NotImplementedException();
    }

    public IProcessor CreateVideoFolderProcessor(IObjectStorage os)
    {
        throw new NotImplementedException();
    }

    public IArticleProcessor CreateArticleProcessor(string seriesName
        , bool articleIsPrefix
        , IObjectStorage os)
    {
        throw new NotImplementedException();
    }

    public IHelper CreateHelper()
    {
        throw new NotImplementedException();
    }

    public IObjectStorage CreateObjectStorage(IProgram program
        , IEnumerable<string> arguments)
    {
        return new TestObjectStorage(program, arguments, FileSystemMock, SearchPatternMatches, this);
    }

    public ITuple CreateTuple(string article
        , bool articleIsPrefix)
    {
        throw new NotImplementedException();
    }

    public IShortcutCreator CreateShortcutCreator(IObjectStorage os)
    {
        throw new NotImplementedException();
    }

    public IIOServices CreateIOServices(IObjectStorage os)
    {
        throw new NotImplementedException();
    }

    public IShortcut CreateShortcut(string linkFileName
        , IObjectStorage os)
    {
        return new TestShortcut(linkFileName, FileSystemMock, os);
    }

    public ILogger CreateLogger(IObjectStorage os)
    {
        throw new NotImplementedException();
    }
}