using System.IO.IsolatedStorage;

namespace Sonos
{
    public class PersistentStorage : IStorage
    {
        private readonly string _prefix;

        private readonly object _sharedLock = new object();

        public PersistentStorage(string prefix)
        {
            _prefix = prefix;
        }

        private IsolatedStorageSettings Store
        {
            get { return IsolatedStorageSettings.ApplicationSettings; }
        }

        public bool Contains(string key)
        {
            key = StorageKey(key);
            return Store.Contains(key);
        }

        public bool Clear(string key)
        {
            if (null == key)
                return false;
            key = StorageKey(key);

            lock (_sharedLock)
            {
                if (Store.Contains(key))
                {
                    Store.Remove(key);
                }
            }
            return true;
        }

        public void Clear()
        {
            Store.Clear();
        }

        public bool Save(string key, object value)
        {
#if DEBUG
            //Debug.WriteLine(DateTime.Now.ToString("hh:mm:ss:fff") + " Storage: Save: " + key);
#endif
            if (null == value) return false;
            key = StorageKey(key);

            lock (_sharedLock)
            {
                Store[key] = value;
            }
            return true;
        }

        public T Load<T>(string key)
        {
            key = StorageKey(key);
#if DEBUG
            //Debug.WriteLine(DateTime.Now.ToString("hh:mm:ss:fff") + " Storage: Load: " + key);
#endif
            lock (_sharedLock)
            {
                object val;
                if (!Store.TryGetValue(key, out val)) return default(T);

                return (T) val;
                //return (T) Store[key];
            }
        }

        private string StorageKey(string key)
        {
            return _prefix + "_" + key;
        }
    }
}