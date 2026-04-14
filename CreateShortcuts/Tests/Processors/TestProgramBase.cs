using System.Collections.Generic;
using System.Diagnostics;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;
using DoenaSoft.CreateShortcuts.Tests.IOServices;
using DoenaSoft.CreateShortcuts.Tests.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.Processors;

internal abstract class TestProgramBase : IProgram
{
    protected readonly IObjectStorage _objectStorage;

    protected TestProgramBase()
    {
        var of = this.ObjectFactory;

        _objectStorage = of.CreateObjectStorage(this, this.Arguments);
    }

    public virtual string RootFolderForShortcutFiles
        => Defaults.RootFolderForShortcutFiles;

    public virtual string SeasonFolderPattern
        => Defaults.SeasonFolderPattern;

    public virtual string StaffelFolderPattern
        => Defaults.StaffelFolderPattern;

    public virtual string SeriesNamePattern
        => Defaults.SeriesNamePattern;

    public virtual string ShortcutExtension
        => Defaults.ShortcutExtension;

    public virtual IEnumerable<string> VideoFileFolders
        => Defaults.VideoFileFolders;

    protected virtual IObjectFactory ObjectFactory
        => new TestObjectFactory(new TestWarningsProcessor(this.ExpectedWarnings), this.SearchPatternMatches);

    protected virtual IEnumerable<SearchPatternMatch> SearchPatternMatches
    {
        get
        {
            yield return new SearchPatternMatch(this.SeriesNamePattern, string.Empty, SearchPatternMatch.StringPosition.Anywhere);
            yield return new SearchPatternMatch(this.SeasonFolderPattern, this.SeasonFolderSearchPatternText, SearchPatternMatch.StringPosition.Start);
            yield return new SearchPatternMatch(this.ShortcutExtensionSearchPatternPattern, this.ShortcutExtension, SearchPatternMatch.StringPosition.End);
        }
    }

    protected virtual string SeasonFolderSearchPatternText
        => "Season ";

    protected virtual string ShortcutExtensionSearchPatternPattern
        => "*" + this.ShortcutExtension;

    protected virtual IEnumerable<string> Arguments
        => new string[0];

    protected virtual IEnumerable<string> ExpectedWarnings
        => new string[0];

    public void Process()
    {
        _objectStorage.ArgumentsProcessor.Process();

        var ioServices = _objectStorage.IOServices;

        ioServices.Folder.CreateFolder(this.RootFolderForShortcutFiles);

        _objectStorage.ShortcutFolderProcessor.Process();

        _objectStorage.VideoFolderProcessor.Process();

        _objectStorage.WarningsProcessor.Process();

        this.Assert();
    }

    protected abstract void Assert();

    protected void AssertFolderExists(IObjectStorage os
        , params string[] pathSegments)
    {
        string path;

        path = _objectStorage.IOServices.Path.Combine(pathSegments);

        Debug.Assert(os.IOServices.Folder.Exists(path), string.Format("Folder doesn't exist: {0}", path));
    }

    protected void AssertFileExists(IObjectStorage os
        , params string[] pathSegments)
    {
        string path;

        path = _objectStorage.IOServices.Path.Combine(pathSegments);

        Debug.Assert(os.IOServices.File.Exists(path), string.Format("File doesn't exist: {0}", path));
    }

    public void Dispose()
    {
        _objectStorage.Dispose();
    }
}