namespace Oleg_ivo.Base.Extensions
{
    public static class StringExtensions
    {

        /// <summary>
        ///  Indicates whether the string is null or an Empty string.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        ///  Indicates whether the string is Nothing, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrWhitespace(this string s)
        {
            if (s != null)
                for (int i = 0; i < s.Length; i++)
                    if (!char.IsWhiteSpace(s[i])) return false;

            return true;
        }


        /// <summary>
        /// Returns a null value if the specified <see cref="System.String"/> object is null or an <see cref="System.String.Empty"/> string.
        /// </summary>
        public static string NullIfEmpty(this string s)
        {
            return (s == string.Empty) ? null : s;
        }

        /// <summary>
        /// Returns a null value if the specified <see cref="System.String"/> object is null 
        /// or an <see cref="System.String.Empty"/> string, or consists only of white-space characters.
        /// </summary>
        public static string NullIfEmptyOrWhitespace(this string s)
        {
            return (s.IsNullOrWhitespace()) ? null : s;
        }


        public static string Left(this string s, int length)
        {
            if (s == null) return null;
            if (length >= s.Length) return s;
            return s.Substring(0, length);
        }

        public static string Right(this string s, int length)
        {
            if (s == null) return null;
            if (length >= s.Length) return s;
            return s.Substring(s.Length - length, length);
        }
    }
}