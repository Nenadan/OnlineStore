using System.Reflection;
using System.Runtime.CompilerServices;

namespace OnlineStore.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddServicesFromAssembly(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var typesWithInterfaces = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Implementation = t,
                    Service = t.GetInterfaces().FirstOrDefault()
                })
                .Where(t => t.Service != null && !typeof(IAsyncStateMachine).IsAssignableFrom(t.Implementation));

            foreach (var type in typesWithInterfaces)
            {
                if(!type.Service.IsGenericTypeDefinition)
                {
                    services.AddScoped(type.Service, type.Implementation);
                }
                else
                {
                    services.AddScoped(type.Service.GetGenericTypeDefinition(), type.Implementation.GetGenericTypeDefinition());
                }

            }
        }
    }
}
