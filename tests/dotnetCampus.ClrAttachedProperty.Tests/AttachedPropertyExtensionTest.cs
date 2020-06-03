using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.ClrAttachedProperty.Tests
{
    [TestClass]
    public class AttachedPropertyExtensionTest
    {
        [ContractTestCase]
        public void SetGetProperty()
        {
            "使用泛形版本的设置附加属性，可以使用非泛形版本读取到相同的对象".Test(() =>
            {
                // Arrange
                var obj = new object();
                int property = 10;
                var name = "age";
                var actualName = $"{typeof(int).FullName}.{name}";

                // Action
                obj.SetAttachedProperty(name, property);

                // Assert
                Assert.AreEqual(property, obj.GetAttachedProperty(actualName));
            });

            "使用非泛形版本的设置附加属性，可以使用泛形版本读取到相同的对象".Test(() =>
            {
                // Arrange
                var obj = new object();
                int property = 10;
                var name = "age";

                // Action
                obj.SetAttachedProperty(name, (object)property);

                // Assert
                Assert.AreEqual(property, obj.GetAttachedProperty<int>(name));
            });

            "可以使用泛形版本设置和获取到相同对象".Test(() =>
            {
                // Arrange
                var obj = new object();
                int property = 10;
                var name = "age";

                // Action
                obj.SetAttachedProperty(name, property);

                // Assert
                Assert.AreEqual(property, obj.GetAttachedProperty<int>(name));
            });
        }
    }
}