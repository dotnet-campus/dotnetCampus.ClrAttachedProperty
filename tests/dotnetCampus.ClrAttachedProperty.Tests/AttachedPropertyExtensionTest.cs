using System.Collections.Generic;
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
            "���ÿɿ�ֵ���ͣ�ֵ�ǿգ����Ի�ȡ���ն���".Test(() =>
            {
                // Arrange
                var obj = new object();
                int? ageProperty = null;
                var name = "age";

                // Action
                obj.SetAttachedProperty(name, ageProperty);

                // Assert
                Assert.AreEqual(ageProperty, obj.GetAttachedProperty<int?>(name));
            });

            "���ÿɿ�ֵ���ͣ�ֵ���ǿգ����Ի�ȡ����Զ���".Test(() =>
            {
                // Arrange
                var obj = new object();
                int? ageProperty = 10;
                var name = "age";

                // Action
                obj.SetAttachedProperty(name, ageProperty);

                // Assert
                Assert.AreEqual(ageProperty, obj.GetAttachedProperty<int?>(name));
            });

            "������������ͬ���������Ͳ���ͬ�����ԣ����Զ�����ȡ�Ͷ�ȡ".Test(() =>
            {
                // Arrange
                var obj = new object();
                int ageProperty = 10;
                var name = "age";
                var fooProperty = new List<int>();

                // Action
                obj.SetAttachedProperty(name, ageProperty);
                obj.SetAttachedProperty(name, fooProperty);

                // Assert
                Assert.AreEqual(ageProperty, obj.GetAttachedProperty<int>(name));
                Assert.AreEqual(fooProperty, obj.GetAttachedProperty<List<int>>(name));
            });

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