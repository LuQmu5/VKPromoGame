using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    private const string RequestSubscribeText = "����������� �� ���� ������, ����� ������� � ���� � �������� ��������";
    private const string ClaimRewardText = "�������� ���� �������! �� ������ ����� ������ ����";
    private const string RewardClaimedText = "��� �������� ���� ��� ����� ���������� � ������ ���������";

    private const string UseFirstPromoPopupText = "����� �������� �������� ����� ������������ �������, ����������?";
    private const string UseSecondPromoPopupText = "���� �� ����������� ���� ��������, �� ������ ������ ����������, ����������?";

    [SerializeField] private Canvas _canvas;

    [Header("Buttons")]
    [SerializeField] private Button _subscribeButton;
    [SerializeField] private Button _firstPromoButton;
    [SerializeField] private Button _secondPromoButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _description;

    [Header("Popup Window")]
    [SerializeField] private PopupWindowDisplay _popupWindow;

    private void OnEnable()
    {
        _subscribeButton.onClick.AddListener(OnSubscribeButtonClicked);
        _firstPromoButton.onClick.AddListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.AddListener(OnSecondPromoButtonClicked);
    }

    private void OnDisable()
    {
        _subscribeButton.onClick.RemoveListener(OnSubscribeButtonClicked);
        _firstPromoButton.onClick.RemoveListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.RemoveListener(OnSecondPromoButtonClicked);
    }

    public void OnGameNotCompleted()
    {
        _canvas.gameObject.SetActive(false);
    }

    public void OnGameCompleted()
    {
        _canvas.gameObject.SetActive(true);

        _subscribeButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(true);
        _firstPromoButton.gameObject.SetActive(true);

        _description.text = ClaimRewardText;
    }

    public void OnRewardClaimed()
    {
        _canvas.gameObject.SetActive(true);

        _subscribeButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);

        _description.text = RewardClaimedText;
    }

    public void OnNotSubscribed()
    {
        _canvas.gameObject.SetActive(true);

        _secondPromoButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);
        _subscribeButton.gameObject.SetActive(true);

        _description.text = RequestSubscribeText;
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
