using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace dotnetCampus.ClrNamedAttachedProperty
{
    /// <summary>
    /// 给任意对象添加属性
    /// </summary>
    public static class NamedAttachedProperty
    {
        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetNamedAttachedProperty(this object obj, string name, object value)
        {
            var dictionary = AttachePropertyList.GetValue(obj, key => new ConcurrentDictionary<string, object>());
            dictionary.AddOrUpdate(name, value, (s, o) => value);
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static object GetNamedAttachedProperty(this object obj, string name)
        {
            if (AttachePropertyList.TryGetValue(obj, out var dictionary))
            {
                if (dictionary.TryGetValue(name, out var value))
                {
                    return value;
                }
            }

            return null;
        }

        private static ConditionalWeakTable<object /*object*/, ConcurrentDictionary<string /*属性名*/, object /*Value*/>>
            AttachePropertyList { get; } =
            new ConditionalWeakTable<object, ConcurrentDictionary<string, object>>();
    }
}
