using System;

namespace dotnetCampus.ClrAttachedProperty
{
    /// <summary>
    /// 可以附加到任意对象的附加属性
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AttachedProperty<T>
    {
        /// <summary>
        /// 创建可以附加到任意对象的附加属性
        /// </summary>
        public AttachedProperty()
        {
            InnerPropertyKey = Guid.NewGuid().ToString("N");
        }

        public AttachedProperty(T defaultValue) : this()
        {
            _defaultValue = defaultValue;
        }

        /// <summary>
        /// 给 <paramref name="attachedObject"/> 附加 <paramref name="value"/> 值
        /// </summary>
        /// <param name="attachedObject"></param>
        /// <param name="value"></param>
        public void SetValue(object attachedObject, T value) =>
            NamedAttachedPropertyExtension.SetNamedAttachedProperty(attachedObject, InnerPropertyKey, value);

        /// <summary>
        /// 获取附加到 <paramref name="attachedObject"/> 的属性值
        /// </summary>
        /// <param name="attachedObject"></param>
        /// <returns></returns>
        public T GetValue(object attachedObject) =>
            NamedAttachedPropertyExtension.GetNamedAttachedProperty<T>(attachedObject, InnerPropertyKey, _defaultValue);

        private string InnerPropertyKey { get; }

        private readonly T _defaultValue = default;
    }
}