using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject.Asteroids;

public class MainMenuManager : MonoBehaviour
{
    private const string RequestSubscribeText = "Подпишитесь на нашу группу, чтобы сыграть в игру и получить промокод";
    private const string ClaimRewardText = "Выберите вашу награду! Вы можете взять только одну";
    private const string RewardClaimedText = "Ваш промокод ниже был также дублирован в личные сообщения";

    private const string UseFirstPromoPopupText = "Чтобы получить промокод нужно опубликовать историю, продолжить?";
    private const string UseSecondPromoPopupText = "Если вы используете этот промокод, то другой станет недоступен, продолжить?";

    [Header("Buttons")]
    [SerializeField] private Button _subscribeButton;
    [SerializeField] private Button _firstPromoButton;
    [SerializeField] private Button _secondPromoButton;
    [SerializeField] private Button _getPromoButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _promoText;

    [Header("Popup Window")]
    [SerializeField] private PopupWindowDisplay _popupWindow;

    private void OnEnable()
    {
        UpdateViewFromUserState(UnityConnector.Singleton.CurrentState);

        UnityConnector.Singleton.UserStateChanged += UpdateViewFromUserState;

        _subscribeButton.onClick.AddListener(OnSubscribeButtonClicked);
        _firstPromoButton.onClick.AddListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.AddListener(OnSecondPromoButtonClicked);
        _getPromoButton.onClick.AddListener(OnGetPromoButtonClicked);
    }

    private void OnDisable()
    {
        UnityConnector.Singleton.UserStateChanged -= UpdateViewFromUserState;

        _subscribeButton.onClick.RemoveListener(OnSubscribeButtonClicked);
        _firstPromoButton.onClick.RemoveListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.RemoveListener(OnSecondPromoButtonClicked);
        _getPromoButton.onClick.AddListener(OnGetPromoButtonClicked);
    }

    private void OnGetPromoButtonClicked()
    {
        UnityConnector.Singleton.OnGetPromoCodeButtonClicked(UnityConnector.Singleton.ActivePromoCode);
    }

    private void OnSubscribeButtonClicked()
    {
        UnityConnector.Singleton.OnCheckSubscribeRequested();
    }

    private void OnFirstPromoButtonClicked()
    {
        _popupWindow.Show(UseFirstPromoPopupText, FirstPromoUse);
    }

    private void OnSecondPromoButtonClicked()
    {
        _popupWindow.Show(UseSecondPromoPopupText, SecondPromoUse);
    }

    private void UpdateViewFromUserState(UserStates state)
    {
        switch (state)
        {
            case (UserStates.GameNotCompleted):
                gameObject.SetActive(false);
                break;

            case (UserStates.NotSubscribed):
                OnNotSubscribed();
                break;

            case (UserStates.GameCompleted):
                OnRewardNotClaimed();
                break;

            case (UserStates.RewardClaimed):
                OnRewardClaimed();
                break;
        }
    }

    private void FirstPromoUse()
    {
        UnityConnector.Singleton.OnClaimRewardButtonClicked(PromoNames.TwelvePercent);
        _popupWindow.Hide();
    }

    private void SecondPromoUse()
    {
        UnityConnector.Singleton.OnClaimRewardButtonClicked(PromoNames.SevenPercent);
        _popupWindow.Hide();
    }

    private void OnRewardNotClaimed()
    {
        _subscribeButton.gameObject.SetActive(false);
        _getPromoButton.gameObject.SetActive(false);

        _secondPromoButton.gameObject.SetActive(true);
        _firstPromoButton.gameObject.SetActive(true);

        _description.text = ClaimRewardText;
    }

    private void OnRewardClaimed()
    {
        _subscribeButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);

        _getPromoButton.gameObject.SetActive(true);

        _promoText.text = UnityConnector.Singleton.ActivePromoCode;
        _description.text = RewardClaimedText;
    }

    private void OnNotSubscribed()
    {
        _secondPromoButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);
        _getPromoButton.gameObject.SetActive(false);

        _subscribeButton.gameObject.SetActive(true);
        _description.text = RequestSubscribeText;
    }
}
