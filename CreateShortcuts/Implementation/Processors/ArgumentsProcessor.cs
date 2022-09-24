using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.Processors
{
    internal sealed class ArgumentsProcessor : IArgumentsProcessor
    {
        private readonly IObjectStorage ObjectStorage;

        private readonly List<string> Arguments;

        private List<string> Warnings { get; set; }

        public bool AppendArticles { get; private set; }

        public string LogFile { get; private set; }

        private bool m_DualLog;

        public bool ShowHelp { get; private set; }

        public ArgumentsProcessor(IObjectStorage os)
        {
            ObjectStorage = os;
            Arguments = ObjectStorage.Arguments.ToList();
            Warnings = new List<string>();

            AppendArticles = true;
            LogFile = null;
            m_DualLog = false;
            ShowHelp = false;
        }

        public bool DualLog
        {
            get
            {
                bool dualLog;

                dualLog = (m_DualLog) && (string.IsNullOrEmpty(LogFile) == false);

                return dualLog;
            }
        }

        public void Process()
        {
            Warnings = new List<string>();

            if (HasItems())
            {
                foreach (var arg in Arguments)
                {
                    ProcessArg(arg);
                }
            }

            if (ShowHelp)
            {
                return;
            }

            if (m_DualLog && string.IsNullOrEmpty(LogFile))
            {
                string warning;

                warning = string.Format("Invalid combination: \"{0}\", but no \"{1}\"", "/DualLog=true", "/LogFile={{FileName}}");

                Warnings.Add(warning);
            }

            var wp = ObjectStorage.WarningsProcessor;
            wp.AddWarnings(Warnings);
        }

        private bool HasItems()
        {
            var hasItems = (Arguments != null) && (Arguments.Count > 0);

            return hasItems;
        }

        private void ProcessArg(string arg)
        {
            const string LoweredAppendArticlesKey = "/appendarticles";
            const string LoweredLogFileKey = "/logfile";
            const string LoweredDualLogKey = "/duallog";
            const string LoweredHelpKey1 = "/help";
            const string HelpKey2 = "/?";

            var loweredArg = arg.ToLower(CultureInfo.InvariantCulture);

            if (loweredArg.StartsWith(LoweredAppendArticlesKey))
            {
                AppendArticles = ProcessBooleanArgument(arg, loweredArg, LoweredAppendArticlesKey);
            }
            else if (loweredArg.StartsWith(LoweredLogFileKey))
            {
                ProcessLogFileArgument(arg);
            }
            else if (loweredArg.StartsWith(LoweredDualLogKey))
            {
                m_DualLog = ProcessBooleanArgument(arg, loweredArg, LoweredDualLogKey);
            }
            else if ((loweredArg == LoweredHelpKey1) || (arg == HelpKey2))
            {
                ShowHelp = true;
            }
            else
            {
                AppendInvalidArgumentWarning(arg);
            }
        }

        private void ProcessLogFileArgument(string arg)
        {
            var split = arg.Split('=');

            if (split.Length == 2)
            {
                var invalidChars = System.IO.Path.GetInvalidFileNameChars();

                if (split[1].IndexOfAny(invalidChars) == -1)
                {
                    LogFile = split[1];

                    return;
                }
            }

            AppendInvalidArgumentWarning(arg);
        }

        private bool ProcessBooleanArgument(string arg, string loweredArg, string LoweredArgKey)
        {
            var value = loweredArg == LoweredArgKey ? true : ProcessBooleanArgument(arg);

            return value;
        }

        private bool ProcessBooleanArgument(string arg)
        {
            var split = arg.Split('=');

            if (split.Length == 2)
            {
                if (bool.TryParse(split[1], out var value))
                {
                    return value;
                }
            }

            AppendInvalidArgumentWarning(arg);

            return false;
        }

        private void AppendInvalidArgumentWarning(string arg)
        {
            var warning = string.Format("Invalid argument: {0}", arg);

            Warnings.Add(warning);
        }

        public void PrintArguments()
        {
            var logger = ObjectStorage.Logger;

            logger.WriteLineForInput("Parameters:");
            logger.WriteLineForInput("/AppendArticles[=false]");
            logger.WriteLineForInput("/DualLog[=false]");
            logger.WriteLineForInput("/LogFile={{FileName}}");
            logger.WriteLineForInput("/Help");
            logger.WriteLineForInput("/?");
        }
    }
}