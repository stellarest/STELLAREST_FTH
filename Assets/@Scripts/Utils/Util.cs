using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static STELLAREST_FTH.Define;

namespace STELLAREST_FTH
{
    public class ShowOnlyAttribute : PropertyAttribute { }
    
    [Serializable]
    public class SerializableKeyValuePair<K, V>
    {
        public K Key;
        public V Value;
    }

    [Serializable]
    public class SDictionary<K, V> : Dictionary<K, V>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<SerializableKeyValuePair<K, V>> _list = new List<SerializableKeyValuePair<K, V>>();

        public void OnBeforeSerialize()
        {
            _list.Clear();
            foreach (var kv in this)
            {
                _list.Add(new SerializableKeyValuePair<K, V>()
                {
                    Key = kv.Key,
                    Value = kv.Value
                });
            }
        }

        public void OnAfterDeserialize()
        {
            this.Clear();
            foreach (var kv in _list)
            {
                if (this.ContainsKey(kv.Key) == false)
                    this.Add(kv.Key, kv.Value);
                else
                    Dev.LogError(obj: nameof(SDictionary<K, V>), method: nameof(OnAfterDeserialize), log: $"Duplicate key found during deserialization(same key): {kv.Key}");
            }
        }
    }

    public static class Util
    {
        public static T InitOnce<T>(T value) where T : class, new()
            => value ?? new T();

        public static T[] InitOnce<T>(int length, T[] array, T initialValue) where T : struct
        {
            if (array == null || array.Length != length)
            {
                array = new T[length];
                for (int i = 0; i < length; ++i)
                    array[i] = initialValue;
            }

            return array;
        }

        public static int EToInt<T>(T eInt) where T : Enum
        {
            return eInt switch
            {
                EIntDefault.NoneOfValue => -1,
                _ => throw new ArgumentOutOfRangeException($"{nameof(EToInt)}, {eInt}")
            };
        }

        public static float EToFloat<T>(T eFloat) where T : Enum
        {
            return eFloat switch
            {
                EFloatDefault.NoneOfValue => -1.0f,
                _ => throw new ArgumentOutOfRangeException($"{nameof(EToFloat)}, {eFloat}")
            };
        }

        public static string EToString<T>(T eString) where T : Enum
        {
            return eString switch
            {
                EStringDefault.Null => null,
                EStringDefault.Empty => "",
                _ => throw new ArgumentOutOfRangeException($"{nameof(EToString)}, {eString}")
            };
        }

        public static int StringToHash(string str)
        {
            unchecked
            {
                int hash = 23;
                foreach (char c in str)
                    hash = hash * 31 + c;
                return hash;
            }
        }
    }
}
