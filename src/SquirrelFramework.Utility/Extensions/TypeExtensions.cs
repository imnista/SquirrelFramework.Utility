namespace SquirrelFramework.Utility.Extensions

{
    public static class TypeExtensions
    {
        /// <summary>
        /// Get type attribute values
        /// Usage:
        /// typeof (TargetClassOrObject).GetAttributeValue<CustomAttribute, CustomValueType>(t => t.CustomAttr)
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="type"></param>
        /// <param name="valueSelector"></param>
        /// <returns></returns>
        public static TValue? GetAttributeValue<TAttribute, TValue>(
            this Type type,
            Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            var att = type.GetCustomAttributes(
                typeof(TAttribute), true
            ).FirstOrDefault() as TAttribute;
            return att == null ? default : valueSelector(att);
        }

        /// <summary>
        ///     To get the attributes of a object
        /// </summary>
        /// <typeparam name="T">the attribute type</typeparam>
        /// <param name="value">an type object</param>
        /// <returns> To get the type attributes</returns>
        public static T[] GetAttributes<T>(this Type value) where T : Attribute
        {
            var attributes = from attribute in value.GetCustomAttributes(typeof(T), true)
                             let strongTypeAttribute = attribute as T
                             orderby strongTypeAttribute descending
                             select strongTypeAttribute;
            return attributes.ToArray();
        }

        /// <summary>
        ///     To get the attribute of a object
        /// </summary>
        /// <typeparam name="T">the attribute type</typeparam>
        /// <param name="value">an type object</param>
        /// <returns> To get the type attribute</returns>
        public static T? GetAttribute<T>(this Type value) where T : Attribute
        {
            return value.GetAttributes<T>().Length == 0
                ? default
                : value.GetAttributes<T>()[0];
        }
    }
}