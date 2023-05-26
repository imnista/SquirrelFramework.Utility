using System.ComponentModel;

namespace SquirrelFramework.Utility.Extensions
{
    /// <Summary>
    /// Extended the System.Enum class
    /// </Summary>
    public static class EnumExtension
    {
        /// <summary>
        /// To get the attribute of a enum field
        /// </summary>
        /// <typeparam name="T">the attribute type</typeparam>
        /// <param name="value">an enum object</param>
        /// <returns> To get the enum attribute <c>null</c> </returns>
        public static T? GetAttribute<T>(this Enum value) where T : Attribute
        {
            var field = value.GetType().GetField(value.ToString());
            return field is not null ? Attribute.GetCustomAttribute(field, typeof(T)) as T : null;
        }

        /// <summary>
        /// Get attribute of [Description]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string? Description(this Enum value)
        {
            return value?.GetAttribute<DescriptionAttribute>()?.Description;
        }
    }
}