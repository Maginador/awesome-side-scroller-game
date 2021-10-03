using System;
using Info;
using UnityEngine;
using UnityEngine.WSA;

namespace Base
{
    public class Info<T>
    {
        private static Action _infoChanged;
        private static string _currentInfo;
        private static T _currentInfoClass;
        public static T GetInfoFromJson(string json)
        {
            if (!string.IsNullOrEmpty(json) && _currentInfo != json)
            {
                _infoChanged.Invoke();
                _currentInfo = json;
                _currentInfoClass = JsonUtility.FromJson<T>(json); 
            }
            
            return _currentInfoClass; 
        }

        public static void AddInfoUpdateListener(Action listenerCall)
        {
            _infoChanged += listenerCall;
        }
        
        public static void RemoveInfoUpdateListener(Action listenerCall)
        {
            _infoChanged -= listenerCall;
        }
    }
}