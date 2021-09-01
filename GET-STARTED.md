# Configure xunit dependency injection
## How to use xunit.di 
1. Install the xunit.di nuget package
2. Create a ***Setup.cs*** class, (optional) and inherits the [xunit.di.Setup.cs](Xunit.Di/Setup.cs)
3. Configure dependencies in the ***Setup.cs*** class.

### Test Project Properties
```xml
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <AssemblyName>project.tests</AssemblyName>
    <RootNamespace>project.tests</RootNamespace>
    <SetupNamespace>$(RootNamespace)</SetupNamespace>
    <SetupAssembly>$(AssemblyName)</SetupAssembly>
    <EnableXunitDi>true</EnableXunitDi>
    ...
  </PropertyGroup>
  ...
</Project>

```
All these properties are optional, when not present, the default values would be used.
Configuration|Default |Values
-------------|--------|------
AssemblyName||The name of the final output assembly after the project is built.
RootNamespace||The root namespace to use when you name an embedded resource. This namespace is part of the embedded resource manifest name. 
SetupNamespace|$(RootNamespace)|The namespace where you should find the ***Setup.cs*** class, set this value when using the ***Setup.cs*** from a different library project.
SetupAssembly|$(AssemblyName)|The assembly where you should find the ***Setup.cs*** class, set this value when using the ***Setup.cs*** from a different library project.
EnableXunitDi|true|Set it to false when you need to temporarily disable the xunit.di

## Sample usage
Find the sample usages from this test project [Xunit.Di.Tests](Xunit.Di.Tests)
