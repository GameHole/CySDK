using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace CySDK
{
    public class CyAnalize : IAnalyzeEvent,IInitializable
    {
#if UNITY_ANDROID
        AndroidJavaClass clasz;
#endif
        public void Initialize()
        {
#if UNITY_ANDROID
            clasz = new AndroidJavaClass("com.unity.cyanalize.CyAnaActivity");
            clasz.CallStatic("Init", "mark");
#endif
        }

        public void SetEvent(string key)
        {
#if UNITY_ANDROID
            if (clasz != null)
            {
                clasz.CallStatic("SendEvent", key, "", "");
            }
#endif
        }

        public void SetEvent(string key, Dictionary<string, string> value)
        {
#if UNITY_ANDROID
            if (clasz != null)
            {
                foreach (var item in value)
                {
                    clasz.CallStatic("SendEvent", key, item.Key, item.Value);
                }
            }
#endif
        }
    }
}
