using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    [SerializeField]private List<TKey> _keys = new List<TKey>();
    [SerializeField]private List<TValue> _values = new List<TValue>();

    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            _keys.Add(pair.Key);
            _values.Add(pair.Value);
        }
    }
    public void OnAfterDeserialize()
    {
        Clear();
        if(_keys.Count != _values.Count) {
            Debug.LogError("Keys and values lists size don't match during deserialization of dictionary. Key Size: " + _keys.Count + ", Values Size: " + _values.Count);
        }
        for(int i = 0; i < _keys.Count; i++)
        {
            Add(_keys[i], _values[i]);
        }
    }


}
