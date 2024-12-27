using UnityEngine;

public class UnityConnector_TestMode : UnityConnector
{
    public override void OnCheckSubscribeRequested()
    {
        print("js function 'checkSubscribe' not working in test mode");
    }

    public override void OnGameSceneInited(AsyncOperation asyncOperation)
    {
        print("js function 'onGameSceneInited' not working in test mode");
        asyncOperation.completed -= OnGameSceneInited;
        LoadUserState();
    }

    public override void OnGameStarted()
    {
        print("js function 'onGameStarted' not working in test mode");
    }

    public override void OnClaimRewardButtonClicked(PromoNames promoName)
    {
        print("js function 'claimReward' not working in test mode");

        SetActivePromoCode(promoName.ToString());
        SetNewState((int)UserStates.RewardClaimed);
    }

    public override void OnGameCompleted()
    {
        print("js function 'onGameCompleted' not working in test mode");

        SetNewState((int)UserStates.GameCompleted);
    }
}