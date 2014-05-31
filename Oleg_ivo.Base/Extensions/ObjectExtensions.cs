using System;

namespace Oleg_ivo.Base.Extensions
{
    public static class ObjectExtensions
    {
        #region IsNumeric
        public static bool IsNumeric(this object x)
        {
            return x!=null && IsNumeric(x.GetType());
        }

        private static bool IsNumeric(Type type)
        {
            return IsNumeric(type, Type.GetTypeCode(type));
        }

        private static bool IsNumeric(Type type, TypeCode typeCode)
        {
            return (typeCode == TypeCode.Decimal ||
                    (type.IsPrimitive && typeCode != TypeCode.Object && typeCode != TypeCode.Boolean &&
                     typeCode != TypeCode.Char));
        }

        #endregion
        
        #region IsBool
		public static bool IsBool(this object x)
        {
            return x != null && IsBool(x.GetType());
        }

        private static bool IsBool(Type type)
        {
            return IsBool(type, Type.GetTypeCode(type));
        }

        private static bool IsBool(Type type, TypeCode typeCode)
        {
            return (type.IsPrimitive && typeCode == TypeCode.Boolean);
        }

	    #endregion    
    }
}
