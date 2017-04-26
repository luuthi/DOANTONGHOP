using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;

namespace nongsanxanh.App_Start
{
    public class IOConfig
    {
        public static void Register(HttpConfiguration configuration)
        {
            var currentPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            //Load all dlls from bin folder in every cases
            PreLoad(currentPath);

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            //Register all services
            var services = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(c => c.FullName.StartsWith("Bussiness"));
            builder.RegisterAssemblyTypes(services).Where(t => t.Name.EndsWith("Service")).AsImplementedInterfaces().InstancePerRequest();

            //We build the container.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static void PreLoad(string p)
        {
            //all try/catch blocks are elided for brevity
            string[] files = null;

            files = Directory.GetFiles(p, "*.dll", SearchOption.AllDirectories);

            AssemblyName a = null;
            foreach (var s in files)
            {
                a = AssemblyName.GetAssemblyName(s);
                if (!AppDomain.CurrentDomain.GetAssemblies().Any(
                    assembly => AssemblyName.ReferenceMatchesDefinition(
                        assembly.GetName(), a)))
                    Assembly.LoadFrom(s);
            }
        }
    }
}