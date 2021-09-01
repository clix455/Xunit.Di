# Xunit.Di
Xunit test framework can be extended to support dependency injection, which allows us to achieve Inversion of Control (IoC) between test classes and their dependencies. 

We use Xunit dependency injection addresses these problems through:
- Configure supporting infrastructure, such as database, storage
- Injection of services into the constructor of the test classes as well as the service classes. The framework takes on the responsibility of creating instances of dependency and disposing of them when test scopes end.

## What is xunit.di?
By default, the xunit framework implementation XunitTestFramework does not have the knowledge of your Service provider or their dependent services; thus we create a derived XunitTestFramework class, namely DiXunitTestFramework, and build the service provider into the xunit test framework. 

Find more information from the [Get Started](GET-STARTED.md) page.

## How can I contribute?

We welcome contributions! [Contributing](CONTRIBUTING.md) explains what kinds of changes we welcome.

## Useful Links
* [About xUnit.net](https://xunit.net/)
* [Dependency injection in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-5.0)