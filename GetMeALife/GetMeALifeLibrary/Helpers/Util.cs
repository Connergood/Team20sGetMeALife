using System;
using System.Data.SqlTypes;

namespace GetMeALifeLibrary.Helpers
{
    public static class Util
    {
        #region ArrayIsNullOrEmpty

        /// <summary>
        /// Determines whether or not the specified array is null or has no elements.
        /// </summary>
        /// <param name="array">The array to test</param>
        /// <param name="checkArrayItems">True to check the emptiness of the array's items as well (optional, defaults to false)</param>
        /// <returns>True if the array is null or has a length of 0; otherwise false</returns>
        public static bool ArrayIsNullOrEmpty(Array array, bool checkArrayItems = false)
        {
            if (array == null) return true;
            if (array.Length == 0) return true;

            if (checkArrayItems)
            {
                foreach (var value in array)
                {
                    if (!Util.IsNullOrBlank(value))
                        return false; // found a value, content is not empty
                }
                // did not find a value, content is empty
                return true;
            }

            return false;
        }

        #endregion ArrayIsNullOrEmpty

        #region IsNullOrBlank

        /// <summary>
        /// Indicates whether the passed in String is null or a String with just blank
        /// characters in it.
        /// </summary>
        /// <remarks>
        /// This is different from String.IsNullOrEmpty() in that we test for when a
        /// String is blank (ie: has just spaces in it) rather than just a test for
        /// if it is equal to String.Empty.
        /// </remarks>
        /// <param name="str">The String to test</param>
        /// <returns>True if the String is null or blank</returns>
        public static bool IsNullOrBlank(string str)
        {
            if (str == null || str.Length == 0) return true;
            return String.IsNullOrWhiteSpace(str); // now this is built into .Net :)
        }

        /// <summary>
        /// Indicates whether the passed in Object's String value is null or a String with just blank
        /// characters in it.
        /// </summary>
        /// <remarks>
        /// This function is aware of INullable types and System.DBNull.
        /// </remarks>
        /// <param name="o">The Object to test</param>
        /// <returns>True if the Object's String is null or blank</returns>
        public static bool IsNullOrBlank(object o)
        {
            if (o == null) return true;
            else if (o is INullable) return IsNullOrBlank((INullable)o);
            else if (o == DBNull.Value) return true;
            else return IsNullOrBlank(o.ToString());
        }

        /// <summary>
        /// Indicates whether the passed in INullable's String value is null or a String with just blank
        /// characters in it.
        /// </summary>
        /// <param name="o">The INullable object to test</param>
        /// <returns>True if the object's IsNull property is true or its String value is blank</returns>
        public static bool IsNullOrBlank(INullable o)
        {
            if (o == null) return true;
            if (o.IsNull) return true;
            else return IsNullOrBlank(o.ToString());
        }

        #endregion IsNullOrBlank
    }
}
