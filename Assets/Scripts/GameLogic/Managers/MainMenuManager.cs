using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string RequestSubscribeText = "�������� � ���� ������,\r\n�������� � ���� � \r\n�������� ��������!";
    private const string GameCompletedText = "�������� �������!\r\n�� ������ ������������ ������ ���� ��������.";
    private const string PromocodeSentText = "�����!\r\n�������� ��������� � ������ ���������!";

    [Header("Curtains")]
    [SerializeField] private Canvas _view;

    [Header("Buttons")]
    [SerializeField] private Button _subscribeButton;
    [SerializeField] private Button _twelvePercentPromoButton;
    [SerializeField] private Button _sevenPercentPromoButton;
    [SerializeField] private RectTransform _sentPromocodeDisplay;

    [Header("Text")]
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _sentPromocodeText;

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

    public void OnPromocodeSent()
    {
        _view.gameObject.SetActive(true);

        _sentPromocodeDisplay.gameObject.SetActive(true); 

        _twelvePercentPromoButton.gameObject.SetActive(false);
        _sevenPercentPromoButton.gameObject.SetActive(false);
        _subscribeButton.gameObject.SetActive(false);

        _sentPromocodeText.text = PromocodeSentText;
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
