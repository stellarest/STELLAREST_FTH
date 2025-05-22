using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Runtime.CompilerServices;
using UnityEngine;
using static STELLAREST_FTH.Define;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace STELLAREST_FTH
{
#if UNITY_EDITOR
    public class DevShortcut : EditorWindow
    {
        [MenuItem("Dev/Clear Log %#E")]
        public static void ClearLog()
            => Dev.ClearLog();

        [MenuItem("Dev/Log %#H")]
        public static void PrintLog()
        {
            Dev.Log("MyLog");
        }

        [MenuItem("Dev/Log - DevKeys %#D")]
        public static void PrintDevKeyInfos()
            => Dev.PrintDevKeyInfos();
    }
#endif

#if UNITY_EDITOR
    public class DevKey
    {
        public DevKey(EDevKey eDevKey, object keyInfo, int uniqueId)
        {
            KeyType = eDevKey;
            KeyInfo = keyInfo;
            UniqueId = Util.StringToHash(eDevKey.ToString());
            UniqueId += uniqueId;
        }

        public EDevKey KeyType { get; } = EDevKey.None;
        public object KeyInfo { get; } = null;
        public int UniqueId { get; } = Util.EToInt(EInt.NoneOfValue);
        
    }
#endif

    public static class Dev
    {
#if UNITY_EDITOR
        public static void ClearLog()
        {
            var assembly = Assembly.GetAssembly(typeof(UnityEditor.Editor));
            var type = assembly.GetType("UnityEditor.LogEntries");
            var method = type.GetMethod("Clear");
            method.Invoke(new object(), null);
            Log("### CLEAR ###");
        }
#endif
        // #FFD966
        [Conditional("UNITY_EDITOR")]
        public static void Log(object log, bool isWarning = false)
        {
            if (isWarning)
                UnityEngine.Debug.LogWarning($"<color=#FFD966>[!] {log}</color>");
            else
                UnityEngine.Debug.Log($"{log}");
        }

        // #FF4433
        [Conditional("UNITY_EDITOR")]
        public static void LogError(object obj, object method, object log = null)
        {
            if (log != null)
                UnityEngine.Debug.LogError($"<color=#FF4433>[!]</color> <color=yellow>{obj}</color><color=white>::</color><color=cyan>{method}</color>\n<color=#FF4433>-></color> <color=white>{log}</color>");
            else
                UnityEngine.Debug.LogError($"<color=#FF4433>[!]</color> <color=yellow>{obj}</color><color=white>::</color><color=cyan>{method}</color>");

            UnityEngine.Debug.Break();
        }

#if UNITY_EDITOR
        private static Dictionary<EDevKey, DevKey> _devKeyDict = null;

        public static bool KeyDown(EDevKey eDevKey, object keyInfo, [CallerLineNumber] int line = 0)
        {
            _devKeyDict = Util.InitOnce<Dictionary<EDevKey, DevKey>>(_devKeyDict);
            bool input = UnityEngine.Input.GetKeyDown(DevInputKey(eDevKey));
            if (input)
            {
                if (_devKeyDict.TryGetValue(eDevKey, out var value) == false)
                {
                    _devKeyDict.Add(eDevKey, new DevKey(eDevKey: eDevKey, keyInfo: keyInfo, uniqueId: line));
                    return true;
                }

                int uniqueId = Util.StringToHash(eDevKey.ToString()) + line;
                if (uniqueId != value.UniqueId)
                    return false;
            }

            return input;
        }

        public static void PrintDevKeyInfos()
        {
            if (_devKeyDict == null || _devKeyDict.Count == 0)
            {
                Dev.Log("None of DevKeys");
                return;
            }

            Dev.Log("<color=white>↓ DevKey List ↓</color>");
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<EDevKey, DevKey> pair in _devKeyDict)
                sb.Append($"-> {pair.Key}::{pair.Value.KeyInfo}\n");
            Dev.Log($"<color=white>DevKeys:</color>\n{sb.ToString()}");
            Dev.Log("<color=white>↑ DevKey List ↑</color>");
        }

        public static GameObject CreatePrimitive(PrimitiveType primitiveType, Vector3 pos, Quaternion rot)
        {
            GameObject go = UnityEngine.GameObject.CreatePrimitive(primitiveType);
            go.transform.position = pos;
            go.transform.rotation = rot;
            return go;
        }

        private static UnityEngine.KeyCode DevInputKey(EDevKey eInput)
        {
            return eInput switch
            {
                EDevKey.Num01 => KeyCode.Alpha1, EDevKey.Num02 => KeyCode.Alpha2,
                EDevKey.Num03 => KeyCode.Alpha3, EDevKey.Num04 => KeyCode.Alpha4,
                EDevKey.Num05 => KeyCode.Alpha5, EDevKey.Num06 => KeyCode.Alpha6,
                EDevKey.Num07 => KeyCode.Alpha7, EDevKey.Num08 => KeyCode.Alpha8,
                EDevKey.Num09 => KeyCode.Alpha9, EDevKey.Num00 => KeyCode.Alpha0,
                _ => throw new ArgumentOutOfRangeException($"{nameof(Dev)}::{nameof(DevInputKey)}")
            };
        }
#endif
    }
}