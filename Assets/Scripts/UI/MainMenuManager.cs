using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject.Asteroids;

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
    [SerializeField] private Button _getPromoButton;

    [Header("Text")]
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _promoText;

    [Header("Popup Window")]
    [SerializeField] private PopupWindowDisplay _popupWindow;

    private void OnEnable()
    {
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

    private void Start()
    {
        UpdateViewFromUserState(UnityConnector.Singleton.CurrentState);
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
        if (_canvas.gameObject.activeSelf == false)
            return;

        switch (state)
        {
            case (UserStates.GameNotCompleted):
                OnGameNotCompleted();
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

    private void OnGameNotCompleted()
    {
        _canvas.gameObject.SetActive(false);
    }

    private void OnRewardNotClaimed()
    {
        _canvas.gameObject.SetActive(true);

        _subscribeButton.gameObject.SetActive(false);
        _getPromoButton.gameObject.SetActive(false);

        _secondPromoButton.gameObject.SetActive(true);
        _firstPromoButton.gameObject.SetActive(true);

        _description.text = ClaimRewardText;
    }

    private void OnRewardClaimed()
    {
        _canvas.gameObject.SetActive(true);

        _subscribeButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);

        _getPromoButton.gameObject.SetActive(true);

        _promoText.text = UnityConnector.Singleton.ActivePromoCode;
        _description.text = RewardClaimedText;
    }

    private void OnNotSubscribed()
    {
        _canvas.gameObject.SetActive(true);

        _secondPromoButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);
        _getPromoButton.gameObject.SetActive(false);

        _subscribeButton.gameObject.SetActive(true);
        _description.text = RequestSubscribeText;
    }
}
