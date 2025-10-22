using System.ComponentModel;
using System.Reflection;

namespace System
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Extension method to get the description attribute of an Enum member
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string StringValueOf(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString())!;
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);
            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}
