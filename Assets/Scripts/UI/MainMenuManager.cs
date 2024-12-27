using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string RequestSubscribeText = "Подпишитесь на нашу группу, чтобы сыграть в игру и получить промокод";
    private const string ClaimRewardText = "Выберите вашу награду! Вы можете взять только одну";
<<<<<<< HEAD
    private const string RewardClaimedText = "Нажмите на кнопку ниже, чтобы скопировать ваш промокод";
=======
    private const string RewardClaimedText = "Нажмите кнопку ниже, чтобы копировать промокод";
>>>>>>> a0fba0e449c9f4e83395aa9cfac568667c3f5883

    private const string UseFirstPromoPopupText = "Чтобы получить промокод нужно опубликовать историю, продолжить?";
    private const string UseSecondPromoPopupText = "Если вы используете этот промокод, то другой станет недоступен, продолжить?";

    [Header("Curtains")]
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _subscribeCurtain;

    [Header("Buttons")]
    [SerializeField] private Button _subscribeButton;
    [SerializeField] private Button _firstPromoButton;
    [SerializeField] private Button _secondPromoButton;
    [SerializeField] private Button _getPromoButton;

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
<<<<<<< HEAD
        _getPromoButton.onClick.AddListener(OnGetPromoButtonClicked);
=======
        _getPromoButton.onClick.AddListener(OnGetPromoButtonButtonClicked);
>>>>>>> a0fba0e449c9f4e83395aa9cfac568667c3f5883
    }

    private void OnDisable()
    {
        _subscribeButton.onClick.RemoveListener(OnSubscribeButtonClicked);
        _firstPromoButton.onClick.RemoveListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.RemoveListener(OnSecondPromoButtonClicked);
<<<<<<< HEAD
        _getPromoButton.onClick.RemoveListener(OnGetPromoButtonClicked);
=======
        _getPromoButton.onClick.RemoveListener(OnGetPromoButtonButtonClicked);
    }

    private void Start()
    {
        _getPromoButton.GetComponentInChildren<TMP_Text>().text = UnityConnector.Singleton.ActivePromoCode;
>>>>>>> a0fba0e449c9f4e83395aa9cfac568667c3f5883
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
        _getPromoButton.gameObject.SetActive(false);
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

    private void OnGetPromoButtonButtonClicked()
    {
        UnityConnector.Singleton.OnGetPromoButtonClicked();
    }

    private void FirstPromoUse()
    {
        UnityConnector.Singleton.OnClaimRewardButtonClicked(PromoNames.TwelvePercent);
        _popupWindow.Hide();
        _getPromoButton.GetComponentInChildren<TMP_Text>().text = UnityConnector.Singleton.ActivePromoCode;
    }

    private void SecondPromoUse()
    {
        UnityConnector.Singleton.OnClaimRewardButtonClicked(PromoNames.SevenPercent);
        _popupWindow.Hide();
        _getPromoButton.GetComponentInChildren<TMP_Text>().text = UnityConnector.Singleton.ActivePromoCode;
    }
}
