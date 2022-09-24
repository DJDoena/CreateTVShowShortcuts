using System;
using System.Collections.Generic;

namespace DoenaSoft.CreateShortcuts.Tests.IOServices
{
    internal sealed class FileSystemMock
    {
        private List<String> m_Files;

        private List<String> m_Folders;

        public IEnumerable<String> Files
        {
            get
            {
                return (m_Files);
            }
        }

        public IEnumerable<String> Folders
        {
            get
            {
                return (m_Folders);
            }
        }

        public FileSystemMock()
        {
            m_Files = new List<String>();
            m_Folders = new List<String>();
        }

        internal void AddFolder(String path)
        {
            Boolean overwritten;

            overwritten = Add(m_Folders, path);

            if (overwritten)
            {
                throw (new NotSupportedException());
            }
        }

        internal Boolean AddFile(String fileName)
        {
            Boolean overwritten;

            overwritten = Add(m_Files, fileName);

            return (overwritten);
        }

        private Boolean Add(List<String> list
            , String path)
        {
            Boolean overwritten;

            if (list.Contains(path))
            {
                overwritten = true;
            }
            else
            {
                list.Add(path);

                overwritten = false;
            }

            return (overwritten);
        }
    }
}