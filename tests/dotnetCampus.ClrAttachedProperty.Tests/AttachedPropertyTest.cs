using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.ClrAttachedProperty.Tests
{
    [TestClass]
    public class AttachedPropertyTest
    {
        [ContractTestCase]
        public void SetGetProperty()
        {
            "一百个线程设置一百个元素，每个元素设置一百个属性，可以获取对这些元素的属性的值".Test(() =>
            {
                // Arrange
                var objectList = new object[100];
                for (int i = 0; i < 100; i++)
                {
                    objectList[i] = new object();
                }

                var propertyList = new object[100, 100];
                for (int i = 0; i < 100; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        propertyList[i, j] = new object();
                    }
                }

                // Action
                var taskList = new List<Task>(100);
                for (int i = 0; i < 100; i++)
                {
                    var n = i;
                    var task = Task.Run(() =>
                    {
                        for (int j = 0; j < 100; j++)
                        {
                            var obj = objectList[j];
                            var property = propertyList[j, n];
                            obj.SetAttachedProperty(n.ToString(), property);
                        }
                    });

                    taskList.Add(task);
                }

                Task.WaitAll(taskList.ToArray());

                // Assert
                taskList.Clear();

                for (int i = 0; i < 100; i++)
                {
                    var n = i;

                    var task = Task.Run(() =>
                    {
                        for (int j = 0; j < 100; j++)
                        {
                            Assert.AreSame(propertyList[n, j], objectList[n].GetAttachedProperty(j.ToString()));
                        }
                    });

                    taskList.Add(task);
                }

                Task.WaitAll(taskList.ToArray());
            });

            "给一个对象设置多个不同属性，可以成功拿到不同的属性的值".Test(() =>
            {
                // Arrange
                var obj = new object();
                var propertyValue1 = new object();
                var propertyName1 = "Foo";

                var propertyValue2 = new object();
                var propertyName2 = "Foo2";

                // Action
                obj.SetAttachedProperty(propertyName1, propertyValue1);
                obj.SetAttachedProperty(propertyName2, propertyValue2);

                // Assert
                Assert.AreSame(propertyValue1, obj.GetAttachedProperty(propertyName1));
                Assert.AreSame(propertyValue2, obj.GetAttachedProperty(propertyName2));
            });

            "给一个不是空的对象设置属性，可以成功给此对象设置定义之外的属性".Test(() =>
            {
                // Arrange
                var obj = new object();
                var propertyValue = new object();
                var propertyName = "Foo";
                // Action
                obj.SetAttachedProperty(propertyName, propertyValue);

                // Assert
                Assert.AreSame(propertyValue, obj.GetAttachedProperty(propertyName));
            });
        }
    }
}
