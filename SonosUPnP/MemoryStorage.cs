using System.Collections.Generic;
using System.Diagnostics;

namespace Sonos
{
    public class MemoryStorage : IStorage
    {
        private static readonly IDictionary<string, object> Objects = new Dictionary<string, object>();
        private static readonly object SharedLock = new object();
        private readonly string _prefix;

        public MemoryStorage(string prefix)
        {
            _prefix = prefix;
        }

        public bool Contains(string key)
        {
            key = StorageKey(key);
            return Objects.ContainsKey(key);
        }

        public bool Clear(string key)
        {
            if (key == null) return false;

            key = StorageKey(key);
            if (!Objects.ContainsKey(key)) return false;

            lock (SharedLock)
            {
                Objects.Remove(key);
            }
            return true;
        }

        public void Clear()
        {
            lock (SharedLock)
            {
                Objects.Clear();
            }
        }

        public bool Save(string key, object value)
        {
            if (key == null) return false;
            if (value == null)
            {
                Clear(key);
                return false;
            }
            key = StorageKey(key);
#if DEBUG
            Debug.WriteLine("Storage: Save: " + key);
#endif
            lock (SharedLock)
            {
                Objects[key] = value;
            }
            return true;
        }

        public T Load<T>(string key)
        {
            key = StorageKey(key);
#if DEBUG
            Debug.WriteLine("Storage: Load: " + key);
#endif
            lock (SharedLock)
            {
                if (!Objects.ContainsKey(key))
                {
                    return default(T);
                }

                return (T) Objects[key];
            }
        }

        private string StorageKey(string key)
        {
            return _prefix + "_" + key;
        }
    }
}