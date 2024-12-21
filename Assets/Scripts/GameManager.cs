using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class GameManager : IDisposable
{
    private readonly PlayerController _playerController;
    private readonly ScoreDisplay _scoreDisplay;
    private readonly UnityConnector _unityConnector;

    private int _score = 0;
    private int _maxScore = 10;

    [Inject]
    public GameManager(PlayerController playerController, ScoreDisplay scoreDisplay, UnityConnector unityConnector)
    {
        _playerController = playerController;
        _scoreDisplay = scoreDisplay;
        _unityConnector = unityConnector;

        _playerController.SnowballCatched += DecreaseScore;
        _playerController.GiftCatched += IncreaseScore;

        _scoreDisplay.SetText($"{_score}/{_maxScore}");
    }

    public void Dispose()
    {
        _playerController.SnowballCatched -= DecreaseScore;
        _playerController.GiftCatched -= IncreaseScore;
    }

    private void IncreaseScore()
    {
        _score++;
        _scoreDisplay.SetText($"{_score}/{_maxScore}");

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
        _scoreDisplay.SetText($"{_score}/{_maxScore}");
    }
}