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
            "传入默认值的附加属性，对于不同的对象，不同类的不同对象，在没有设置属性时，返回相同的默认值".Test(() =>
            {
                // Arrange
                var f1 = new F1();
                var f2 = new F1();

                var foo1 = new F2();
                var defaultValue = new List<int>();

                // Action
                var attachedProperty = new AttachedProperty<List<int>>(defaultValue);

                // Assert
                Assert.AreSame(defaultValue, attachedProperty.GetValue(f1));
                Assert.AreSame(defaultValue, attachedProperty.GetValue(f2));
                Assert.AreSame(defaultValue, attachedProperty.GetValue(foo1));
            });

            "传入默认值，如果有设置对应属性，不返回默认值".Test(() =>
            {
                // Arrange
                var f1 = new F1();
                var defaultValue = new List<int>();
                var attachedProperty = new AttachedProperty<List<int>>(defaultValue);

                // Action
                attachedProperty.SetValue(f1, new List<int>());

                // Assert
                Assert.AreNotSame(defaultValue, attachedProperty.GetValue(f1));
            });

            "传入默认值，如果没有设置对应属性，返回默认值".Test(() =>
            {
                // Arrange
                var f1 = new F1();
                var defaultValue = new List<int>();

                // Action
                var attachedProperty = new AttachedProperty<List<int>>(defaultValue);

                // Assert
                Assert.AreSame(defaultValue, attachedProperty.GetValue(f1));
            });

            "业务方可以私有化附加属性对象，此时支持仅在业务内部使用附加属性".Test(() =>
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

            "通过某个业务类的附加属性，可以设置或获取附加属性".Test(() =>
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

            "两个不同的业务，使用不相同的附加属性对象，设置和获取的属性值不相同".Test(() =>
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