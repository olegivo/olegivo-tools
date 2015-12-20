using System.Windows;
using System.Windows.Controls;
using NLog;

namespace Oleg_ivo.Base.WPF.LogBinding
{
    public class LogLevelStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            var logEvent = item as RenderedLogEvent;

            if (logEvent != null)
            {
                var level = logEvent.Level;
                return GetExplicitStyle(level) ?? GetImplicitStyle(level);
            }
            return null;
        }

        private Style GetExplicitStyle(LogLevel level)
        {
            if (level == LogLevel.Trace) return Trace;
            if (level == LogLevel.Debug) return Debug;
            if (level == LogLevel.Info) return Info;
            if (level == LogLevel.Warn) return Warn;
            if (level == LogLevel.Error) return Error;
            if (level == LogLevel.Fatal) return Fatal;
            return null;
        }

        public Style Trace { get; set; }
        public Style Debug { get; set; }
        public Style Info { get; set; }
        public Style Warn { get; set; }
        public Style Error { get; set; }
        public Style Fatal { get; set; }

        public FrameworkElement StyleSource { get; set; }

        private Style GetImplicitStyle(LogLevel level)
        {
            return StyleSource != null ? StyleSource.TryFindResource(level) as Style : null;
        }
    }
}