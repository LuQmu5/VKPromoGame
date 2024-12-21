using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : IDisposable
{
    private readonly PlayerController _playerController;
    private readonly ProgressDisplay2 _progressDisplay;
    private readonly UnityConnector _unityConnector;

    private int _score = 0;
    private int _maxScore = 10;

    [Inject]
    public GameManager(PlayerController playerController, ProgressDisplay2 scoreDisplay, UnityConnector unityConnector)
    {
        _playerController = playerController;
        _progressDisplay = scoreDisplay;
        _unityConnector = unityConnector;

        _progressDisplay.Init(_maxScore);

        _playerController.SnowballCatched += DecreaseScore;
        _playerController.GiftCatched += IncreaseScore;
    }

    public void Dispose()
    {
        _playerController.SnowballCatched -= DecreaseScore;
        _playerController.GiftCatched -= IncreaseScore;
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
        _unityConnector.OnGameCompleted();
        SceneManager.LoadScene(0);
    }

    private void DecreaseScore()
    {
        if (_score == 0)
            return;

        _score--;
        _progressDisplay.UpdateView(false);
    }
}