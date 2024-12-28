using System;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum UserStates
{
    NotSubscribed = 0,
    GameNotCompleted = 1,
    GameCompleted = 2,
    PromocodeSelected = 3,
    PromocodeSent = 4
}

public enum PromocodeID
{
    TwelvePercent = 0,
    SevenPercent = 1
}

public class UnityConnector : MonoBehaviour
{
    protected const string UserState = nameof(UserState);
    protected const string UserPromocode = nameof(UserPromocode);
    protected const string GameSceneName = "Game";

    [SerializeField] private string _twelvePercentPromo = "twelvepercentprm156";
    [SerializeField] private string _sevenPercentPromo = "sevenpercentprm228";

    private UserStates[] _notSavableStates = { UserStates.NotSubscribed };
    private UserStates _defaultUserState = UserStates.GameNotCompleted;

    public static UnityConnector Singleton { get; private set; }
    public UserStates CurrentState { get; protected set; }
    public string CurrentPromocode { get; protected set; }


    public event Action<UserStates> UserStateChanged;


    [DllImport("__Internal")]
    private static extern void RequestJsSubscribe();

    [DllImport("__Internal")]
    private static extern void RequestJsPostStory();

    [DllImport("__Internal")]
    private static extern void RequestJsPromocodeSend(string promocode);


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

        SceneManager.LoadScene(GameSceneName);
    }

    /// <summary>
    /// must be called first in index.html in script on load or in Start Unity Method if test
    /// </summary>
    public void InitSDK()
    {
        TryLoadUserPromocode();
        SetNewState((int)UserStates.NotSubscribed);
        Subscribe();
    }

    public virtual void Subscribe()
    {
        RequestJsSubscribe();
    }

    public virtual void SendPromocode()
    {
        RequestJsPromocodeSend(CurrentPromocode);
    }

    public void OnGameNotCompleted()
    {
        SetNewState((int)UserStates.GameNotCompleted);
    }

    public void OnGameCompleted()
    {
        SetNewState((int)UserStates.GameCompleted);
    }

    public void OnPromocodeSelected(int promocodeID)
    {
        SetUserPromocode((PromocodeID)promocodeID);
        SetNewState((int)UserStates.PromocodeSelected);
    }

    public virtual void PostStory()
    {
        RequestJsPostStory();
    }

    public void SetNewState(int state)
    {
        CurrentState = (UserStates)state;
        UserStateChanged?.Invoke(CurrentState);

        if (_notSavableStates.Contains(CurrentState) == false)
            PlayerPrefs.SetInt(UserState, (int)CurrentState);
    }

    public void SetUserPromocode(PromocodeID iD)
    {
        if (iD == PromocodeID.TwelvePercent)
            CurrentPromocode = _twelvePercentPromo;
        else if (iD == PromocodeID.SevenPercent)
            CurrentPromocode = _sevenPercentPromo;

        PlayerPrefs.SetString(UserPromocode, CurrentPromocode);
    }

    public void TryLoadUserPromocode()
    {
        if (PlayerPrefs.HasKey(UserPromocode) == false)
            return;

        CurrentPromocode = PlayerPrefs.GetString(UserPromocode);
    }

    public void LoadUserState()
    {
        if (PlayerPrefs.HasKey(UserState))
            CurrentState = (UserStates)PlayerPrefs.GetInt(UserState);
        else
            CurrentState = _defaultUserState;

        UserStateChanged?.Invoke(CurrentState);
    }
}
