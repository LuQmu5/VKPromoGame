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
    private static extern bool RequestJsCheckSubscribe();

    [DllImport("__Internal")]
    private static extern bool RequestJsCheckPostStory();

    [DllImport("__Internal")]
    private static extern bool RequestJsCheckPromocodeSend(string promocode);


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

        if (CheckSubscribe())
        {
            LoadUserState();
        }
        else
        {
            SetNewState((int)UserStates.NotSubscribed);
        }
    }

    public virtual bool CheckSubscribe()
    {
        return RequestJsCheckSubscribe();
    }

    public virtual void TrySendPromocode()
    {
        if (RequestJsCheckPromocodeSend(CurrentPromocode))
        {
            SetNewState(UserStates.PromocodeSent);
        }
    }

    public void OnGameNotCompleted()
    {
        SetNewState(UserStates.GameNotCompleted);
    }

    public void OnGameCompleted()
    {
        SetNewState(UserStates.GameCompleted);
    }

    public void OnPromocodeSelected(PromocodeID promocodeID)
    {
        SetUserPromocode(promocodeID);
        SetNewState(UserStates.PromocodeSelected);
    }

    public virtual bool CheckPostStory()
    {
        return RequestJsCheckPostStory();
    }

    protected void SetNewState(UserStates state)
    {
        CurrentState = state;
        UserStateChanged?.Invoke(CurrentState);

        if (_notSavableStates.Contains(state) == false)
            PlayerPrefs.SetInt(UserState, (int)CurrentState);

        print(CurrentState);
    }

    private void SetUserPromocode(PromocodeID iD)
    {
        if (iD == PromocodeID.TwelvePercent)
            CurrentPromocode = _twelvePercentPromo;
        else if (iD == PromocodeID.SevenPercent)
            CurrentPromocode = _sevenPercentPromo;

        PlayerPrefs.SetString(UserPromocode, CurrentPromocode);
    }

    private void TryLoadUserPromocode()
    {
        if (PlayerPrefs.HasKey(UserPromocode) == false)
            return;

        CurrentPromocode = PlayerPrefs.GetString(UserPromocode);
    }

    private void LoadUserState()
    {
        if (PlayerPrefs.HasKey(UserState))
            CurrentState = (UserStates)PlayerPrefs.GetInt(UserState);
        else
            CurrentState = _defaultUserState;

        UserStateChanged?.Invoke(CurrentState);
    }
}
