using System;
using System.Collections.Generic;
using System.Diagnostics;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;
using DoenaSoft.CreateShortcuts.Tests.IOServices;
using DoenaSoft.CreateShortcuts.Tests.ObjectStorage;

namespace DoenaSoft.CreateShortcuts.Tests.Processors
{
    internal abstract class TestProgramBase : IProgram
    {
        protected readonly IObjectStorage ObjectStorage;

        protected TestProgramBase()
        {
            IObjectFactory of;

            of = ObjectFactory;

            ObjectStorage = of.CreateObjectStorage(this, Arguments);
        }

        public virtual String RootFolderForShortcutFiles
        {
            get
            {
                return (Defaults.RootFolderForShortcutFiles);
            }
        }

        public virtual String SeasonFolderPattern
        {
            get
            {
                return (Defaults.SeasonFolderPattern);
            }
        }

        public virtual String StaffelFolderPattern
        {
            get
            {
                return (Defaults.StaffelFolderPattern);
            }
        }

        public virtual String SeriesNamePattern
        {
            get
            {
                return (Defaults.SeriesNamePattern);
            }
        }

        public virtual String ShortcutExtension
        {
            get
            {
                return (Defaults.ShortcutExtension);
            }
        }

        public virtual IEnumerable<String> VideoFileFolders
        {
            get
            {
                return (Defaults.VideoFileFolders);
            }
        }

        protected virtual IObjectFactory ObjectFactory
        {
            get
            {
                return (new TestObjectFactory(new TestWarningsProcessor(ExpectedWarnings), SearchPatternMatches, LogFileName));
            }
        }

        protected virtual String LogFileName
        {
            get
            {
                return ("TestProgramBase.log");
            }
        }

        protected virtual IEnumerable<SearchPatternMatch> SearchPatternMatches
        {
            get
            {
                SearchPatternMatch match;

                match = new SearchPatternMatch(SeriesNamePattern, String.Empty, SearchPatternMatch.StringPosition.Anywhere);
                yield return (match);

                match = new SearchPatternMatch(SeasonFolderPattern, SeasonFolderSearchPatternText, SearchPatternMatch.StringPosition.Start);
                yield return (match);

                match = new SearchPatternMatch(ShortcutExtensionSearchPatternPattern, ShortcutExtension, SearchPatternMatch.StringPosition.End);
                yield return (match);
            }
        }

        protected virtual String SeasonFolderSearchPatternText
        {
            get
            {
                return ("Season ");
            }
        }

        protected virtual String ShortcutExtensionSearchPatternPattern
        {
            get
            {
                return ("*" + ShortcutExtension);
            }
        }

        protected virtual IEnumerable<String> Arguments
        {
            [DebuggerStepThrough]
            get
            {
                return (new String[0]);
            }
        }

        protected virtual IEnumerable<String> ExpectedWarnings
        {
            [DebuggerStepThrough]
            get
            {
                return (new String[0]);
            }
        }

        public void Process()
        {
            IWarningsProcessor wp;
            IArgumentsProcessor ap;
            IProcessor sfp;
            IProcessor vfp;
            IIOServices ioServices;

            ap = ObjectStorage.ArgumentsProcessor;
            ap.Process();

            ioServices = ObjectStorage.IOServices;

            ioServices.Folder.CreateFolder(RootFolderForShortcutFiles);

            sfp = ObjectStorage.ShortcutFolderProcessor;
            sfp.Process();

            vfp = ObjectStorage.VideoFolderProcessor;
            vfp.Process();

            wp = ObjectStorage.WarningsProcessor;
            wp.Process();

            Assert();            
        }

        protected abstract void Assert();

        protected void AssertFolderExists(IObjectStorage os
            , params String[] pathSegments)
        {
            String path;

            path = ObjectStorage.IOServices.Path.Combine(pathSegments);

            Debug.Assert(os.IOServices.Folder.Exists(path), String.Format("Folder doesn't exist: {0}", path));
        }

        protected void AssertFileExists(IObjectStorage os
            , params String[] pathSegments)
        {
            String path;

            path = ObjectStorage.IOServices.Path.Combine(pathSegments);

            Debug.Assert(os.IOServices.File.Exists(path), String.Format("File doesn't exist: {0}", path));
        }

        public void Dispose()
        {
            ObjectStorage.Dispose();
        }
    }
}