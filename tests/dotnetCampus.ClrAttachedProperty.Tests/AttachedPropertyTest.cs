using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTest.Extensions.Contracts;

namespace dotnetCampus.ClrAttachedProperty.Tests
{
    [TestClass]
    public class AttachedPropertyTest
    {
        [ContractTestCase]
        public void SetGetProperty()
        {
            "ҵ�񷽿���˽�л��������Զ��󣬴�ʱ֧�ֽ���ҵ���ڲ�ʹ�ø�������".Test(() =>
            {
                // Arrange
                var f1 = new F1();
                var foo1List = new List<int>();
                var foo2List = new List<int>();
                var foo1 = new F2();
                var foo2 = new F2();

                // Action
                foo1.SetFoo(f1, foo1List);
                foo2.SetFoo(f1, foo2List);

                // Assert
                Assert.AreSame(foo1List, foo1.GetFoo(f1));
                Assert.AreSame(foo2List, foo2.GetFoo(f1));
            });

            "ͨ��ĳ��ҵ����ĸ������ԣ��������û��ȡ��������".Test(() =>
            {
                // Arrange
                var obj = new object();
                var fooList = new List<int>();
                var age = 10;

                // Action
                F1.AttachedFooProperty.SetValue(obj, fooList);
                F1.AgeProperty.SetValue(obj, age);

                // Assert
                Assert.AreSame(fooList, F1.AttachedFooProperty.GetValue(obj));
                Assert.AreEqual(age, F1.AgeProperty.GetValue(obj));
            });

            "������ͬ��ҵ��ʹ�ò���ͬ�ĸ������Զ������úͻ�ȡ������ֵ����ͬ".Test(() =>
            {
                // Arrange
                var obj = new object();
                var age1 = 1000;
                var age2 = 200;

                // Action
                F1.AgeProperty.SetValue(obj, age1);
                F2.AgeProperty.SetValue(obj, age2);

                // Assert
                Assert.AreEqual(age1, F1.AgeProperty.GetValue(obj));
                Assert.AreEqual(age2, F2.AgeProperty.GetValue(obj));
            });
        }

        class F1
        {
            public static readonly AttachedProperty<List<int>> AttachedFooProperty = new AttachedProperty<List<int>>();

            public static readonly AttachedProperty<int> AgeProperty = new AttachedProperty<int>();
        }

        class F2
        {
            public void SetFoo(F1 obj, List<int> fooList)
            {
                AttachedFooProperty.SetValue(obj, fooList);
            }

            public List<int> GetFoo(F1 obj)
            {
                return AttachedFooProperty.GetValue(obj);
            }

            private AttachedProperty<List<int>> AttachedFooProperty { get; } = new AttachedProperty<List<int>>();

            public static readonly AttachedProperty<int> AgeProperty = new AttachedProperty<int>();
        }
    }
}