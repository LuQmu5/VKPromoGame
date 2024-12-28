using UnityEngine;

public class UnityConnector_TestMode : UnityConnector
{
    public override bool CheckSubscribe()
    {
        return true;
    }

    public override void TrySendPromocode()
    {
        SetNewState(UserStates.PromocodeSent);
    }

    public override bool CheckPostStory()
    {
        return true;
    }
}