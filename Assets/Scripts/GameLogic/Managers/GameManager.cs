using System;
using System.Collections;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : IDisposable
{
    private readonly PlayerController _playerController;
    private readonly ProgressDisplay _progressDisplay;
    private readonly ObjectSpawner _objectSpawner;
    private readonly TutorialManager _tutorialManager;
    private readonly GameOverDisplay _gameOverDisplay;

    private int _score = 0;
    private int _maxScore = 10;

    [Inject]
    public GameManager(PlayerController playerController, ProgressDisplay scoreDisplay, ObjectSpawner objectSpawner,
        TutorialManager tutorialManager, GameOverDisplay gameOverDisplay)
    {
        _playerController = playerController;
        _progressDisplay = scoreDisplay;
        _objectSpawner = objectSpawner;
        _tutorialManager = tutorialManager;
        _gameOverDisplay = gameOverDisplay;

        _playerController.SnowballCatched += DecreaseScore;
        _playerController.GiftCatched += IncreaseScore;
        _tutorialManager.TutorialFinished += OnTutorialFinished;

        _progressDisplay.Init(_maxScore);
        _progressDisplay.Hide();
    }

    public void Dispose()
    {
        _playerController.SnowballCatched -= DecreaseScore;
        _playerController.GiftCatched -= IncreaseScore;
        _tutorialManager.TutorialFinished -= OnTutorialFinished;
    }

    private void IncreaseScore()
    {
        _score++;
        _progressDisplay.UpdateView(true);

        if (_score >= _maxScore)
            HandleEndGame();
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
        UnityConnector.Singleton.OnGameStarted();

        _progressDisplay.Show();
        _objectSpawner.StartSpawn();
    }

    private void HandleEndGame()
    {
        UnityConnector.Singleton.OnGameCompleted();

        _playerController.StopLogic();
        _objectSpawner.StopSpawn();
        _progressDisplay.Hide();
        _gameOverDisplay.Show();
    }

    // dev tools
    public void CompleteGame()
    {
        UnityConnector.Singleton.OnGameCompleted();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ClearPrefs()
    {
        UnityConnector.Singleton.OnResetUserState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // dev tools
}
