using System;
using UnityEngine;
using Zenject;

public class GameManager : IDisposable
{
    private readonly PlayerController _playerController;
    private readonly ScoreDisplay _scoreDisplay;

    private int _score = 0;
    private int _maxScore = 10;

    [Inject]
    public GameManager(PlayerController playerController, ScoreDisplay scoreDisplay)
    {
        _playerController = playerController;
        _scoreDisplay = scoreDisplay;

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
            Time.timeScale = 0;
    }

    private void DecreaseScore()
    {
        if (_score == 0)
            return;

        _score--;
        _scoreDisplay.SetText($"{_score}/{_maxScore}");
    }
}