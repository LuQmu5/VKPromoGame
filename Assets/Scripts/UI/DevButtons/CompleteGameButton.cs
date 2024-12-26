﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class CompleteGameButton : MonoBehaviour
{
    [SerializeField] private Button _button;

    private GameManager _gameManager;

    [Inject]
    public void Construct(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClicked);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClicked);
    }

    public void HideButton()
    {
        gameObject.SetActive(false);
    }

    private void OnButtonClicked()
    {
        _gameManager.EndGame();
        UnityConnector.Singleton.SaveState((int)UserStates.GameCompleted);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}