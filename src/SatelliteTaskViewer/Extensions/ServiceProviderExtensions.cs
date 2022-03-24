using System;

namespace SatelliteTaskViewer
{
    public static class ServiceProviderExtensions
    {
        public static T GetService<T>(this IServiceProvider serviceProvider)
        {
            var res = serviceProvider.GetService(typeof(T)) ?? throw new Exception();
            return (T)res;
        }

        public static R GetService<T, R>(this IServiceProvider serviceProvider, Func<T, R> transform)
        {
            var res = serviceProvider.GetService(typeof(T)) ?? throw new Exception();
            return transform((T)res);
        }

        public static Lazy<T> GetServiceLazily<T>(this IServiceProvider serviceProvider)
        {
            var res = serviceProvider.GetService(typeof(T)) ?? throw new Exception();
            return new Lazy<T>(() => (T)res);
        }

        public static Lazy<T> GetServiceLazily<T>(this IServiceProvider serviceProvider, Action<T> initialize)
        {
            return new Lazy<T>(() =>
            {
                var res = serviceProvider.GetService(typeof(T)) ?? throw new Exception();
                var result = (T)res;
                initialize(result);
                return result;
            });
        }

        public static Lazy<R> GetServiceLazily<T, R>(this IServiceProvider serviceProvider, Func<T, R> transform)
        {
            var res = serviceProvider.GetService(typeof(T)) ?? throw new Exception();
            return new Lazy<R>(() => transform((T)res));
        }

        public static Lazy<object> GetServiceLazily(this IServiceProvider serviceProvider, Type type)
        {
            var res = serviceProvider.GetService(type) ?? throw new Exception();
            return new Lazy<object>(() => res);
        }
    }
}
