using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SonosWp8.Extensions
{
    public static class CollectionExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll)
        {
            return coll == null ? 
                new ObservableCollection<T>() : 
                new ObservableCollection<T>(coll);
        }
    }
}