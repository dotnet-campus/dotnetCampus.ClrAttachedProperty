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
            "һ�ٸ��߳�����һ�ٸ�Ԫ�أ�ÿ��Ԫ������һ�ٸ����ԣ����Ի�ȡ����ЩԪ�ص����Ե�ֵ".Test(() =>
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

            "��һ���������ö����ͬ���ԣ����Գɹ��õ���ͬ�����Ե�ֵ".Test(() =>
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

            "��һ�����ǿյĶ����������ԣ����Գɹ����˶������ö���֮�������".Test(() =>
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
