using System;

namespace Xunit.Di
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class DiSetupTypeAttribute : Attribute
    {
        public string TypeName { get; }
        public string AssemblyName { get; }

        /// <summary>
         /// Initializes an instance of <see cref="T:DiSetupTypeAttribute" />.
         /// </summary>
         /// <param name="typeName">The fully qualified type name of the test setup type
         /// (f.e., 'Function.Tests.Setup')</param>
         /// <param name="assemblyName">The name of the assembly that the setup type
         /// is located in, without file extension (f.e., 'Function.Tests')</param>
        public DiSetupTypeAttribute(string typeName, string assemblyName)
        {
            TypeName = typeName;
            AssemblyName = assemblyName;
        }
    }
}
