using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;

namespace CySDK
{
    public class CyInterstitial : IInterstitialAdAPI, IReloader,IInitializable
    {
        bool _isReady;
        IRetryer retryer;
        AdLoader loader;
        InterstitialInfo info;
        public int RetryCount => 5;

        public int IdCount => 1;

        public Action<bool> onReloaded { get; set; }

        public event Action<bool> onClose;

        public void Initialize()
        {
            loader = new AdLoader((v) =>
            {
                onReloaded?.Invoke(v);
                _isReady = v;
            });
            info = new InterstitialInfo() { owner = this };
            retryer.Regist(this);
        }

        public bool isReady() => _isReady;

        public void Reload(int id)
        {
            AdHelper.loadInterstitial(loader);
        }

        public void Show()
        {
            AdHelper.showInterstitial(info);
        }
        void LoadInternal()
        {
            retryer.Load(this);
        }
        class InterstitialInfo : IShowListener
        {
            public CyInterstitial owner;
            public void onClose()
            {
                owner.onClose?.Invoke(true);
                owner.LoadInternal();
            }

            public void onShow()
            {
                
            }

            public void onShowFailed(REASON reason, string message)
            {
                owner.LoadInternal();
            }
        }
    }
}
