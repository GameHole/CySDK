using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using UnityEditor;
namespace CySDK
{
    public class PlayerSettingSeter : IParamSettng
    {
        public void SetParam()
        {
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel19;
        }
    }
}
