using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using DoenaSoft.CreateShortcuts.Interfaces.ObjectStorage;
using DoenaSoft.CreateShortcuts.Interfaces.Processors;

namespace DoenaSoft.CreateShortcuts.Implementation.Processors
{
    internal sealed class ArgumentsProcessor : IArgumentsProcessor
    {
        private readonly IObjectStorage _objectStorage;

        private readonly List<string> _arguments;

        private bool _dualLog;

        private List<string> Warnings { get; set; }

        public bool AppendArticles { get; private set; }

        public string LogFile { get; private set; }

        public bool ShowHelp { get; private set; }

        public ArgumentsProcessor(IObjectStorage os)
        {
            _objectStorage = os;
            _arguments = _objectStorage.Arguments.ToList();
            this.Warnings = new List<string>();

            this.AppendArticles = true;
            this.LogFile = null;
            _dualLog = false;
            this.ShowHelp = false;
        }

        public bool DualLog
        {
            get
            {
                var dualLog = _dualLog && (string.IsNullOrEmpty(this.LogFile) == false);

                return dualLog;
            }
        }

        public void Process()
        {
            this.Warnings = new List<string>();

            if (this.HasItems())
            {
                foreach (var arg in _arguments)
                {
                    this.ProcessArg(arg);
                }
            }

            if (this.ShowHelp)
            {
                return;
            }

            if (_dualLog && string.IsNullOrEmpty(this.LogFile))
            {
                string warning;

                warning = string.Format("Invalid combination: \"{0}\", but no \"{1}\"", "/DualLog=true", "/LogFile={{FileName}}");

                this.Warnings.Add(warning);
            }

            var wp = _objectStorage.WarningsProcessor;

            wp.AddWarnings(this.Warnings);
        }

        private bool HasItems()
        {
            var hasItems = (_arguments != null) && (_arguments.Count > 0);

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
                this.AppendArticles = this.ProcessBooleanArgument(arg, loweredArg, LoweredAppendArticlesKey);
            }
            else if (loweredArg.StartsWith(LoweredLogFileKey))
            {
                this.ProcessLogFileArgument(arg);
            }
            else if (loweredArg.StartsWith(LoweredDualLogKey))
            {
                _dualLog = this.ProcessBooleanArgument(arg, loweredArg, LoweredDualLogKey);
            }
            else if ((loweredArg == LoweredHelpKey1) || (arg == HelpKey2))
            {
                this.ShowHelp = true;
            }
            else
            {
                this.AppendInvalidArgumentWarning(arg);
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
                    this.LogFile = split[1];

                    return;
                }
            }

            this.AppendInvalidArgumentWarning(arg);
        }

        private bool ProcessBooleanArgument(string arg, string loweredArg, string LoweredArgKey)
        {
            var value = loweredArg == LoweredArgKey || this.ProcessBooleanArgument(arg);

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

            this.AppendInvalidArgumentWarning(arg);

            return false;
        }

        private void AppendInvalidArgumentWarning(string arg)
        {
            var warning = string.Format("Invalid argument: {0}", arg);

            this.Warnings.Add(warning);
        }

        public void PrintArguments()
        {
            var logger = _objectStorage.Logger;

            logger.WriteLineForInput("Parameters:");
            logger.WriteLineForInput("/AppendArticles[=false]");
            logger.WriteLineForInput("/DualLog[=false]");
            logger.WriteLineForInput("/LogFile={{FileName}}");
            logger.WriteLineForInput("/Help");
            logger.WriteLineForInput("/?");
        }
    }
}