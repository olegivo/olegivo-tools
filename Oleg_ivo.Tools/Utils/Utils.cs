using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Oleg_ivo.Tools.Utils
{
    /// <summary>
    /// Утилиты
    /// </summary>
    public abstract class Utils
    {
        /// <summary>
        /// Строковая хрень, обрезающая всё, что левее последнего вхождения символа
        /// </summary>
        /// <param name="s"></param>
        /// <param name="startPos"></param>
        /// <returns></returns>
        /// <param name="symbol">указанный символ</param>
        public static string CutLeftSlash(string s, int startPos, char symbol)
        {
            if (s.Contains(@"\"))
            {
                int pos = s.IndexOf('\\', startPos);
                return s.Substring(pos + 1, s.Length - pos - 1);
            }
            return null;
        }

        /// <summary>
        /// Утилиты работы с перечислениями
        /// </summary>
        public abstract class Enums
        {
            /// <summary>
            /// Проверка на равенство пересечения двух полей нумератора третьему его полю 
            /// (если нумератор позволяет пересечение (атрибут [Flags]))
            /// </summary>
            /// <param name="value1">Первое поле для пересечения</param>
            /// <param name="value2">Второе поле для пересечения</param>
            /// <param name="junctionEqualThis">Поле, с которым сравнивается пересечение</param>
            /// <param name="type">Тип нумератора</param>
            /// <returns>true если пересечение равно полю, false если не равно 
            /// или нумератор не позволяет пересечение (не указан атрибут [Flags])</returns>
            public static bool EnumJunctionEquals(int value1, int value2, int junctionEqualThis, Type type)
            {
                // при сравнении нумераторов типы должны быть равны
                FlagsAttribute flagsAttribute = Reflection.GetAttribute(type, typeof (FlagsAttribute)) as FlagsAttribute;
                if (flagsAttribute != null && (Enum.IsDefined(type, value1) && Enum.IsDefined(type, value1)))
                {
                    bool ret = (value1 & value2) == junctionEqualThis;
                    return ret;
                }
                return false;
            }

            /// <summary>
            /// Проверка вхождения одного перечисления в другое
            /// </summary>
            /// <param name="container">Нумератор, содержание которого проверяется</param>
            /// <param name="containedValue">Нумератор, вхождение которого проверяется</param>
            /// <returns></returns>
            public static bool FlagsValueContains(Enum container, Enum containedValue)
            {
                Type type = container.GetType();
                if(type!=null && type.Equals(containedValue.GetType()))// типы должны быть эквивалентны
                {
                    FlagsAttribute flagsAttribute = Reflection.GetAttribute(type, typeof (FlagsAttribute)) as FlagsAttribute;
                    if(flagsAttribute!=null)// тип нумератора должен быть помечен атрибутом Flags
                    {
                        string containerString = Enum.Format(type, container, "d");
                        string containedValueString = Enum.Format(type, containedValue, "d");
                        uint containerInt, containedValueInt;
                        if(uint.TryParse(containerString, out containerInt) && uint.TryParse(containedValueString, out containedValueInt))
                        {
                            uint junktion = containerInt & containedValueInt;
                            return junktion > 0 /*|| containerInt==0*/ || containedValueInt==0;
                        }
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// Утилиты работы со строками
        /// </summary>
        public abstract class FileUtils
        {
            public static string UnwrapEnvironmentBasedPath(string path)
            {
                if (path.StartsWith("%"))
                {
                    var variable = path.Split(new[] { '%' }, StringSplitOptions.RemoveEmptyEntries).First();
                    path = path.Replace(string.Format("%{0}%", variable),
                        Environment.GetEnvironmentVariable(variable));
                }
                return path;
            }

            public class EnvironmentVariableUsage
            {
                public string Path;
                public string VariableName;
                public string VariableValue;
                public int VariableUsageLength;
            }

            public static EnvironmentVariableUsage GetEnvironmentVariableUsage(string path)
            {
                var variableUsage =
                    Environment.GetEnvironmentVariables()
                        .OfType<DictionaryEntry>()
                        .Where(entry => path.StartsWith((string) entry.Value))
                        .Select(entry =>
                        {
                            var variableValue = (string) entry.Value;
                            return new EnvironmentVariableUsage
                            {
                                Path = path,
                                VariableName = (string) entry.Key,
                                VariableValue = variableValue,
                                VariableUsageLength = variableValue.Length
                            };
                        })
                        .OrderByDescending(usage => usage.VariableUsageLength)
                        .FirstOrDefault();
                return variableUsage;
            }
        }

        /// <summary>
        /// Утилиты работы со строками
        /// </summary>
        public abstract class StringUtils
        {
            ///<summary>
            ///
            ///</summary>
            ///<param name="array"></param>
            ///<returns></returns>
            public static string Array2String(Array array)
            {
                string result = "";
                
                if (array != null)
                {
                    if (array is bool[])
                    {
                        //ushort u = 0;
                        for (int i = 0; i < array.Length; i++)
                        {
                            result += ((bool[])array)[i] ? "1" : "0";
                            //if ((bool)array[i]) u += (ushort)(2 ^ i);
                        }
                    }
                    else
                    {
                        foreach (object o in array)
                        {
                            result += string.Format("{0}{1}", 
                                result.Length > 0 ? Environment.NewLine : "", 
                                o);
                        }

                    }
                }

                return result;
            }

            /// <summary>
            /// Объединяет 2 строки в одну, разделяя их сеператором
            /// </summary>
            /// <param name="s1"></param>
            /// <param name="s2"></param>
            /// <param name="separator"></param>
            /// <returns></returns>
            public static string GetJoining(string s1, string s2, string separator)
            {
                string result = (!string.IsNullOrEmpty(s1) && !string.IsNullOrEmpty(s2)) 
                                    ? separator : null;
                result = string.Format("{0}{1}{2}", s1, result, s2);
                return string.IsNullOrEmpty(result)
                           ? null 
                           : result;
            }

            /// <summary>
            /// Обрамление строки
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            /// <param name="leftFrame"></param>
            /// <param name="rightFrame"></param>
            public static string GetFramingString(string s, string leftFrame, string rightFrame)
            {
                if(string.IsNullOrEmpty(s))
                    leftFrame = rightFrame = null;
                return string.Format("{0}{1}{2}", leftFrame, s, rightFrame);
            }

            /// <summary>
            /// Заключает строку в скобки, если строка не пуста
            /// </summary>
            /// <param name="s"></param>
            /// <returns></returns>
            public static string GetGappedString(string s)
            {
                return GetGappedString(s, Gaps.Parentheses);
            }

            /// <summary>
            /// Заключает строку в скобки, если строка не пуста
            /// </summary>
            /// <param name="s"></param>
            /// <param name="gaps"></param>
            /// <returns></returns>
            public static string GetGappedString(string s, Gaps gaps)
            {
                string leftGap = "";
                string rightGap = "";
                if (string.IsNullOrEmpty(s))
                { 
                    //leftGap = rightGap = null;
                    return null;
                }
                switch (gaps)
                {
                    case Gaps.Parentheses:
                        leftGap = "(";
                        rightGap = ")";
                        break;
                    case Gaps.SquareBrackets:
                        leftGap = "[";
                        rightGap = "]";
                        break;
                    case Gaps.Braces:
                        leftGap = "{";
                        rightGap = "}";
                        break;
                    case Gaps.TriangularBrackets:
                        leftGap = "<";
                        rightGap = ">";
                        break;
                    default:
                        break;
                }
                return string.Format("{0}{1}{2}", leftGap, s, rightGap);
            }

            ///<summary>
            ///
            ///</summary>
            public enum Gaps
            {
                /// <summary>
                /// Круглые скобки
                /// </summary>
                Parentheses,

                /// <summary>
                /// Квадратные скобки
                /// </summary>
                SquareBrackets,

                /// <summary>
                /// Фигурные скобки
                /// </summary>
                Braces,

                /// <summary>
                /// Треугольные скобки
                /// </summary>
                TriangularBrackets,
            }
        }
        
        /// <summary>
        /// Утилиты работы со списками
        /// </summary>
        public abstract class ListUtils
        {
            /// <summary>
            /// Проверка вхождения объекта в массив
            /// </summary>
            /// <param name="list"></param>
            /// <param name="item"></param>
            /// <returns></returns>
            public static bool ListContainsItem(object[] list, object item)
            {
                if(list!=null && list.Length > 0)
                {
                    List<object> lst = new List<object>(list);
                    return lst.Contains(item);
                }
                return false;
            }

            /// <summary>
            /// Строковое представление списка, каждый элемент которого будет разделён строковым разделителем
            /// </summary>
            /// <param name="list">Список</param>
            /// <param name="separator">Строковый разделитель</param>
            /// <returns></returns>
            public static string ToStringSeparating(object[] list, string separator)
            {
                if(list!=null && list.Length > 0)
                {
                    string result = list[0].ToString();
                    for (int i = 1; i < list.Length; i++)
                        result += string.Format("{0}{1}", separator, list[i]);
                    return result;
                }
                return null;
            }

            ///<summary>
            ///
            ///</summary>
            ///<param name="list"></param>
            ///<param name="propertyName"></param>
            ///<param name="propertyValue"></param>
            ///<returns></returns>
            public static bool Remove(IList list, string propertyName, object propertyValue)
            {
                object found = FindItem(list, propertyName, propertyValue);
                if (found != null)
                {
                    list.Remove(found);
                    return true;
                }
                return false;
            }

            /// <summary>
            /// Замещает элемент в списке
            /// </summary>
            /// <param name="list"></param>
            /// <param name="propertyName"></param>
            /// <param name="propertyValue"></param>
            /// <param name="objectToPlace"></param>
            /// <returns></returns>
            public static bool Replace(IList list, string propertyName, object propertyValue, object objectToPlace)
            {
                if(list!=null)
                {
                    int index = IndexOf(list, propertyName, propertyName);
                    if (index > -1)
                    {
                        list[index] = objectToPlace;
                        return true;
                    }
                }
                return false;
            }

            /// <summary>
            /// Возвращает индекс первого вхождения элемента в списке
            /// </summary>
            /// <param name="list"></param>
            /// <param name="propertyName"></param>
            /// <param name="propertyValue"></param>
            /// <returns></returns>
            public static int IndexOf(IList list, string propertyName, object propertyValue)
            {
                if(list!=null)
                    return list.IndexOf(FindItem(list, propertyName, propertyValue));
                return -1;
            }

            /// <summary>
            /// Возвращает элемент из списка по первому вхождению
            /// </summary>
            /// <param name="enumerable"></param>
            /// <param name="propertyName"></param>
            /// <param name="propertyValue"></param>
            /// <returns></returns>
            public static object FindItem(IEnumerable enumerable, string propertyName, object propertyValue)
            {
                if (enumerable != null)
                {
                    PropertyInfo propertyInfo = enumerable.GetType().GetProperty(propertyName);
                    if (propertyInfo != null)
                        foreach (object o in enumerable)
                        {
                            object value = propertyInfo.GetValue(o, null);
                            if (value == propertyValue)
                                return o;
                        }
                }
                return null;
            }

            /// <summary>
            /// Проверка вхождения элемента в список
            /// </summary>
            /// <param name="enumerable"></param>
            /// <param name="propertyName"></param>
            /// <param name="propertyValue"></param>
            /// <returns></returns>
            public static bool Contains(IEnumerable enumerable, string propertyName, object propertyValue)
            {
                if(enumerable!=null)
                {
                    PropertyInfo propertyInfo = enumerable.GetType().GetProperty(propertyName);
                    if(propertyInfo!=null)
                        foreach (object o in enumerable)
                        {
                            object value = propertyInfo.GetValue(o, null);
                            if (value == propertyValue)
                                return true;
                        }
                }
                return false;
            }

            /// <summary>
            /// Объединение
            /// </summary>
            /// <param name="list1"></param>
            /// <param name="list2"></param>
            /// <returns></returns>
            /// <param name="noRepeat">если true, то неповторяющийся список</param>
            public static IList Plus(IList list1, IList list2, bool noRepeat)
            {
                if (noRepeat)
                {
                }
                return null;
            }

            //public static IList 
        }

        #region Utils
        /// <summary>
        /// Проверка наследования (класс унаследован от базового или является им)
        /// </summary>
        /// <param name="type">Тип, значение которого проверяется</param>
        /// <param name="itemType">Тип-эталон</param>
        /// <returns></returns>
        public static bool IsDerivedOrTheSame(Type type, Type itemType)
        {
            if (type != null && itemType != null)                 
                return (type == itemType || type.IsSubclassOf(itemType));
            
            return false;
        }

        #endregion
        
        #region Reflection

        #endregion
    }
}