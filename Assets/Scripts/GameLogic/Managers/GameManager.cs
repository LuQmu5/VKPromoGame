using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : IDisposable
{
    private readonly PlayerController _playerController;
    private readonly ProgressDisplay _progressDisplay;
    private readonly ObjectSpawner _objectSpawner;
    private readonly GameOverDisplay _gameOverDisplay;

    private int _score = 0;
    private int _maxScore = 3;

    public event UnityAction GameFinished;

    [Inject]
    public GameManager(PlayerController playerController, ProgressDisplay scoreDisplay, ObjectSpawner objectSpawner, GameOverDisplay gameOverDisplay)
    {
        _playerController = playerController;
        _progressDisplay = scoreDisplay;
        _objectSpawner = objectSpawner;
        _gameOverDisplay = gameOverDisplay;

        _playerController.SnowballCatched += DecreaseScore;
        _playerController.GiftCatched += IncreaseScore;

        _progressDisplay.Init(_maxScore);
        _progressDisplay.Hide();
    }

    public void Dispose()
    {
        _playerController.SnowballCatched -= DecreaseScore;
        _playerController.GiftCatched -= IncreaseScore;
    }

    public void StartGame()
    {
        _objectSpawner.StartSpawn();
        _progressDisplay.Show();
    }

    public void EndGame()
    {
        _playerController.StopLogic();
        _objectSpawner.StopSpawn();
        _progressDisplay.Hide();
        _gameOverDisplay.Show();
    }

    private void IncreaseScore()
    {
        _score++;
        _progressDisplay.UpdateView(true);

        if (_score >= _maxScore)
            GameFinished?.Invoke();
    }

    private void DecreaseScore()
    {
        if (_score == 0)
            return;

        _score--;
        _progressDisplay.UpdateView(false);
    }
}
