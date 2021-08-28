using System;
using System.Linq;
using System.Reflection;

namespace Xunit.Di
{
    public static class DiLoader
    {
        public static IServiceProvider? GetServiceProvider(AssemblyName assemblyName)
        {
            var thisAssembly = Assembly.Load(assemblyName);
            var setupTypeAtrAttribute = thisAssembly.GetCustomAttribute<DiSetupTypeAttribute>();

            if (setupTypeAtrAttribute == null)
                throw new InvalidOperationException("Setup type not configured in your project.");

            var setupTypeAssembly = Assembly.Load(setupTypeAtrAttribute.AssemblyName);
            if (setupTypeAssembly == null)
                throw new InvalidOperationException($"Could not load assembly '{setupTypeAtrAttribute.AssemblyName}'");

            var setupType = setupTypeAssembly.GetType(setupTypeAtrAttribute.TypeName) 
                       ?? throw new InvalidOperationException($"Can't load type {setupTypeAtrAttribute.TypeName} in '{setupTypeAtrAttribute.AssemblyName}'");

            var property = setupType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(p => (p.PropertyType.Name).Equals(nameof(IServiceProvider)));

            var setup = Activator.CreateInstance(setupType);
            return property?.GetValue(setup) as IServiceProvider;
        }
    }
}
