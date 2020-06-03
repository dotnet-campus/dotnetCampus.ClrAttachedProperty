﻿using System;

namespace dotnetCampus.ClrAttachedProperty
{
    /// <summary>
    /// 给任意对象添加属性的扩展支持
    /// </summary>
    public static class AttachedPropertyExtension
    {
        /// <summary>
        /// 设置属性，存入实际的 属性名 是 ${typeof(<typeparamref name="T"/>).FullName}.{<paramref name="name"/>}
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void SetAttachedProperty<T>(this object obj, string name, T value)
        {
            var actualName = $"{typeof(T).FullName}.{name}";
            AttachedProperty.SetAttachedProperty(obj, actualName, value);
        }

        /// <summary>
        /// 获取属性，实际尝试获取的是 ${typeof(<typeparamref name="T"/>).FullName}.{<paramref name="name"/>} 属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T GetAttachedProperty<T>(this object obj, string name)
        {
            var actualName = $"{typeof(T).FullName}.{name}";
            var attachedProperty = AttachedProperty.GetAttachedProperty(obj, actualName);
            if (attachedProperty != null)
            {
                return (T)attachedProperty;
            }

            // 如果是空，需要考虑兼容，尝试获取 name 是否存在
            attachedProperty = AttachedProperty.GetAttachedProperty(obj, name);

            if (attachedProperty is T t)
            {
                return t;
            }

            return default;
        }
    }
}