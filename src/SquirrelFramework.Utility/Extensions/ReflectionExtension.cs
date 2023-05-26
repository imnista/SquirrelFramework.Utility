using System.Reflection;

namespace SquirrelFramework.Utility.Extensions
{
    public static class ReflectionExtension
    {
        public static T? GetAttribute<T>(this PropertyInfo propertyInfo)
            where T : Attribute
        {
            return Attribute.GetCustomAttribute(propertyInfo, typeof(T)) as T;
        }
    }
}