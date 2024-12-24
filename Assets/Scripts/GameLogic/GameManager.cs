using System;
using System.Collections;
using Zenject;

public class GameManager : IDisposable
{
    private readonly PlayerController _playerController;
    private readonly ProgressDisplay _progressDisplay;
    private readonly ObjectSpawner _objectSpawner;
    private readonly TutorialManager _tutorialManager;
    private readonly GameOverDisplay _gameOverDisplay;
    private readonly MainMenuManager _mainMenuManager;
    private int _score = 0;
    private int _maxScore = 10;

    [Inject]
    public GameManager(PlayerController playerController, ProgressDisplay scoreDisplay, ObjectSpawner objectSpawner,
        TutorialManager tutorialManager, GameOverDisplay gameOverDisplay, MainMenuManager mainMenuManager)
    {
        _playerController = playerController;
        _progressDisplay = scoreDisplay;
        _objectSpawner = objectSpawner;
        _tutorialManager = tutorialManager;
        _gameOverDisplay = gameOverDisplay;
        _mainMenuManager = mainMenuManager;

        _progressDisplay.Init(_maxScore);

        _playerController.SnowballCatched += DecreaseScore;
        _playerController.GiftCatched += IncreaseScore;

        _tutorialManager.TutorialFinished += OnTutorialFinished;

        _mainMenuManager.GameStarted += OnGameStarted;

        _progressDisplay.gameObject.SetActive(false);
        _tutorialManager.gameObject.SetActive(false);
    }

    public void Dispose()
    {
        _playerController.SnowballCatched -= DecreaseScore;
        _playerController.GiftCatched -= IncreaseScore;

        _tutorialManager.TutorialFinished -= OnTutorialFinished;

        _mainMenuManager.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        _tutorialManager.gameObject.SetActive(true);
    }

    private void IncreaseScore()
    {
        _score++;
        _progressDisplay.UpdateView(true);

        if (_score >= _maxScore)
            HandleEndGame();
    }

    private void HandleEndGame()
    {
        UnityConnector.Singleton.OnGameCompleted();

        _playerController.PlayVictoryAnimation();
        _progressDisplay.gameObject.SetActive(false);
        _objectSpawner.StopSpawn();
        _gameOverDisplay.Show();
    }

    private void DecreaseScore()
    {
        if (_score == 0)
            return;

        _score--;
        _progressDisplay.UpdateView(false);
    }

    private void OnTutorialFinished()
    {
        _progressDisplay.gameObject.SetActive(true);
        _objectSpawner.StartSpawn();
    }

    // dev tools
    public void CompleteGame()
    {
        _tutorialManager.gameObject.SetActive(false);
        HandleEndGame();
    }
}
