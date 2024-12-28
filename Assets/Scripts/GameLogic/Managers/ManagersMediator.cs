using System;
using Zenject;

public class ManagersMediator : IDisposable
{
    private readonly GameManager _gameManager;
    private readonly MainMenuManager _mainMenuManager;
    private readonly TutorialManager _tutorialManager;
    private readonly GameOverDisplay _gameOverDisplay;

    private bool _isGameWasCompleted = true;

    [Inject]
    public ManagersMediator(GameManager gameManager, MainMenuManager mainMenuManager, TutorialManager tutorialManager, GameOverDisplay gameOverDisplay)
    {
        _gameManager = gameManager;
        _mainMenuManager = mainMenuManager;
        _tutorialManager = tutorialManager;
        _gameOverDisplay = gameOverDisplay;

        Init();
        Subscribe();
    }

    public void Dispose()
    {
        Unsubscribe();
    }

    private void Init()
    {
        if (UnityConnector.Singleton is UnityConnector_TestMode)
            UnityConnector.Singleton.InitSDK();
    }

    private void Subscribe()
    {
        UnityConnector.Singleton.UserStateChanged += UpdateGameFromUserState;
        _tutorialManager.TutorialFinished += OnTutorialFinished;
        _gameManager.GameVictoryHandled += OnGameVictoryHandled;
    }

    private void Unsubscribe()
    {
        UnityConnector.Singleton.UserStateChanged -= UpdateGameFromUserState;
        _tutorialManager.TutorialFinished -= OnTutorialFinished;
        _gameManager.GameVictoryHandled -= OnGameVictoryHandled;
    }

    private void UpdateGameFromUserState(UserStates state)
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

            case (UserStates.PromocodeSelected):
                OnPromocodeSelected();
                break;

            case (UserStates.PromocodeSent):
                OnPromocodeSent();
                break;
        }
    }

    private void OnNotSubscribed()
    {
        _mainMenuManager.OnNotSubscribed();
    }

    private void OnGameNotCompleted()
    {
        _isGameWasCompleted = false;

        _mainMenuManager.Hide();
        _tutorialManager.Activate();
    }

    private void OnGameCompleted()
    {
        if (_isGameWasCompleted)
            _mainMenuManager.OnGameCompleted();
        else
            _gameOverDisplay.Show();
    }

    private void OnPromocodeSelected()
    {
        _mainMenuManager.OnPromocodeSelected();
    }

    private void OnPromocodeSent()
    {
        _mainMenuManager.OnPromocodeSent();
    }

    private void OnTutorialFinished()
    {
        _gameManager.StartGame();
        _tutorialManager.Deactivate();
    }

    private void OnGameVictoryHandled()
    {
        UnityConnector.Singleton.OnGameCompleted();
    }
}