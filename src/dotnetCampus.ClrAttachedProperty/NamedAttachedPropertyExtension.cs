namespace dotnetCampus.ClrAttachedProperty
{
    /// <summary>
    /// 给任意对象添加属性的扩展支持
    /// </summary>
    public static class NamedAttachedPropertyExtension
    {
        /// <summary>
        /// 设置属性，存入实际的 属性名 是 ${typeof(<typeparamref name="T"/>).FullName}.{<paramref name="name"/>}
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetNamedAttachedProperty<T>(this object obj, string name, T value)
        {
            var actualName = $"{typeof(T).FullName}.{name}";
            NamedAttachedProperty.SetNamedAttachedProperty(obj, actualName, value);
        }

        /// <summary>
        /// 获取属性，实际尝试获取的是 ${typeof(<typeparamref name="T"/>).FullName}.{<paramref name="name"/>} 属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetNamedAttachedProperty<T>(this object obj, string name)
            => GetNamedAttachedProperty(obj, name, default(T));

        /// <summary>
        /// 获取属性，实际尝试获取的是 ${typeof(<typeparamref name="T"/>).FullName}.{<paramref name="name"/>} 属性
        /// </summary>
        /// <returns></returns>
        public static T GetNamedAttachedProperty<T>(this object obj, string name, T defaultValue)
        {
            var actualName = $"{typeof(T).FullName}.{name}";
            var attachedProperty = NamedAttachedProperty.GetNamedAttachedProperty(obj, actualName);
            if (attachedProperty != null)
            {
                return (T) attachedProperty;
            }

            // 如果是空，需要考虑兼容，尝试获取 name 是否存在
            attachedProperty = NamedAttachedProperty.GetNamedAttachedProperty(obj, name);

            if (attachedProperty is T t)
            {
                return t;
            }

            return defaultValue;
        }
    }
}