using UnityEngine;

public class UnityConnector_TestMode : UnityConnector
{
    public override void SendPromocode()
    {
        SetNewState((int)UserStates.PromocodeSent);
    }

    public override void PostStory()
    {
        OnPromocodeSelected((int)PromocodeID.TwelvePercent);
    }

    public override void Subscribe()
    {
        LoadUserState();
    }
}