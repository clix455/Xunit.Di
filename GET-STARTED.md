# Configure xunit dependency injection
## How to use xunit.di 
1. Install the xunit.di nuget package
2. Create a ***Setup.cs*** class, (optional) and inherits the [Xunit.Di.Setup.cs](Xunit.Di/Setup.cs)
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

| Configuration  | Default                    | Values                                                                                                                                       |
| -------------- | -------------------------- | -------------------------------------------------------------------------------------------------------------------------------------------- |
| AssemblyName   | test project assembly name | The name of the final output assembly after the project is built.                                                                            |
| RootNamespace  | test project namespace     | The root namespace to use when you name an embedded resource. This namespace is part of the embedded resource manifest name.                 |
| SetupNamespace | $(RootNamespace)           | The namespace where you should find the ***Setup.cs*** class, set this value when using the ***Setup.cs*** from a different library project. |
| SetupAssembly  | $(AssemblyName)            | The assembly where you should find the ***Setup.cs*** class, set this value when using the ***Setup.cs*** from a different library project.  |
| EnableXunitDi  | true                       | Set it to false when you need to temporarily disable the xunit.di                                                                            |
  
    
### Configure the ***Setup.cs*** file
1. Create a ***Setup.cs*** class in your test project. To leverage the ***setup*** provided by the package, you can optionally inherit the [Xunit.Di.Setup.cs](Xunit.Di/Setup.cs) class.
```c#
    public class Setup : Di.Setup
```
2. Configure the dependency services
```C#
/// When inherits the Xunit.Di.Setup.cs class
        protected override void Configure()
        {
            ConfigureAppConfiguration((hostingContext, config) =>
            {
                bool reloadOnChange =
                    hostingContext.Configuration.GetValue("hostBuilder:reloadConfigOnChange", true);

                if (hostingContext.HostingEnvironment.IsDevelopment())
                    config.AddUserSecrets<Setup>(true, reloadOnChange);
            });

            ConfigureServices((context, services) =>
            {
                services.AddSingleton<TextReaderService>();
            });
        }
 /// or otherwise configure the setup from scratch, and it needs to have a public property of type IServiceProvider. Refer to the Xunit.Di.Setup.cs class as a template.
```
3. Consume the registered service from your test class.
```C#
        private readonly TextReaderService _textReader;

        public ServiceDependencyTests(TextReaderService textReader)
        {
            this._textReader = textReader;
        }

        [Fact]
        public async Task Dependency_Instantiated_and_CanRead()
        {
            var value = await _textReader.Reader.ReadToEndAsync();
            Assert.NotNull(value);
            Assert.NotEmpty(value);
        }
```
*note:* The TextReaderService is a sample service class defined in the test project.

## Sample usage
Find the sample usages from this test project [Xunit.Di.Tests](Xunit.Di.Tests)
