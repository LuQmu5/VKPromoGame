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
    private const string GameSceneName = "Game";

    public event Action<UserStates> UserStateChanged;
    public static UnityConnector Singleton { get; private set; }
    public UserStates CurrentState { get; private set; } = UserStates.GameNotCompleted;

    [DllImport("__Internal")]
    private static extern void RequestJsFirstPromoUse();

    [DllImport("__Internal")]
    private static extern void RequestJsSecondPromoUse();

    [DllImport("__Internal")]
    private static extern void RequestJsCheckSubscribe();


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

        LoadUserState();
        SceneManager.LoadScene(GameSceneName);
    }


    // js requests
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
    // js requests


    // in game requests
    public void OnGameCompleted()
    {
        SetNewState(UserStates.GameCompleted);
    }
    // in game requests


    // from js requests
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

    public void ResetState()
    {
        PlayerPrefs.DeleteAll();
        SetNewState(UserStates.GameNotCompleted);
    }
    // from js requests


    private void SetNewState(UserStates newState)
    {
        CurrentState = newState;
        PlayerPrefs.SetInt(UserState, (int)CurrentState);
        UserStateChanged?.Invoke(CurrentState);
    }
}