using System.Windows.Data;

namespace Oleg_ivo.Base.WPF.Converters.Patterns
{
    public interface ISwitchConverter : IValueConverter
    {
        CaseSet Cases { get; }
        object Default { get; set; }
        bool TypeMode { get; set; }
    }
}