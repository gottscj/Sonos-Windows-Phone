namespace Sonos
{
    public interface IStorage
    {
        bool Contains(string key);
        bool Clear(string key);
        void Clear();
        bool Save(string key, object value);
        T Load<T>(string key);
    }
}