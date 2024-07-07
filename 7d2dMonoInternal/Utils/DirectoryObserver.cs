using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenDTDMono.Utils
{
    //public class DictionaryObserver<TKey, TValue>
    //{
    //    private readonly Dictionary<TKey, TValue> _dictionary;

    //    public event EventHandler<KeyValuePair<TKey, TValue>> ItemAdded;
    //    public event EventHandler<TKey> ItemRemoved;
    //    public event EventHandler<KeyValuePair<TKey, TValue>> ItemChanged;

    //    public DictionaryObserver(Dictionary<TKey, TValue> dictionary)
    //    {
    //        _dictionary = dictionary;
    //    }

    //    public void Add(TKey key, TValue value)
    //    {
    //        _dictionary.Add(key, value);
    //        ItemAdded?.Invoke(this, new KeyValuePair<TKey, TValue>(key, value));
    //    }

    //    public bool Remove(TKey key)
    //    {
    //        if (_dictionary.ContainsKey(key))
    //        {
    //            _dictionary.Remove(key);
    //            ItemRemoved?.Invoke(this, key);
    //            return true;
    //        }
    //        return false;
    //    }

    //    public TValue this[TKey key]
    //    {
    //        get => _dictionary[key];
    //        set
    //        {
    //            bool exists = _dictionary.ContainsKey(key);
    //            _dictionary[key] = value;
    //            if (exists)
    //            {
    //                ItemChanged?.Invoke(this, new KeyValuePair<TKey, TValue>(key, value));
    //            }
    //            else
    //            {
    //                ItemAdded?.Invoke(this, new KeyValuePair<TKey, TValue>(key, value));
    //            }
    //        }
    //    }

    //    public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

    //    // Add other dictionary methods as needed
    //}





}
