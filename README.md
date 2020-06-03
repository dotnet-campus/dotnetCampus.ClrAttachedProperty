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

## Contributing

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://github.com/dotnet-campus/dotnetCampus.ClrNamedNamedAttachedProperty/pulls)

If you would like to contribute, feel free to create a [Pull Request](https://github.com/dotnet-campus/dotnetCampus.ClrNamedNamedAttachedProperty/pulls), or give us [Bug Report](https://github.com/dotnet-campus/dotnetCampus.ClrNamedNamedAttachedProperty/issues/new).