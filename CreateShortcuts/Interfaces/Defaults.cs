using System;
using System.Collections.Generic;

namespace DoenaSoft.CreateShortcuts
{
    public static class Defaults
    {
        public const string RootFolderForShortcutFiles = @"D:\Videos\Links";

        public const string SeasonFolderPattern = "Season *";

        public const string StaffelFolderPattern = "Staffel *";

        public const string SeriesNamePattern = "*.*";

        public const string ShortcutExtension = ".lnk";

        public static IEnumerable<string> VideoFileFolders
        {
            get
            {
                yield return @"N:\Drive1\TVShows\";
                yield return @"N:\Drive2\TVShows\";
                yield return @"N:\Drive3\TVShows\";
            }
        }
    }
}
