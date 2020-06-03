# dotnetCampus.ClrNamedNamedAttachedProperty

| Build | NuGet |
|--|--|
|![](https://github.com/dotnet-campus/dotnetCampus.ClrNamedNamedAttachedProperty/workflows/.NET%20Core/badge.svg)|[![](https://img.shields.io/nuget/v/dotnetCampus.ClrNamedNamedAttachedProperty.svg)](https://www.nuget.org/packages/dotnetCampus.ClrNamedNamedAttachedProperty)|

This library supports adding any property to any objects.


## Install NuGet package

Two different libraries are provided for installation.

### Install the traditionary NuGet Dll library

.NET CLI:

```
dotnet add package dotnetCampus.ClrNamedNamedAttachedProperty --version 1.0.0
```

PackageReference:

```xml
<PackageReference Include="dotnetCampus.ClrNamedNamedAttachedProperty" Version="1.0.0" />
```

### Install the [SourceYard](https://github.com/dotnet-campus/SourceYard) NuGet source code

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

## Usage

We can attach properies to any object by extension method

```csharp
using dotnetCampus.ClrNamedNamedAttachedProperty;

        public void Foo(object obj, string propertyName, object objectValue)
        {
            obj.SetNamedAttachedProperty(propertyName, objectValue);

            var value = obj.GetNamedAttachedProperty(propertyName);
        }
```

The `propertyName` will be shared in global. You need to be careful about all unexpected object sharing caused by unexpected naming conflicts.

## Advanced usage

We can define the AttachedProperty property to define a property that can be attached to object.

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

And you can set the AttachedProperty as private that you can only use it in private business code.

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

As the code show, you can find we use two F2 to attach two list to the F1 object. And the two F2 use the independent property value.

## Contributing

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://github.com/dotnet-campus/dotnetCampus.ClrNamedNamedAttachedProperty/pulls)

If you would like to contribute, feel free to create a [Pull Request](https://github.com/dotnet-campus/dotnetCampus.ClrNamedNamedAttachedProperty/pulls), or give us [Bug Report](https://github.com/dotnet-campus/dotnetCampus.ClrNamedNamedAttachedProperty/issues/new).