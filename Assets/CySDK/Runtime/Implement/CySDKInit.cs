using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace CySDK
{
    public interface ISdkInit: IInterface { }
    public class CySDKInit : ISdkInit, IInitializable
    {
        public void Initialize()
        {
            string id = null;
            var setting = AScriptableObject.Get<CySDKSetting>();
            if (setting.debug)
                id = "4D617A80F25BE57BF11002FB22B39D7D";
            AdHelper.init(id);
        }
    }
}
