# dotnetCampus.ClrAttachedProperty

| Build | NuGet |
|--|--|
|![](https://github.com/dotnet-campus/dotnetCampus.ClrAttachedProperty/workflows/.NET%20Core/badge.svg)|[![](https://img.shields.io/nuget/v/dotnetCampus.ClrAttachedProperty.svg)](https://www.nuget.org/packages/dotnetCampus.ClrAttachedProperty)|

This library supports adding any property to any objects.


## Install NuGet package

Two different libraries are provided for installation.

### Install the traditionary NuGet Dll library

.NET CLI:

```
dotnet add package dotnetCampus.ClrAttachedProperty --version 1.0.0
```

PackageReference:

```xml
<PackageReference Include="dotnetCampus.ClrAttachedProperty" Version="1.0.0" />
```

### Install the [SourceYard](https://github.com/dotnet-campus/SourceYard) NuGet source code

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

## Usage

We can attach properies to any object by extension method

```csharp
using dotnetCampus.ClrAttachedProperty;

        public void Foo(object obj, string propertyName, object objectValue)
        {
            obj.SetAttachedProperty(propertyName, objectValue);

            var value = obj.GetAttachedProperty(propertyName);
        }
```

## Contributing

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://github.com/dotnet-campus/dotnetCampus.ClrAttachedProperty/pulls)

If you would like to contribute, feel free to create a [Pull Request](https://github.com/dotnet-campus/dotnetCampus.ClrAttachedProperty/pulls), or give us [Bug Report](https://github.com/dotnet-campus/dotnetCampus.ClrAttachedProperty/issues/new).