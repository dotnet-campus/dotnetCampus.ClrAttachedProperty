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
            "���ÿɿ�ֵ���ͣ�ֵ�ǿգ����Ի�ȡ���ն���".Test(() =>
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

            "���ÿɿ�ֵ���ͣ�ֵ���ǿգ����Ի�ȡ����Զ���".Test(() =>
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

            "������������ͬ���������Ͳ���ͬ�����ԣ����Զ�����ȡ�Ͷ�ȡ".Test(() =>
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

            "ʹ�÷��ΰ汾�����ø������ԣ�����ʹ�÷Ƿ��ΰ汾��ȡ����ͬ�Ķ���".Test(() =>
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

            "ʹ�÷Ƿ��ΰ汾�����ø������ԣ�����ʹ�÷��ΰ汾��ȡ����ͬ�Ķ���".Test(() =>
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

            "����ʹ�÷��ΰ汾���úͻ�ȡ����ͬ����".Test(() =>
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