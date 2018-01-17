using SimpleInjector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Arcesoft.TicTacToe
{
    //public static class ContainerExtensions
    //{
    //    public static void LoadAndRegisterAllReferencedAssembliesInAppDomain(this Container container, string assemblyNamesStartingWith = null)
    //    {
    //        var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
    //        var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

    //        var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, $"{assemblyNamesStartingWith}*.dll");
    //        var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();
    //        toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

    //        foreach (var assembly in loadedAssemblies)
    //        {
    //            assembly.
    //        }
    //    }
    //}
}
