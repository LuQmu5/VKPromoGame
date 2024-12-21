using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject.Asteroids;

public class MainMenuManager : MonoBehaviour
{
    private const string RequestSubscribeText = "Подпишитесь на нашу группу, чтобы получать награды за задания!";
    private const string PlayGameText = "Выполняй задания и получай награды!";
    private const string ClaimRewardTextt = "Выберите вашу награду! Вы можете взять только одну";
    private const string RewardClaimedText = "Вы уже получили награду, следите за обновлениями в группе";

    [Header("Buttons")]
    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _firstPromoButton;
    [SerializeField] private Button _secondPromoButton;
    [SerializeField] private Button _subscribeButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _description;

    [Header("SDK")]
    [SerializeField] private UnityConnector _unityConnector;

    [Header("Game")]
    [SerializeField] private ObjectSpawner _objectSpawner;

    [Header("Dev Tools")]
    [SerializeField] private Button _clearPrefsButton;

    private void OnEnable()
    {
        _playGameButton.onClick.AddListener(OnPlayGameButtonClicked);
        _firstPromoButton.onClick.AddListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.AddListener(OnSecondPromoButtonClicked);
        _subscribeButton.onClick.AddListener(OnSubscribeButtonClicked);

        _clearPrefsButton.onClick.AddListener(OnClearPrefsButtonClicked);

        _unityConnector.UserStateChanged += OnUserStateChanged;
    }

    private void OnDisable()
    {
        _playGameButton.onClick.RemoveListener(OnPlayGameButtonClicked);
        _firstPromoButton.onClick.RemoveListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.RemoveListener(OnSecondPromoButtonClicked);
        _subscribeButton.onClick.RemoveListener(OnSubscribeButtonClicked);

        _clearPrefsButton.onClick.RemoveListener(OnClearPrefsButtonClicked);

        _unityConnector.UserStateChanged -= OnUserStateChanged;
    }

    private void OnPlayGameButtonClicked()
    {
        _objectSpawner.StartSpawn();
        gameObject.SetActive(false);
    }

    private void OnClearPrefsButtonClicked()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }

    // calls from js
    public void OnUserStateChanged(UserStates state)
    {
        switch (state)
        {
            case (UserStates.NotSubscribed):
                OnNotSubscribed();
                break;

            case (UserStates.GameNotCompleted):
                OnGameNotCompleted();
                break;

            case (UserStates.GameCompleted):
                OnRewardNotClaimed();
                break;

            case (UserStates.RewardClaimed):
                OnRewardClaimed();
                break;
        }
    }

    public void HideDevTools()
    {
        _clearPrefsButton.gameObject.SetActive(false);
    }

    // Buttons click connect js
    private void OnFirstPromoButtonClicked()
    {
        _unityConnector.UseFirstPromo();
    }

    private void OnSecondPromoButtonClicked()
    {
        _unityConnector.UseSecondPromo();
    }

    private void OnSubscribeButtonClicked()
    {
        _unityConnector.Subscribe();
    }
    // Buttons click connect js

    // User states
    private void OnNotSubscribed()
    {
        _playGameButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);

        _subscribeButton.gameObject.SetActive(true);
        _description.text = RequestSubscribeText;
    }

    private void OnGameNotCompleted()
    {
        _firstPromoButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);
        _subscribeButton.gameObject.SetActive(false);

        _playGameButton.gameObject.SetActive(true);
        _description.text = PlayGameText;
    }

    private void OnRewardNotClaimed()
    {
        _subscribeButton.gameObject.SetActive(false);
        _playGameButton.gameObject.SetActive(false);

        _secondPromoButton.gameObject.SetActive(true);
        _firstPromoButton.gameObject.SetActive(true);
        _description.text = ClaimRewardTextt;
    }

    private void OnRewardClaimed()
    {
        _subscribeButton.gameObject.SetActive(false);
        _playGameButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);

        _description.text = RewardClaimedText;
    }
}
