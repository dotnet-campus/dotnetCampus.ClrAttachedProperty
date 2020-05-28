# dotnetCampus.ClrAttachedProperty

| Build | NuGet |
|--|--|
|![](https://github.com/dotnet-campus/dotnetCampus.ClrAttachedProperty/workflows/.NET%20Core/badge.svg)|[![](https://img.shields.io/nuget/v/dotnetCampus.ClrAttachedProperty.svg)](https://www.nuget.org/packages/dotnetCampus.ClrAttachedProperty)|

给任意对象附加上任意额外的属性

## 安装 NuGet 包

这个库提供了两个不同的包可以给大家安装。其中一个包是传统的 Dll 引用包。另一个包是使用 [SourceYard](https://github.com/dotnet-campus/SourceYard) 打出来的源代码包，源代码包安装之后将会引用源代码

### 安装传统 NuGet Dll 库

.NET CLI:

```
dotnet add package dotnetCampus.ClrAttachedProperty --version 1.0.0
```

PackageReference:

```xml
<PackageReference Include="dotnetCampus.ClrAttachedProperty" Version="1.0.0" />
```

### 安装源代码包


.NET CLI:

```
dotnet add package dotnetCampus.ClrAttachedProperty.Source --version 1.0.0
```

PackageReference:

```xml
<PackageReference Include="dotnetCampus.ClrAttachedProperty.Source" Version="1.0.0">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>
```

## 使用方法

可以给任意的对象附加任意类型的属性，同时可以从对象里面获取附加的属性

```csharp
using dotnetCampus.ClrAttachedProperty;

        public void Foo(object obj, string propertyName, object objectValue)
        {
            obj.SetAttachedProperty(propertyName, objectValue);

            var value = obj.GetAttachedProperty(propertyName);
        }
```

