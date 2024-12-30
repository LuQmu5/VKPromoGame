using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string RequestSubscribeText = "Вступите в нашу группу,\r\nсыграйте в игру и \r\nполучите промокод!";
    private const string GameCompletedText = "Выберите награду!\r\nВы можете активировать только один промокод.";
    private const string PromocodeSelectedText = "Супер!\r\nТеперь вы можете получить промокод.";
    private const string PromocodeSentText = "Супер!\r\nВы получили свой промокод в личные сообщения!";

    [Header("Curtains")]
    [SerializeField] private Canvas _view;

    [Header("Buttons")]
    [SerializeField] private Button _subscribeButton;
    [SerializeField] private Button _twelvePercentPromoButton;
    [SerializeField] private Button _sevenPercentPromoButton;
    [SerializeField] private RectTransform _sentPromocodeDisplay;

    [Header("Text")]
    [SerializeField] private TMP_Text _description;

    private void OnEnable()
    {
        _subscribeButton.onClick.AddListener(OnSubscribeButtonClicked);
        _twelvePercentPromoButton.onClick.AddListener(OnTwelvePercentPromoButtonClicked);
        _sevenPercentPromoButton.onClick.AddListener(OnSevenPercentPromoButtonClicked);
    }

    private void OnDisable()
    {
        _subscribeButton.onClick.RemoveListener(OnSubscribeButtonClicked);
        _twelvePercentPromoButton.onClick.RemoveListener(OnTwelvePercentPromoButtonClicked);
        _sevenPercentPromoButton.onClick.RemoveListener(OnSevenPercentPromoButtonClicked);
    }

    public void Hide()
    {
        _view.gameObject.SetActive(false);
    }

    public void OnNotSubscribed()
    {
        _view.gameObject.SetActive(true);
        _subscribeButton.gameObject.SetActive(true);

        _twelvePercentPromoButton.gameObject.SetActive(false);
        _sevenPercentPromoButton.gameObject.SetActive(false);
        _sentPromocodeDisplay.gameObject.SetActive(false);

        _description.text = RequestSubscribeText;
    }

    public void OnGameCompleted()
    {
        _view.gameObject.SetActive(true);
        _twelvePercentPromoButton.gameObject.SetActive(true);
        _sevenPercentPromoButton.gameObject.SetActive(true);

        _subscribeButton.gameObject.SetActive(false);
        _sentPromocodeDisplay.gameObject.SetActive(false);

        _description.text = GameCompletedText;
    }

    public void OnPromocodeSelected()
    {
        _view.gameObject.SetActive(true);
        _sentPromocodeDisplay.gameObject.SetActive(true);

        _twelvePercentPromoButton.gameObject.SetActive(false);
        _sevenPercentPromoButton.gameObject.SetActive(false);
        _subscribeButton.gameObject.SetActive(false);

        _description.text = PromocodeSelectedText;
    }

    public void OnPromocodeSent()
    {
        _view.gameObject.SetActive(true);

        _sentPromocodeDisplay.gameObject.SetActive(true); // поменять на false, чтобы кнопка отключилась после отправки промокода


        _twelvePercentPromoButton.gameObject.SetActive(false);
        _sevenPercentPromoButton.gameObject.SetActive(false);
        _subscribeButton.gameObject.SetActive(false);

        _description.text = PromocodeSentText;
    }

    private void OnSubscribeButtonClicked()
    {
        UnityConnector.Singleton.Subscribe();
    }

    private void OnTwelvePercentPromoButtonClicked()
    {
        UnityConnector.Singleton.PostStory();
    }

    private void OnSevenPercentPromoButtonClicked()
    {
        UnityConnector.Singleton.OnPromocodeSelected((int)PromocodeID.SevenPercent);
    }
}
