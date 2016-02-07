using System;

namespace Oleg_ivo.Base.WPF.Converters.Patterns
{
    public interface ICase
    {
        object Key { get; set; }
        object Value { get; set; }
        Type KeyType { get; set; }
    }
}