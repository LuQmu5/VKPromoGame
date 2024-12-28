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

public enum PromoNames
{
    TwelvePercent = 0,
    SevenPercent = 1
}

public class UnityConnector : MonoBehaviour
{
    private const string UserState = nameof(UserState);
    private const string UserPromoCode = nameof(UserPromoCode);
    private const string GameSceneName = "Game";


    public static UnityConnector Singleton { get; private set; }
    public UserStates CurrentState { get; private set; } = UserStates.GameNotCompleted;
    public string ActivePromoCode { get; protected set; }


    public event Action<UserStates> UserStateChanged;


    [DllImport("__Internal")]
    private static extern void RequestJsClaimReward(int id);

    [DllImport("__Internal")]
    private static extern void RequestJsCheckSubscribe();

    [DllImport("__Internal")]
    private static extern void RequestJsOnGameSceneInited();

    [DllImport("__Internal")]
    private static extern void RequestJsOnGameCompleted();

    [DllImport("__Internal")]
    private static extern void RequestJsOnGameStarted();

    [DllImport("__Internal")]
    private static extern void RequestJsGetPromo(string str);


    // �������������
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

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(GameSceneName);
        asyncOperation.completed += OnGameSceneInited;
    }

    /// <summary>
    /// ���������� ������������� � ������ �������� ������� �����. NOTE: ���������� �������� � index.html ������� � ���������� onload ��� unityInstance
    /// </summary>
    public virtual void OnGameSceneInited(AsyncOperation asyncOperation)
    {
        asyncOperation.completed -= OnGameSceneInited;
    }

    /// <summary>
    /// ����� �������� �� ����� ������ ���� ��� �����-���� �������������� � JS (� ������������� � � VK API)
    /// </summary>
    public virtual void OnGameStarted()
    {
        RequestJsOnGameStarted();
    }

    /// <summary>
    /// ����� �������� � ������ ���������� ���� ��� �����-���� �������������� � JS (� ������������� � � VK API)
    /// </summary>
    public virtual void OnGameCompleted()
    {
        RequestJsOnGameCompleted();
    }

    /// <summary>
    /// ����� �������� � ������, ����� �� ����� ��������� �������� ����� �� ������ ����� JS (����� VK API)
    /// </summary>
    public virtual void OnCheckSubscribeRequested()
    {
        RequestJsCheckSubscribe();
    }
    
    /// <summary>
    /// �������� � ������ ����� �� ��������� ���������, 
    /// </summary>
    public virtual void OnClaimRewardButtonClicked(PromoNames promoName)
    {
        RequestJsClaimReward((int)promoName);
    }

    /// <summary>
    /// ������������� � ��������� ���������� ��������
    /// </summary>
    /// <param name="value"></param>
    public void SetActivePromoCode(string value)
    {
        ActivePromoCode = value;
        PlayerPrefs.SetString(UserPromoCode, ActivePromoCode);
    }

    public virtual void OnGetPromoButtonClicked()
    {
        RequestJsGetPromo();
    }

    /// <summary>
    /// ����� ��������� ����� (��� ������), ����� ���� ����� ������������� ������
    /// </summary>
    public void OnResetUserState()
    {
        print("prefs cleared, restart game");
        PlayerPrefs.DeleteAll();
    }

    /// <summary>
    /// ��������� ��������� ����������� ��������� ����� � ��������� ������� UserStateChanged
    /// </summary>
    public void LoadUserState()
    {
        if (PlayerPrefs.HasKey(UserState))
            CurrentState = (UserStates)PlayerPrefs.GetInt(UserState);
        else
            CurrentState = UserStates.GameNotCompleted;

        UserStateChanged?.Invoke(CurrentState);
    }

    /// <summary>
    /// ��������: NotSubscribed = 0, GameNotCompleted = 1, GameCompleted = 2, RewardClaimed = 3,
    /// </summary>
    /// <param name="stateID"></param>
    public void SetNewState(int stateID)
    {
        if (stateID < 0 || stateID >= Enum.GetValues(typeof(UserStates)).Length)
        {
            stateID = 1;
            return;
        }

        CurrentState = (UserStates)stateID;

        if (CurrentState != UserStates.NotSubscribed)
            PlayerPrefs.SetInt(UserState, (int)CurrentState);

        UserStateChanged?.Invoke(CurrentState);
    }

    /// <summary>
    /// ����� �� ��� SetNewState(), �� ��� ��������� CurrentState-� � ���� � �������
    /// </summary>
    /// <param name="stateID"></param>
    public void SaveState(int stateID)
    {
        if (stateID < 0 || stateID >= Enum.GetValues(typeof(UserStates)).Length)
        {
            stateID = 1;
            return;
        }

        PlayerPrefs.SetInt(UserState, stateID);
    }
}
