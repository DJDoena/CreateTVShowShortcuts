using System;
using System.Collections.Generic;

namespace DoenaSoft.CreateShortcuts
{
    public static class Defaults
    {
        public const String RootFolderForShortcutFiles = @"D:\Videos\Links";

        public const String SeasonFolderPattern = "Season *";

        public const String StaffelFolderPattern = "Staffel *";

        public const String SeriesNamePattern = "*.*";

        public const String ShortcutExtension = ".lnk";

        public static IEnumerable<String> VideoFileFolders
        {
            get
            {
                yield return (@"N:\Drive1\TVShows\");
                yield return (@"N:\Drive2\TVShows\");
                yield return (@"N:\Drive3\TVShows\");
            }
        }
    }
}
