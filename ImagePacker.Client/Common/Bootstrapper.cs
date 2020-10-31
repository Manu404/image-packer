using System;
using System.Collections.Generic;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using ImagePacker.Client.Common.Interface;
using ImagePacker.Client.ViewModel;

namespace ImagePacker.Client
{
    public static class Bootstrapper
    {
        public static IWindsorContainer CreateDefaultContainer()
        {
            var container = new WindsorContainer();
            var kernel = container.Kernel;
            container.AddFacility<TypedFactoryFacility>(); // generate factory
            kernel.Resolver.AddSubResolver(new CollectionResolver(kernel)); // recursive resolution
            return container;
        }

        public static IEnumerable<Type> GetDefaultTypes()
        {
            return new List<Type>
            {
                typeof(IFactory<>),
                typeof(IFactory<,>),
                typeof(IFileDialogProvider),
                typeof(IViewModel)
            };
        }

        public static IWindsorContainer RegisterStatic(this IWindsorContainer container)
        {
            //container.Register(Component.For<IMessenger>().ImplementedBy<Messenger>());
            return container;
        }

        public static IWindsorContainer RegisterTypesFrom(this IWindsorContainer container, FromAssemblyDescriptor sourceAssembly, IEnumerable<Type> typesToRegister)
        {
            foreach (var type in typesToRegister)
                container.Register(sourceAssembly.BasedOn(type).WithServiceAllInterfaces());
            return container;
        }

        public static IWindsorContainer RegisterDefaultTypesFrom(this IWindsorContainer container, FromAssemblyDescriptor sourceAssembly)
        {
            return container.RegisterTypesFrom(sourceAssembly, GetDefaultTypes());
        }

        public static IWindsorContainer GetDefaultContainer()
        {
            return CreateDefaultContainer()
                .RegisterDefaultTypesFrom(Classes.FromAssemblyInThisApplication())
                .RegisterStatic();
        }
    }
}
