using System;
using Zenject;

public class ManagersMediator : IDisposable
{
    private readonly GameManager _gameManager;
    private readonly MainMenuManager _mainMenuManager;
    private readonly TutorialManager _tutorialManager;

    [Inject]
    public ManagersMediator(GameManager gameManager, MainMenuManager mainMenuManager, TutorialManager tutorialManager)
    {
        _gameManager = gameManager;
        _mainMenuManager = mainMenuManager;
        _tutorialManager = tutorialManager;

        Init();
        Subscribe();
    }

    public void Dispose()
    {
        Unsubscribe();
    }

    private void Init()
    {
        _tutorialManager.Deactivate();

        UnityConnector.Singleton.LoadUserState();
        UpdateFromUserState(UnityConnector.Singleton.CurrentState);
    }

    private void Subscribe()
    {
        UnityConnector.Singleton.UserStateChanged += UpdateFromUserState;
        _tutorialManager.TutorialFinished += OnTutorialFinished;
        _gameManager.GameFinished += OnGameFinished;
    }

    private void Unsubscribe()
    {
        UnityConnector.Singleton.UserStateChanged -= UpdateFromUserState;
        _tutorialManager.TutorialFinished -= OnTutorialFinished;
        _gameManager.GameFinished -= OnGameFinished;
    }

    private void UpdateFromUserState(UserStates state)
    {
        switch (state)
        {
            case (UserStates.NotSubscribed):
                OnNotSubscribed();
                break;

            case (UserStates.GameNotCompleted):
                OnGameNotCompleted();
                break;

            case (UserStates.GameCompleted):
                OnGameCompleted();
                break;

            case (UserStates.RewardClaimed):
                OnRewardClaimed();
                break;
        }
    }

    private void OnTutorialFinished()
    {
        _gameManager.StartGame();
    }

    private void OnGameFinished()
    {
        UnityConnector.Singleton.SetNewState((int)UserStates.GameCompleted);
        _gameManager.EndGame();
    }

    private void OnGameCompleted()
    {
        _mainMenuManager.OnGameCompleted();
    }

    private void OnNotSubscribed()
    {
        _mainMenuManager.OnNotSubscribed();
    }

    private void OnGameNotCompleted()
    {
        _mainMenuManager.OnGameNotCompleted();
        _tutorialManager.Activate();
    }

    private void OnRewardClaimed()
    {
        _mainMenuManager.OnRewardClaimed();
    }
}