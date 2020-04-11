using System;
using Unity;

namespace Everaldo.Cardoso.Araujo.C19BR.Framework.DepedencyInjection
{
    public static class DependencyInjectionFactory
    {
        private static IUnityContainer Container = new UnityContainer();

        public static void AddDependencyInjection(Type from, Type to)
        {
            Container.RegisterType(from, to);
        }

        public static T CreateInstance<T>()
        {
            T implementation = Container.Resolve<T>();
            return implementation;
        } 
    }
}
