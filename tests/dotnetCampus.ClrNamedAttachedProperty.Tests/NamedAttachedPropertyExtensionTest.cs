using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.ClrNamedAttachedProperty.Tests
{
    [TestClass]
    public class NamedAttachedPropertyExtensionTest
    {
        [ContractTestCase]
        public void SetGetProperty()
        {
            "设置可空值类型，值是空，可以获取到空对象".Test(() =>
            {
                // Arrange
                var obj = new object();
                int? ageProperty = null;
                var name = "age";

                // Action
                obj.SetNamedAttachedProperty(name, ageProperty);

                // Assert
                Assert.AreEqual(ageProperty, obj.GetNamedAttachedProperty<int?>(name));
            });

            "设置可空值类型，值不是空，可以获取到相对对象".Test(() =>
            {
                // Arrange
                var obj = new object();
                int? ageProperty = 10;
                var name = "age";

                // Action
                obj.SetNamedAttachedProperty(name, ageProperty);

                // Assert
                Assert.AreEqual(ageProperty, obj.GetNamedAttachedProperty<int?>(name));
            });

            "两个属性名相同，但是类型不相同的属性，可以独立存取和读取".Test(() =>
            {
                // Arrange
                var obj = new object();
                int ageProperty = 10;
                var name = "age";
                var fooProperty = new List<int>();

                // Action
                obj.SetNamedAttachedProperty(name, ageProperty);
                obj.SetNamedAttachedProperty(name, fooProperty);

                // Assert
                Assert.AreEqual(ageProperty, obj.GetNamedAttachedProperty<int>(name));
                Assert.AreEqual(fooProperty, obj.GetNamedAttachedProperty<List<int>>(name));
            });

            "使用泛形版本的设置附加属性，可以使用非泛形版本读取到相同的对象".Test(() =>
            {
                // Arrange
                var obj = new object();
                int property = 10;
                var name = "age";
                var actualName = $"{typeof(int).FullName}.{name}";

                // Action
                obj.SetNamedAttachedProperty(name, property);

                // Assert
                Assert.AreEqual(property, obj.GetNamedAttachedProperty(actualName));
            });

            "使用非泛形版本的设置附加属性，可以使用泛形版本读取到相同的对象".Test(() =>
            {
                // Arrange
                var obj = new object();
                int property = 10;
                var name = "age";

                // Action
                obj.SetNamedAttachedProperty(name, (object)property);

                // Assert
                Assert.AreEqual(property, obj.GetNamedAttachedProperty<int>(name));
            });

            "可以使用泛形版本设置和获取到相同对象".Test(() =>
            {
                // Arrange
                var obj = new object();
                int property = 10;
                var name = "age";

                // Action
                obj.SetNamedAttachedProperty(name, property);

                // Assert
                Assert.AreEqual(property, obj.GetNamedAttachedProperty<int>(name));
            });
        }
    }
}