using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UserStates
{
    NotSubscribed = 0,
    GameNotCompleted = 1,
    GameCompleted = 2,
    RewardClaimed = 3,
}


public class UnityConnector : MonoBehaviour
{
    private const string UserState = nameof(UserState);
    private const string UserPromoCode = nameof(UserPromoCode);
    private const string GameSceneName = "Game";

    public event Action<UserStates> UserStateChanged;
    public static UnityConnector Singleton { get; private set; }
    public UserStates CurrentState { get; private set; } = UserStates.GameNotCompleted;
    public string ActivePromoCode { get; private set; }

    [DllImport("__Internal")]
    private static extern void RequestJsFirstPromoUse();

    [DllImport("__Internal")]
    private static extern void RequestJsSecondPromoUse();

    [DllImport("__Internal")]
    private static extern void RequestJsCheckSubscribe();

    [DllImport("__Internal")]
    private static extern void RequestJsGetPromo(string str);


    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Singleton != this)
        {
            Destroy(gameObject);
        }

        if (PlayerPrefs.HasKey(UserPromoCode))
            ActivePromoCode = PlayerPrefs.GetString(UserPromoCode);

        LoadUserState();
        SceneManager.LoadScene(GameSceneName);
    }


    // js requests in script
    public void OnCheckSubscribeRequested()
    {
        RequestJsCheckSubscribe();
    }
    
    public void OnFirstPromoUseRequested()
    {
        RequestJsFirstPromoUse();
    }

    public void OnSecondPromoUseRequested()
    {
        RequestJsSecondPromoUse();
    }

    public void OnGetPromoRequested(string promo)
    {
        RequestJsGetPromo(promo);
    }
    // js requests in script


    // in game requests
    public void OnGameCompleted()
    {
        SetNewState(UserStates.GameCompleted);
    }

    public void ResetState()
    {
        PlayerPrefs.DeleteAll();
        SetNewState(UserStates.GameNotCompleted);
    }
    // in game requests


    // from js unity game instance requests
    public void UpdateStateToNotSubscribed()
    {
        SetNewState(UserStates.NotSubscribed);
    }

    public void UpdateStateToRewardClaimed()
    {
        SetNewState(UserStates.RewardClaimed);
    }

    public void LoadUserState()
    {
        if (PlayerPrefs.HasKey(UserState))
            CurrentState = (UserStates)PlayerPrefs.GetInt(UserState);
        else
            CurrentState = UserStates.GameNotCompleted;

        UserStateChanged?.Invoke(CurrentState);
    }

    public void SetActivePromoCode(string value)
    {
        ActivePromoCode = value;
        PlayerPrefs.SetString(UserPromoCode, ActivePromoCode);
    }
    // from js unity game instance requests


    private void SetNewState(UserStates newState)
    {
        CurrentState = newState;

        if (newState != UserStates.NotSubscribed)
            PlayerPrefs.SetInt(UserState, (int)CurrentState);

        UserStateChanged?.Invoke(CurrentState);
    }
}