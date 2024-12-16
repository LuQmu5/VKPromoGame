using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string GameSceneName = "Game";

    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _firstPromoButton;
    [SerializeField] private Button _secondPromoButton;

    [Header("SDK")]
    [SerializeField] private UnityConnector _unityConnector;

    private void OnEnable()
    {
        _playGameButton.onClick.AddListener(OnPlayGameButtonClicked);
        _firstPromoButton.onClick.AddListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.AddListener(OnSecondPromoButtonClicked);
    }

    private void OnDisable()
    {
        _playGameButton.onClick.RemoveListener(OnPlayGameButtonClicked);
        _firstPromoButton.onClick.RemoveListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.RemoveListener(OnSecondPromoButtonClicked);
    }

    private void OnPlayGameButtonClicked()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    private void OnFirstPromoButtonClicked()
    {
        _unityConnector.RequestJs();
    }

    private void OnSecondPromoButtonClicked()
    {
        _unityConnector.RequestJs();
    }
}
