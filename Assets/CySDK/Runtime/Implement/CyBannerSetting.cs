using System.Collections.Generic;
using UnityEngine;
namespace CySDK
{
    public class CyBannerSetting : AScriptableObject
    {
        public override string filePath => "CySDK/BannerSetting";
        public int startMargin = 16;
        public int endMargin = 16;
        public int bottomMargin = 100;
        public AdHelper.Horizon horizMode = AdHelper.Horizon.HORIZ_MODEL_CENTER;
    }
}
