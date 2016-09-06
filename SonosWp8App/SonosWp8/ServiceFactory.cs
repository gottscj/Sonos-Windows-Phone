using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SonosWp8
{
    public class ServiceFactory
    {
        private static readonly IDictionary<Type, Func<object>> Registrations = new Dictionary<Type, Func<object>>();

        public static void Register<T, TR>(Func<TR> creator) where TR : class, T
        {
            Registrations[typeof (T)] = creator;
        }

        public static T Get<T>()
        {
            Func<object> creator;
            if (Registrations.TryGetValue(typeof (T), out creator)) 
                return (T) creator();

            Debug.WriteLine("ServiceFactory: No Type defined for: " + typeof (T));
            return default(T);
        }
    }
}