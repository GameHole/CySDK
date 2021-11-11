using System;
using System.Collections.Generic;
using UnityEngine;
namespace CySDK
{
	public class AdLoader:ILoadListener
	{
        Action<bool> _load;
        public AdLoader(Action<bool> load)
        {
            _load = load;
        }
        public void onAdFailedToLoad(REASON reason, string message)
        {
            Debug.LogError($"ad load error reason:{reason} msg:{message}");
            _load?.Invoke(false);
        }

        public void onAdLoaded()
        {
            //Debug.Log($"onAdLoaded");
            _load?.Invoke(true);
        }
    }
}
