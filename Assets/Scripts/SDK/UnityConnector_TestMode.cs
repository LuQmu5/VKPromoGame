using UnityEngine;

public class UnityConnector_TestMode : UnityConnector
{
    public override void OnCheckSubscribeRequested()
    {
        print("js function 'checkSubscribe' not working in test mode");
        SetNewState((int)UserStates.GameNotCompleted);
    }

    public override void OnGameSceneInited(AsyncOperation asyncOperation)
    {
        FindObjectOfType<MainMenuManager>().HideSubscribeCurtain();
        // LoadUserState();
        SetNewState((int)UserStates.NotSubscribed);
        print("js function 'onGameSceneInited' not working in test mode");
        asyncOperation.completed -= OnGameSceneInited;
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