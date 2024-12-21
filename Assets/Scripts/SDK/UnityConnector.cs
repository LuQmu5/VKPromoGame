using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class UnityConnector : MonoBehaviour
{
    // Js functions
    [DllImport("__Internal")]
    private static extern void RequestJsFirstPromoUse();

    [DllImport("__Internal")]
    private static extern void RequestJsSecondPromoUse();

    [DllImport("__Internal")]
    private static extern void RequestJsSubscribe();

    [DllImport("__Internal")]
    private static extern void RequestJsCheckSubscribe();
    // Js functions

    private const string UserState = nameof(UserState);
    private UserStates _currentUserState;

    public Action<UserStates> UserStateChanged;

    private void Start()
    {
        RequestJsCheckSubscribe();
    }

    public void OnGameCompleted()
    {
        _currentUserState = UserStates.GameCompleted;
        PlayerPrefs.SetInt(UserState, (int)UserStates.GameCompleted);
        UserStateChanged?.Invoke(_currentUserState);
    }

    // js functions on buttons
    public void UseFirstPromo()
    {
        RequestJsFirstPromoUse();
    }

    public void UseSecondPromo()
    {
        RequestJsSecondPromoUse();
    }

    public void Subscribe()
    {
        RequestJsSubscribe();
    }
    // js functions on buttons


    // calls from js in index.html
    public void UpdateStateToNotSubscribed()
    {
        _currentUserState = UserStates.NotSubscribed;
        UserStateChanged?.Invoke(_currentUserState);
    }

    public void UpdateStateToRewardClaimed()
    {
        _currentUserState = UserStates.RewardClaimed;
        PlayerPrefs.SetInt(UserState, (int)_currentUserState);
        UserStateChanged?.Invoke(_currentUserState);
    }

    public void LoadUserState()
    {
        if (PlayerPrefs.HasKey(UserState))
            _currentUserState = (UserStates)PlayerPrefs.GetInt(UserState);
        else
            _currentUserState = UserStates.GameNotCompleted;

        UserStateChanged?.Invoke(_currentUserState);
    }
    // calls from js in index.html
}
