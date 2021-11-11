using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace CySDK
{
    public class AssetSetter : IParamSettng
    {
        public void SetParam()
        {
            AssetHelper.CreateAsset<AdmobID>();
            AssetHelper.CreateAsset<CySDKSetting>();
            AssetHelper.CreateAsset<CyBannerSetting>();
        }
    }
}
