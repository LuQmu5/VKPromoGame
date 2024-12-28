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

    private int _score = 0;
    private int _maxScore = 10;

    public event Action GameVictoryHandled;

    [Inject]
    public GameManager(PlayerController playerController, ProgressDisplay scoreDisplay, ObjectSpawner objectSpawner)
    {
        _playerController = playerController;
        _progressDisplay = scoreDisplay;
        _objectSpawner = objectSpawner;

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

    public void HandleGameVictory()
    {
        _playerController.HandleVictory();
        _objectSpawner.StopSpawn();
        _progressDisplay.Hide();

        GameVictoryHandled?.Invoke();
    }

    private void IncreaseScore()
    {
        _score++;
        _progressDisplay.UpdateView(true);

        if (_score >= _maxScore)
            HandleGameVictory();
    }

    private void DecreaseScore()
    {
        if (_score == 0)
            return;

        _score--;
        _progressDisplay.UpdateView(false);
    }
}
