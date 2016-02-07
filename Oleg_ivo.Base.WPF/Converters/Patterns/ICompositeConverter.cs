using System.Windows.Data;

namespace Oleg_ivo.Base.WPF.Converters.Patterns
{
    public interface ICompositeConverter : IValueConverter
    {
        IValueConverter PostConverter { get; set; }
        object PostConverterParameter { get; set; }
    }
}