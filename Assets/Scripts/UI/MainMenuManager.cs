using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string RequestSubscribeText = "Подпишитесь на нашу группу, чтобы сыграть в игру и получить промокод";
    private const string ClaimRewardText = "Выберите вашу награду! Вы можете взять только одну";
    private const string RewardClaimedText = "Нажмите на кнопку ниже, чтобы скопировать ваш промокод";

    private const string UseFirstPromoPopupText = "Чтобы получить промокод нужно опубликовать историю, продолжить?";
    private const string UseSecondPromoPopupText = "Если вы используете этот промокод, то другой станет недоступен, продолжить?";

    [Header("Curtains")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _subscribeCurtain;

    [Header("Buttons")]
    [SerializeField] private Button _subscribeButton;
    [SerializeField] private Button _firstPromoButton;
    [SerializeField] private Button _secondPromoButton;

    [Header("Promo")]
    [SerializeField] private Button _getPromoButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _description;

    [Header("Popup Window")]
    [SerializeField] private PopupWindowDisplay _popupWindow;

    private void OnEnable()
    {
        _subscribeButton.onClick.AddListener(OnSubscribeButtonClicked);
        _firstPromoButton.onClick.AddListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.AddListener(OnSecondPromoButtonClicked);
        _getPromoButton.onClick.AddListener(OnGetPromoButtonClicked);
    }

    private void OnDisable()
    {
        _subscribeButton.onClick.RemoveListener(OnSubscribeButtonClicked);
        _firstPromoButton.onClick.RemoveListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.RemoveListener(OnSecondPromoButtonClicked);
        _getPromoButton.onClick.RemoveListener(OnGetPromoButtonClicked);
    }

    public void Show()
    {
        _canvas.gameObject.SetActive(true);
        _subscribeCurtain.gameObject.SetActive(false);
    }

    public void HideSubscribeCurtain()
    {
        _subscribeCurtain.gameObject.SetActive(false);
    }

    public void OnGameNotCompleted()
    {
        _canvas.gameObject.SetActive(false);
    }

    public void OnGameCompleted()
    {
        _subscribeButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(true);
        _firstPromoButton.gameObject.SetActive(true);
        _getPromoButton.gameObject.SetActive(false);

        _description.text = ClaimRewardText;
    }

    public void OnRewardClaimed()
    {
        _canvas.gameObject.SetActive(true);

        _getPromoButton.gameObject.SetActive(true);
        _getPromoButton.GetComponentInChildren<TMP_Text>().text = UnityConnector.Singleton.ActivePromoCode;

        _subscribeButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);

        _description.text = RewardClaimedText;
    }

    public void OnNotSubscribed()
    {
        _canvas.gameObject.SetActive(true);

        _getPromoButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);
        _subscribeButton.gameObject.SetActive(true);

        _description.text = RequestSubscribeText;
    }

    private void OnGetPromoButtonClicked()
    {
        UnityConnector.Singleton.OnGetPromoButtonClicked();
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
}
