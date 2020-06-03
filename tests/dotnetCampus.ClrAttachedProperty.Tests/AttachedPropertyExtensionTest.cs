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
            "ʹ�÷��ΰ汾�����ø������ԣ�����ʹ�÷Ƿ��ΰ汾��ȡ����ͬ�Ķ���".Test(() =>
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

            "ʹ�÷Ƿ��ΰ汾�����ø������ԣ�����ʹ�÷��ΰ汾��ȡ����ͬ�Ķ���".Test(() =>
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

            "����ʹ�÷��ΰ汾���úͻ�ȡ����ͬ����".Test(() =>
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