# dotnetCampus.ClrNamedNamedAttachedProperty

| Build | NuGet |
|--|--|
|![](https://github.com/dotnet-campus/dotnetCampus.ClrNamedNamedAttachedProperty/workflows/.NET%20Core/badge.svg)|[![](https://img.shields.io/nuget/v/dotnetCampus.ClrNamedNamedAttachedProperty.svg)](https://www.nuget.org/packages/dotnetCampus.ClrNamedNamedAttachedProperty)|

给任意对象附加上任意额外的属性

## 安装 NuGet 包

这个库提供了两个不同的包可以给大家安装。其中一个包是传统的 Dll 引用包。另一个包是使用 [SourceYard](https://github.com/dotnet-campus/SourceYard) 打出来的源代码包，源代码包安装之后将会引用源代码

### 安装传统 NuGet Dll 库

.NET CLI:

```
dotnet add package dotnetCampus.ClrNamedNamedAttachedProperty --version 1.0.0
```

PackageReference:

```xml
<PackageReference Include="dotnetCampus.ClrNamedNamedAttachedProperty" Version="1.0.0" />
```

### 安装源代码包


.NET CLI:

```
dotnet add package dotnetCampus.ClrNamedNamedAttachedProperty.Source --version 1.0.0
```

PackageReference:

```xml
<PackageReference Include="dotnetCampus.ClrNamedNamedAttachedProperty.Source" Version="1.0.0">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>
```

## 使用方法

可以给任意的对象附加任意类型的属性，同时可以从对象里面获取附加的属性

```csharp
using dotnetCampus.ClrNamedNamedAttachedProperty;

        public void Foo(object obj, string propertyName, object objectValue)
        {
            obj.SetNamedAttachedProperty(propertyName, objectValue);

            var value = obj.GetNamedAttachedProperty(propertyName);
        }
```

传入的 `propertyName` 是全局共享的，意味着在两个无关的业务模块，可以使用相同的属性名从相同的对象读写相同的属性值

此用法可能存在的问题

1. 多个不同业务设置的值名字相同，但是类型不相同
1. 多个不应该共享值的业务，给出非常通用的属性名，导致两个业务非预期共享属性值

## 进阶方法

咱可以定义 AttachedProperty 作为本地的静态属性或成员字段，甚至是局部变量。通过 AttachedProperty 可以表示一个附加属性的定义，只有通过相同的 AttachedProperty 对象，才能设置和获取相同的附加属性值

使用方法如下面代码

```csharp
using dotnetCampus.ClrNamedNamedAttachedProperty;

        class F1
        {
            public static readonly AttachedProperty<List<int>> AttachedFooProperty = new AttachedProperty<List<int>>();

            public static readonly AttachedProperty<int> AgeProperty = new AttachedProperty<int>();
        }

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
```

上面代码在 F1 类中创建了两个静态的只读字段，表示两个附加属性，通过不同的 AttachedProperty 对象可以设置相同对象的两个不同的附加属性

如果期望仅在某个业务中使用，可以将此 AttachedProperty 对象可访问标记为私有。更进一步的是将 AttachedProperty 定义为成员字段，此时利用不同的 AttachedProperty 对象对应不同的附加属性的功能，可以使用相同的类型定义使用多个不同的附加属性，如下面代码

```csharp
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
        }

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
```

上面代码创建了两个 F2 对象，而这个对象的定义是通过将 AttachedFooProperty 私有化进行二次封装，这两个 F2 对象尽管调用了相同的 SetFoo 方法定义，但因为 AttachedFooProperty 属性是成员属性，两个 F2 拥有不同的 AttachedFooProperty 的值。此时两个 F2 调用 SetFoo 时设置的附加属性为两个不同的属性

此做法和 WPF 保持一致

优点如下：

- 业务级别隔离，不同的业务访问附加属性，不会因为仅命名原因造成非预期的属性覆盖
- 支持仅对象内部访问的封装，只需将 AttachedProperty 设置为私有
- 支持对象单独定制附加属性，只需将 AttachedProperty 定义为成员字段或属性

可能存在的问题：

- 如定义为局部变量的 AttachedProperty 附加的属性值的回收是跟随附加的对象被回收而回收，而非跟随 AttachedProperty 变量的回收而被回收
- 无法做到两个互不依赖的业务之间传递附加属性

