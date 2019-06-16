using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace GetMeALifeLibrary.Helpers
{
    public static class _Extensions
    {
        #region Array Extensions

        /// <summary>
        /// Combines the two arrays together into one array that contains elements of both arrays.
        /// </summary>
        /// <typeparam name="T">The array's type</typeparam>
        /// <param name="arr">The array we are extending</param>
        /// <param name="otherArr">The array to combine</param>
        /// <returns>The combined array</returns>
        public static T[] Concat<T>(this T[] arr, T[] otherArr)
        {
            if (arr == null) return otherArr;
            if (otherArr == null) return arr;

            T[] newArr = new T[arr.Length + otherArr.Length];
            Array.Copy(arr, 0, newArr, 0, arr.Length);
            Array.Copy(otherArr, 0, newArr, arr.Length, otherArr.Length);

            return newArr;
        }

        /// <summary>
        /// Finds the index of the specified <see cref="T"/> object in the array.
        /// </summary>
        /// <typeparam name="T">The array's type</typeparam>
        /// <param name="arr">The array we are searching</param>
        /// <param name="toFind">The object to find within the array</param>
        /// <returns>The index of the object in the array; </returns>
        public static int IndexOf<T>(this T[] arr, T toFind)
        {
            for (int i = 0; i < arr.Length; i++)
                if (Object.ReferenceEquals(arr[i], toFind))
                    return i;
            return -1;
        }

        /// <summary>
        /// Returns a subset of the array.
        /// </summary>
        /// <typeparam name="T">The array's type</typeparam>
        /// <param name="arr">The array we are extending</param>
        /// <param name="startIndex">The index in the source array to start the subset at</param>
        /// <returns>A subset of the array</returns>
        public static T[] SubArray<T>(this T[] arr, int startIndex)
        {
            return SubArray(arr, startIndex, arr.Length - startIndex);
        }

        /// <summary>
        /// Returns a subset of the array.
        /// </summary>
        /// <typeparam name="T">The array's type</typeparam>
        /// <param name="arr">The array we are extending</param>
        /// <param name="startIndex">The index in the source array to start the subset at</param>
        /// <param name="length">The length of the subset array</param>
        /// <returns>A subset of the array</returns>
        public static T[] SubArray<T>(this T[] arr, int startIndex, int length)
        {
            // bounds check the length and make it shorter if the caller's math sucked
            if (startIndex + length > arr.Length)
                length = startIndex + length - 1;
            if (length <= 0)
                return new T[0];

            T[] newArr = new T[length];
            Array.Copy(arr, startIndex, newArr, 0, length);
            return newArr;
        }

        #endregion Array Extensions

        #region IEnumerable<T> Extensions

        /// <summary>
        /// Creates a dictionary keyed by the result of a lambda expression our of an <see cref="IEnumerable"/>.
        /// If multiple values match a single key only the first value is kept.
        /// </summary>
        /// <typeparam name="TKey">The Type of the result of the lambda expression</typeparam>
        /// <typeparam name="TValue">The Type of the values in the IEnumerable</typeparam>
        /// <param name="enumerable">The IEnumerable instance to convert to a Dictionary</param>
        /// <param name="keyExpr">The expression that defines the key</param>
        /// <returns>The resulting Dictionary</returns>
        public static Dictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IEnumerable<TValue> enumerable, Expression<Func<TValue, TKey>> keyExpr)
        {
            if (enumerable == null) return null;

            Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(enumerable.Count());
            foreach (TValue value in enumerable)
            {
                TKey key = keyExpr.Compile()(value);
                if (!dictionary.ContainsKey(key))
                    dictionary.Add(key, value);
            }
            return dictionary;
        }

        /// <summary>
        /// Creates a dictionary keyed by the result of a lambda expression our of an <see cref="IEnumerable"/>.
        /// </summary>
        /// <typeparam name="TKey">The Type of the result of the lambda expression</typeparam>
        /// <typeparam name="TValue">The Type of the values in the IEnumerable</typeparam>
        /// <param name="enumerable">The IEnumerable instance to convert to a Dictionary</param>
        /// <param name="keyExpr">The expression that defines the key</param>
        /// <returns>The resulting Dictionary</returns>
        public static Dictionary<TKey, List<TValue>> AsDictionaryWithMultipleValues<TKey, TValue>(this IEnumerable<TValue> enumerable, Expression<Func<TValue, TKey>> keyExpr)
        {
            if (keyExpr == null) throw new ArgumentNullException("keyExpr");
            if (enumerable == null) return null;

            Dictionary<TKey, List<TValue>> dictionary = new Dictionary<TKey, List<TValue>>(enumerable.Count());
            foreach (TValue value in enumerable)
            {
                TKey key = keyExpr.Compile()(value);
                if (!dictionary.ContainsKey(key))
                    dictionary.Add(key, new List<TValue>(new TValue[] { value }));
                else dictionary[key].Add(value);
            }
            return dictionary;
        }

        /// <summary>
        /// Creates a dictionary keyed by the result of a lambda expression our of an <see cref="IEnumerable"/>.
        /// </summary>
        /// <typeparam name="TValue">The Type of the result of the lambda expression</typeparam>
        /// <typeparam name="TValue">The Type of the values in the IEnumerable</typeparam>
        /// <param name="enumerable">The IEnumerable instance to convert to a Dictionary</param>
        /// <param name="comparer">The expression that defines the key</param>
        /// <returns>The resulting Dictionary</returns>
        public static IEnumerable<TValue> Distinct<TValue>(this IEnumerable<TValue> enumerable, Expression<Func<TValue, TValue, bool>> comparer)
        {
            List<TValue> list = new List<TValue>(enumerable.Count());
            var comparerExp = comparer.Compile();

            foreach (TValue value in enumerable)
            {
                if (list.Count == 0)
                {
                    list.Add(value);
                    continue;
                }

                var match = (from m in list
                             where comparerExp(value, m) || Object.ReferenceEquals(value, m)
                             select m).FirstOrDefault();
                if (match == null) // if we didn't find a match
                    list.Add(value);
            }

            return list;
        }

        /// <summary>
        /// Splits an array into several smaller arrays.
        /// </summary>
        /// <remarks>
        /// Taken from: http://stackoverflow.com/questions/18986129/c-splitting-an-array-into-n-parts
        /// </remarks>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to split.</param>
        /// <param name="size">The size of the smaller arrays.</param>
        /// <returns>An array containing smaller arrays.</returns>
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> array, int size)
        {
            if (size <= 0) throw new ArgumentOutOfRangeException("size", "size cannot be less than 1");

            int enumberableSize = array.Count();
            if (enumberableSize < size)
                yield return array;

            for (var i = 0; i < (float)enumberableSize / size; i++)
            {
                yield return array.Skip(i * size).Take(size);
            }
        }

        #endregion IEnumerable<T> Extensions

        #region String Extensions

        /// <summary>
        /// Counts the number of times the specified character appears in the <see cref="System.String"/>.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="c">The character to count</param>
        /// <returns>The number of times the character appears in the string</returns>
        public static int CharCount(this string str, char c)
        {
            if (str == null) return 0;

            int count = 0;
            foreach (char ch in str)
            {
                if (ch == c)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="System.Char"/>
        /// occurs within this string.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The <see cref="System.Char"/> to seek</param>
        /// <returns>True if the value is found; otherwise false</returns>
        public static bool Contains(this string str, char value)
        {
            return str.IndexOf(value) > -1;
        }

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="System.String"/> object
        /// occurs within this string.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The <see cref="System.String"/> object to seek</param>
        /// <param name="comparisonType">One of the <see cref="System.StringComparison"/> values</param>
        /// <returns>True if the value is found; otherwise false</returns>
        public static bool Contains(this string str, string value, StringComparison comparisonType)
        {
            if (str == null) return value == null; // only allow null if it contains null (null contains null, else false)

            return str.IndexOf(value, comparisonType) > -1;
        }

        /// <summary>
        /// Returns a value indicating whether the specified <see cref="System.String"/> object
        /// occurs within this string without regard to character case.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The <see cref="System.String"/> object to seek</param>
        /// <returns>True if the value is found; otherwise false</returns>
        public static bool ContainsIgnoreCase(this string str, string value)
        {
            return Contains(str, value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether the end of this string instance matches a specified char.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="c">The char to compare with the end of the string</param>
        /// <returns>True if the last character matches the end of this instance; otherwise false</returns>
        public static bool EndsWith(this string str, char c)
        {
            if (String.IsNullOrEmpty(str)) return false;
            return str[str.Length - 1] == c;
        }

        /// <summary>
        /// Determines whether the end of this string matches the specified string. 
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The string to compare</param>
        /// <returns>True if this instance ends with value; otherwise false</returns>
        public static bool EndsWithIgnoreCase(this string str, string value)
        {
            if (value == null) return false;
            return str.EndsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Determines whether this string and a specified <see cref="System.String"/> object
        /// have the same value ignoring case sensitivity.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">A <see cref="System.String"/> object</param>
        /// <returns>True if the values are equal; otherwise false</returns>
        public static bool EqualsIgnoreCase(this string str, string value)
        {
            if (str == null && value == null) return true;
            return str != null && str.Equals(value, StringComparison.OrdinalIgnoreCase);
        }
        
        /// <summary>
        /// Reports the index of the first occurrence in this instance of any <cref="System.String"/> in a
        /// specified array of <cref="System.String"/>s.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="anyOf">A <cref="System.String"/> array containing one or more strings to seek. </param>
        /// <returns>The zero-based index position of the first occurrence in this instance where any string in anyOf was found; -1 if no character in anyOf was found</returns>
        public static int IndexOfAny(this string str, string[] anyOf)
        {
            return IndexOfAny(str, anyOf, StringComparison.Ordinal);
        }

        /// <summary>
        /// Reports the index of the first occurrence in this instance of any <cref="System.String"/> in a
        /// specified array of <cref="System.String"/>s.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="anyOf">A <cref="System.String"/> array containing one or more strings to seek. </param>
        /// <param name="comparisonType">One of the <see cref="System.StringComparison"/> values</param>
        /// <returns>The zero-based index position of the first occurrence in this instance where any string in anyOf was found; -1 if no character in anyOf was found</returns>
        public static int IndexOfAny(this string str, string[] anyOf, StringComparison comparisonType)
        {
            if (Util.ArrayIsNullOrEmpty(anyOf)) return -1;

            foreach (string value in anyOf)
            {
                int index = str.IndexOf(value, comparisonType);
                if (index > -1)
                    return index;
            }

            return -1;
        }

        /// <summary>
        /// Reports the indices of all occurrences of the specified string in the
        /// current <see cref="System.String"/> object.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The <see cref="System.String"/> object to seek</param>
        /// <returns>All indices to which the string occurs; null if none</returns>
        public static int[] IndicesOf(this string str, string value)
        {
            return IndicesOf(str, value, 0, StringComparison.Ordinal);
        }

        /// <summary>
        /// Reports the indices of all occurrences of the specified string in the
        /// current <see cref="System.String"/> object.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The <see cref="System.String"/> object to seek</param>
        /// <param name="startIdx">The search starting position</param>
        /// <returns>All indices to which the string occurs; null if none</returns>
        public static int[] IndicesOf(this string str, string value, int startIdx)
        {
            return IndicesOf(str, value, startIdx, StringComparison.Ordinal);
        }

        /// <summary>
        /// Reports the indices of all occurrences of the specified string in the
        /// current <see cref="System.String"/> object.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The <see cref="System.String"/> object to seek</param>
        /// <param name="startIdx">The search starting position</param>
        /// <param name="comparisonType">One of the <see cref="System.StringComparison"/> values</param>
        /// <returns>All indices to which the string occurs; null if none</returns>
        public static int[] IndicesOf(this string str, string value, int startIdx, StringComparison comparisonType)
        {
            List<int> indices = new List<int>();

            while (startIdx < str.Length)
            {
                int foundIdx = str.IndexOf(value, startIdx, comparisonType);
                if (foundIdx != -1)
                {
                    indices.Add(foundIdx);
                    startIdx = foundIdx + value.Length; // start after the occurrence we just found
                }
                else break;
            }

            if (indices.Count == 0)
                return null;
            return indices.ToArray();
        }

        /// <summary>
        /// Gets the last number of specified characters of the <see cref="System.String"/>.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="length">The number of last characters to return</param>
        /// <returns>The last characters as a string</returns>
        public static string Last(this string str, int length)
        {
            if (str.Length <= length)
                return str;
            else
                return str.Substring(str.Length - length);
        }

        /// <summary>
        /// Escapes certain reserved xml characters from the string.
        /// </summary>
        /// <param name="value">The string we are extending</param>
        /// <returns>The escaped xml string</returns>
        public static string EscapeValueForXml(this string value)
        {
            // see also: SecurityElement.Escape()
            StringBuilder sb = new StringBuilder(value);
            sb.Replace("&", "&amp;");
            sb.Replace("'", "&apos;");
            sb.Replace(":", "&#58;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\"", "&quot;");
            return sb.ToString();
        }

        /// <summary>
        /// Unescapes certain reserved xml characters from the string.
        /// </summary>
        /// <param name="value">The string we are extending</param>
        /// <returns>The escaped xml string</returns>
        public static string UnEscapeValueForXml(this string value)
        {
            StringBuilder sb = new StringBuilder(value);
            sb.Replace("&amp;", "&");
            sb.Replace("&apos;", "'");
            sb.Replace("&#58;", ":");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&quot;", "\"");
            return sb.ToString();
        }

        /// <summary>
        /// Returns a new string in which all instances of the specified string are removed from
        /// the current string.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The string value to remove</param>
        /// <returns>A new string without the removed string value in it</returns>
        public static string Remove(this string str, string value)
        {
            return str.Replace(value, String.Empty);
        }

        /// <summary>
        /// Returns a new string in which all instances of the specified character are removed from
        /// the current string.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The character to remove</param>
        /// <returns>A new string without the removed character in it</returns>
        public static string RemoveChar(this string str, char value)
        {
            if (String.IsNullOrEmpty(str)) return str;

            StringBuilder sb = new StringBuilder(str.Length);
            foreach (char ch in str)
                if (ch != value) sb.Append(ch);
            return sb.ToString();
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified strings in the current
        /// instance are replaced with another specified string.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="oldValues">An array of strings to be replaced</param>
        /// <param name="newValue">The string to replace all occurrences of oldValue</param>
        /// /// <returns>A string that is equivalent to the current string except that all instances of oldValues are replaced with newValue</returns>
        public static string Replace(this string str, string[] oldValues, string newValue)
        {
            if (oldValues == null) throw new ArgumentNullException("oldValues");
            if (oldValues.Length == 0) return str;

            StringBuilder b = new StringBuilder(str);
            foreach (string old in oldValues)
                b.Replace(old, newValue);
            return b.ToString();
        }

        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current
        /// instance are replaced with another specified string.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="oldValue">The string to be replaced</param>
        /// <param name="newValue">The string to replace all occurrences of oldValue</param>
        /// <param name="comparisionType">One of the <see cref="System.StringComparison"/> values</param>
        /// <returns>A string that is equivalent to the current string except that all instances of oldValue are replaced with newValue</returns>
        public static string Replace(this string str, string oldValue, string newValue,
            StringComparison comparisionType)
        {
            if (oldValue == null) throw new ArgumentNullException("oldValue");
            if (newValue == null) throw new ArgumentNullException("newValue");

            string result = str;
            if (oldValue != newValue)
            {
                int index = -1;
                int lastIndex = 0;

                StringBuilder buffer = new StringBuilder(str.Length);
                // while there are still instances of oldValue in the string
                while ((index = str.IndexOf(oldValue, index + 1, comparisionType)) >= 0)
                {
                    buffer.Append(str, lastIndex, index - lastIndex);
                    buffer.Append(newValue);

                    lastIndex = index + oldValue.Length;
                }
                buffer.Append(str, lastIndex, str.Length - lastIndex);

                result = buffer.ToString();
            }
            return result;
        }

        /// <summary>
        /// Reverses the string.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        public static string Reverse(this string str)
        {
            StringBuilder sb = new StringBuilder(str.Length);
            for (int i = str.Length - 1; i >= 0; --i)
                sb.Append(str[i]);
            return sb.ToString();
        }

        /// <summary>
        /// A substring function that performs bounds checks.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="startIdx">The zero-based starting character position of a substring in this instance</param>
        /// <param name="length">The maximum number of characters in the substring</param>
        /// <returns>The substring</returns>
        public static string SafeSubstring(this string str, int startIdx, int length)
        {
            if (str == null) return null;
            if (startIdx < 0) startIdx = 0;
            if (startIdx >= str.Length) return "";

            if (startIdx + length > str.Length)
                return str.Substring(startIdx);
            return str.Substring(startIdx, length);
        }

        /// <summary>
        /// A substring function that performs bounds checks.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="startIdx">The zero-based starting character position of a substring in this instance</param>
        /// <param name="length">The maximum number of characters in the substring</param>
        /// <param name="suffix">The string to append to the result if the string was trimmed</param>
        /// <returns>The substring</returns>
        public static string SafeSubstring(this string str, int startIdx, int length, string suffix)
        {
            string result = SafeSubstring(str, startIdx, length);
            if (result != null && result.Length == length && str.Length != length && suffix != null)
                return result.TrimEnd() + suffix;
            return result;
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited
        /// by elements of a specified Unicode character.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="separator">A Unicode character that delimits the substrings in this string</param>
        /// <param name="options">RemoveEmptyEntries to omit empty array elements from the array returned; or None to include empty array elements in the array returned</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in separator</returns>
        public static string[] Split(this string str, char separator, StringSplitOptions options)
        {
            return str.Split(new char[] { separator }, options);
        }

        /// <summary>
        /// Returns a string array that contains the substrings in this string that are delimited
        /// by elements of a specified string.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="separator">A string that delimits the substrings in this string</param>
        /// <param name="options">RemoveEmptyEntries to omit empty array elements from the array returned; or None to include empty array elements in the array returned</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited by one or more characters in separator</returns>
        public static string[] Split(this string str, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return str.Split(new string[] { separator }, options);
        }

        /// <summary>
        /// Returns a string array that contains the trimmed substrings in this string that are delimited
        /// by elements of a specified Unicode character.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="separator">A Unicode character that delimits the substrings in this string</param>
        /// <param name="options">RemoveEmptyEntries to omit empty array elements from the array returned; or None to include empty array elements in the array returned</param>
        /// <returns>An array whose elements contain the trimmed substrings in this string that are delimited by one or more characters in separator</returns>
        public static string[] SplitAndTrim(this string str, char separator, StringSplitOptions options = StringSplitOptions.None)
        {
            var strings = str.Split(separator, options);

            for (int i = 0; i < strings.Length; i++)
                strings[i] = strings[i].Trim();

            return strings;
        }

        /// <summary>
        /// Returns a string array that contains the trimmed substrings in this string that are delimited
        /// by elements of a specified string.
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="separator">A string that delimits the substrings in this string</param>
        /// <param name="options">RemoveEmptyEntries to omit empty array elements from the array returned; or None to include empty array elements in the array returned</param>
        /// <returns>An array whose elements contain the trimmed substrings in this string that are delimited by one or more characters in separator</returns>
        public static string[] SplitAndTrim(this string str, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            var strings = str.Split(separator, options);

            for (int i = 0; i < strings.Length; i++)
                strings[i] = strings[i].Trim();

            return strings;
        }

        /// <summary>
        /// Determines whether the beginning of this string matches the specified char. 
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="c">The char to compare with the start of the string</param>
        /// <returns>True if the first character matches the start of this instance; otherwise false</returns>
        public static bool StartsWith(this string str, char c)
        {
            if (String.IsNullOrEmpty(str)) return false;
            return str[0] == c;
        }

        /// <summary>
        /// Determines whether the beginning of this string matches the specified string. 
        /// </summary>
        /// <param name="str">The string we are extending</param>
        /// <param name="value">The string to compare</param>
        /// <returns>True if this instance begins with value; otherwise false</returns>
        public static bool StartsWithIgnoreCase(this string str, string value)
        {
            if (value == null) return false;
            return str.StartsWith(value, StringComparison.OrdinalIgnoreCase);
        }

        #endregion String Extensions
    }
}
