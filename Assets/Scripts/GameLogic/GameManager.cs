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

    private int _score = 0;
    private int _maxScore = 3;

    [Inject]
    public GameManager(PlayerController playerController, ProgressDisplay scoreDisplay, ObjectSpawner objectSpawner, TutorialManager tutorialManager)
    {
        _playerController = playerController;
        _progressDisplay = scoreDisplay;
        _objectSpawner = objectSpawner;
        _tutorialManager = tutorialManager;

        _progressDisplay.Init(_maxScore);

        _playerController.SnowballCatched += DecreaseScore;
        _playerController.GiftCatched += IncreaseScore;

        _tutorialManager.TutorialFinished += OnTutorialFinished;
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

    private void HandleEndGame()
    {
        _objectSpawner.StopSpawn();
        UnityConnector.Singleton.OnGameCompleted();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
}
