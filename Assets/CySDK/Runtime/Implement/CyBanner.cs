using MiniGameSDK;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace CySDK
{
    public class CyBanner : IBannerAd,IInitializable
    {
        CyBannerSetting setting;
        BannerInfo info;
        public Action<int> onClose { get; set; }
        public Action onHide { get; set; }

        public event Action onShow;

        public void Hide()
        {
            AdHelper.closeBanner();
        }

        public void Show()
        {
            AdHelper.showBanner(info, setting.startMargin, setting.endMargin, setting.bottomMargin, setting.horizMode);
        }

        public void Initialize()
        {
            setting = AScriptableObject.Get<CyBannerSetting>();
            if (setting == null)
            {
                setting = new CyBannerSetting();
                Debug.LogWarning("Cy Banner Setting Not Found,Used Default Setting.");
            }
        }
        class BannerInfo : IBannerListener
        {
            public CyBanner owner;
            public void onAdClosed()
            {
                owner.onClose?.Invoke(0);
                owner.onHide?.Invoke();
            }

            public void onAdFailedToLoad(REASON reason, string message)
            {
                Debug.LogError($"banner ad load error reason:{reason} msg:{message}");
            }

            public void onAdImpression()
            {
                
            }

            public void onAdLoaded()
            {
                
            }

            public void onAdOpened()
            {
                owner.onShow?.Invoke();
            }
        }
    }
}
