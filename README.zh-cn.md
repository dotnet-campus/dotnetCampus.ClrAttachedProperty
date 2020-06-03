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

## 可能存在的问题

1. 多个不同业务设置的值名字相同，但是类型不相同
1. 多个不应该共享值的业务，给出非常通用的属性名，导致两个业务非预期共享属性值