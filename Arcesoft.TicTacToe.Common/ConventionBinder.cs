using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace Arcesoft.TicTacToe
{
    /// <summary>
    /// Convention based binder that uses convention to register all types.
    /// </summary>
    /// <typeparam name="TAssemblyType"></typeparam>
    public class ConventionBinder<TAssemblyType> : IBinder
    {
        public void BindDependencies(Container container)
        {
            var assembly = typeof(TAssemblyType).Assembly;

            var interfaces = assembly
                .GetExportedTypes()
                .Where(a => a.GetInterfaces().Any())
                .Select(a => new { Service = a.GetInterfaces().Single(), Implementation = a })
                .ToList();

            interfaces.ForEach(a => container.Register(a.Service, a.Implementation, Lifestyle.Transient));
        }
    }
}
