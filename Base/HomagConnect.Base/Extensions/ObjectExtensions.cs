using Newtonsoft.Json;

namespace HomagConnect.Base.Extensions
{
    /// <summary>
    /// <see cref="object" /> extension methods.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Converts an object to a specified type by using JSON serialization.
        /// </summary>
        public static T ConvertTo<T>(this object o)
        {
            var serializedObject = JsonConvert.SerializeObject(o);
            return JsonConvert.DeserializeObject<T>(serializedObject);
        }

        /// <summary>
        /// Gets the value of a property.
        /// </summary>
        public static object GetPropertyValue(this object o, string propertyName)
        {
            object objValue = string.Empty;

            var propertyInfo = o.GetType().GetProperty(propertyName);

            if (propertyInfo != null)
            {
                objValue = propertyInfo.GetValue(o, null);
            }

            return objValue;
        }
    }
}