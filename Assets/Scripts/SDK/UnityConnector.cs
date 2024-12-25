using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UserStates
{
    GameNotCompleted = 1,
    GameCompleted = 2,
    RewardClaimed = 3,
}


public class UnityConnector : MonoBehaviour
{
    [SerializeField] private bool _isTest = false;


    private const string UserState = nameof(UserState);
    private const string UserPromoCode = nameof(UserPromoCode);
    private const string GameSceneName = "Game";


    public static UnityConnector Singleton { get; private set; }
    public UserStates CurrentState { get; private set; } = UserStates.GameNotCompleted;
    public string ActivePromoCode { get; private set; }


    public event Action<UserStates> UserStateChanged;
    public event Action GameStarted;


    [DllImport("__Internal")]
    private static extern void RequestJsFirstPromoUse();

    [DllImport("__Internal")]
    private static extern void RequestJsSecondPromoUse();

    [DllImport("__Internal")]
    private static extern void RequestJsCheckSubscribe();

    [DllImport("__Internal")]
    private static extern void RequestJsGetPromo(string str);

    [DllImport("__Internal")]
    private static extern void RequestJsOnGameSceneInited();


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


    // js requests from unity
    public void OnGameSceneInited()
    {
        if (_isTest)
        {
            print("init scene request doesn't work in test mode");
            return;
        }

        RequestJsOnGameSceneInited();
    }

    public void OnCheckSubscribeRequested()
    {
        if (_isTest)
        {
            print("check sub request doesn't work in test mode");
            StartGame();
            return;
        }

        RequestJsCheckSubscribe();
    }
    
    public void OnFirstPromoUseRequested()
    {
        if (_isTest)
        {
            print("first promo use request doesn't work in test mode");
            SetActivePromoCode("first promo");
            UpdateStateToRewardClaimed();
            return;
        }

        RequestJsFirstPromoUse();
    }

    public void OnSecondPromoUseRequested()
    {
        if (_isTest)
        {
            print("second promo use request doesn't work in test mode");
            SetActivePromoCode("second promo");
            UpdateStateToRewardClaimed();
            return;
        }

        RequestJsSecondPromoUse();
    }

    public void OnGetPromoRequested(string promo)
    {
        if (_isTest)
        {
            print("get promo request doesn't work in test mode");
            return;
        }

        RequestJsGetPromo(promo);
    }
    // js requests from unity


    // in unity requests
    public void OnGameCompleted()
    {
        SetNewState(UserStates.GameCompleted);
    }

    public void ResetState()
    {
        PlayerPrefs.DeleteAll();
        SetNewState(UserStates.GameNotCompleted);
    }
    // in unity requests


    // from js to unity game instance requests
    public void StartGame()
    {
        GameStarted?.Invoke();
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
    // from js to unity game instance requests


    // private methods
    private void SetNewState(UserStates newState)
    {
        CurrentState = newState;
        PlayerPrefs.SetInt(UserState, (int)CurrentState);
        UserStateChanged?.Invoke(CurrentState);
    }
    // private methods
}