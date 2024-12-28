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
    [SerializeField] private Button _sendPromoButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _description;

    private void OnEnable()
    {
        _subscribeButton.onClick.AddListener(OnSubscribeButtonClicked);
        _twelvePercentPromoButton.onClick.AddListener(OnTwelvePercentPromoButtonClicked);
        _sevenPercentPromoButton.onClick.AddListener(OnSevenPercentPromoButtonClicked);
        _sendPromoButton.onClick.AddListener(OnSendPromocodeButtonClicked);
    }

    private void OnDisable()
    {
        _subscribeButton.onClick.RemoveListener(OnSubscribeButtonClicked);
        _twelvePercentPromoButton.onClick.RemoveListener(OnTwelvePercentPromoButtonClicked);
        _sevenPercentPromoButton.onClick.RemoveListener(OnSevenPercentPromoButtonClicked);
        _sendPromoButton.onClick.RemoveListener(OnSendPromocodeButtonClicked);
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
        _sendPromoButton.gameObject.SetActive(false);

        _description.text = RequestSubscribeText;
    }

    public void OnGameCompleted()
    {
        _view.gameObject.SetActive(true);
        _twelvePercentPromoButton.gameObject.SetActive(true);
        _sevenPercentPromoButton.gameObject.SetActive(true);

        _subscribeButton.gameObject.SetActive(false);
        _sendPromoButton.gameObject.SetActive(false);

        _description.text = GameCompletedText;
    }

    public void OnPromocodeSelected()
    {
        _view.gameObject.SetActive(true);
        _sendPromoButton.gameObject.SetActive(true);

        // _sendPromoButton.GetComponentInChildren<TMP_Text>().text = UnityConnector.Singleton.CurrentPromocode;

        _twelvePercentPromoButton.gameObject.SetActive(false);
        _sevenPercentPromoButton.gameObject.SetActive(false);
        _subscribeButton.gameObject.SetActive(false);

        _description.text = PromocodeSelectedText;
    }

    public void OnPromocodeSent()
    {
        _view.gameObject.SetActive(true);
        _sendPromoButton.gameObject.SetActive(true);

        // _sendPromoButton.GetComponentInChildren<TMP_Text>().text = UnityConnector.Singleton.CurrentPromocode;

        _twelvePercentPromoButton.gameObject.SetActive(false);
        _sevenPercentPromoButton.gameObject.SetActive(false);
        _subscribeButton.gameObject.SetActive(false);

        _description.text = PromocodeSentText;
    }

    private void OnSubscribeButtonClicked()
    {
        if (UnityConnector.Singleton.CheckSubscribe())
            UnityConnector.Singleton.OnGameNotCompleted();
    }

    private void OnTwelvePercentPromoButtonClicked()
    {
        if (UnityConnector.Singleton.CheckPostStory())
            UnityConnector.Singleton.OnPromocodeSelected(PromocodeID.TwelvePercent);
    }

    private void OnSevenPercentPromoButtonClicked()
    {
        UnityConnector.Singleton.OnPromocodeSelected(PromocodeID.SevenPercent);
    }

    private void OnSendPromocodeButtonClicked()
    {
       UnityConnector.Singleton.TrySendPromocode();
    }
}
