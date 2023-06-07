using Shared.Abstractions.Modules;
using System.Reflection;

namespace Shared.Infrastructure.Modules
{
    public static class ModuleScanner
    {
        public static List<Assembly> ScanForModules()
        {
            var assemblies = new List<Assembly>();

            // Retrieve all loaded assemblies
            var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            // Iterate through the loaded assemblies
            foreach (var assembly in loadedAssemblies)
            {
                // Check if the assembly contains any types that implement the IModule interface
                var moduleTypes = assembly.GetTypes().Where(t => typeof(IModule).IsAssignableFrom(t));

                // If the assembly contains modules, add it to the list
                if (moduleTypes.Any())
                {
                    assemblies.Add(assembly);
                }
            }

            return assemblies;
        }
    }
}
