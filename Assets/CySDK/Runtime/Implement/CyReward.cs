using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using System;
using System.Threading.Tasks;

namespace CySDK
{
    public class CyReward : IRewardAdAPI,IReloader,IInitializable
    {
        IRetryer retryer;
        AdLoader loader;
        RewardInfo info;
        TaskCompletionSource<bool> tcs;
        bool _isReady;
        public bool isNotUseAd { get; set; }

        public int RetryCount => 5;

        public int IdCount => 1;

        public Action<bool> onReloaded { get; set; }

        public event Action<bool> onClose;
        Action<bool> _onclose;
        public void AutoShow(Action<bool> onclose)
        {
            if (!isReady()) return;
            _onclose = onclose;
            ShowInternal();
        }
        void ShowInternal()
        {
            if (isNotUseAd)
            {
                onCloseInternal(true);
            }
            else
            {
                AdHelper.showReward(info);
            }
        }
        void onCloseInternal(bool v)
        {
            _onclose?.Invoke(v);
            if (tcs != null && !tcs.Task.IsCompleted)
                tcs.SetResult(v);
            onClose?.Invoke(v);
        }
        public Task<bool> AutoShowAsync()
        {
            tcs = new TaskCompletionSource<bool>();
            ShowInternal();
            return tcs.Task;
        }
        void LoadInternal()
        {
            retryer.Load(this);
        }
        public void Initialize()
        {
            loader = new AdLoader((v) =>
            {
                _isReady = v;
                onReloaded?.Invoke(v);
            });
            info = new RewardInfo() { inst = this };
            retryer.Regist(this);
        }

        public bool isReady()
        {
            return _isReady;
        }

        public void Reload(int id)
        {
            AdHelper.loadReward(loader);
        }

        class RewardInfo : IRewardShowListener
        {
            public CyReward inst;
            bool isReward;
            public void onClose()
            {
                inst.onCloseInternal(isReward);
                inst.LoadInternal();
            }

            public void onShow()
            {
                isReward = false;
            }

            public void onShowFailed(REASON reason, string message)
            {
                Debug.LogError($"reward ad show error reason:{reason} msg:{message}");
                inst.LoadInternal();
            }

            public void onUserEarnedReward(int amount, string type)
            {
                isReward = true;
            }
        }
    }
}
