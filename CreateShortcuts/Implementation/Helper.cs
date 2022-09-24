using System.Collections.Generic;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.CreateShortcuts.Interfaces;

namespace DoenaSoft.CreateShortcuts.Implementation
{
    internal sealed class Helper : IHelper
    {
        public bool IsSpecialFolder(IFolderInfo di)
        {
            var isSpecialFolder = SpecialFolders.Contains(di.Name);

            return isSpecialFolder;
        }

        private IEnumerable<string> SpecialFolders
        {
            get
            {
                yield return "The Ultimate Pilot Collection";
                yield return "Movies";
            }
        }
    }
}