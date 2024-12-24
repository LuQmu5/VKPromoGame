using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject.Asteroids;

public class MainMenuManager : MonoBehaviour
{
    private const string RequestSubscribeText = "����������� �� ���� ������, ����� �������� ������� �� �������!";
    private const string PlayGameText = "�������� ������� � ������� �������!";
    private const string ClaimRewardTextt = "�������� ���� �������! �� ������ ����� ������ ����";
    private const string RewardClaimedText = "������� ������ ����, ����� ����������� ��� ��������";
    private const string UseFirstPromoPopupText = "����� �������� �������� ����� ������������ �������";
    private const string UseSecondPromoPopupText = "���� �� ����������� ���� ��������, �� ������ ������ ����������, ����������?";

    [Header("Buttons")]
    [SerializeField] private Button _playGameButton;
    [SerializeField] private Button _firstPromoButton;
    [SerializeField] private Button _secondPromoButton;
    [SerializeField] private Button _subscribeButton;
    [SerializeField] private Button _getPromoButton;

    [Header("Description Text")]
    [SerializeField] private TMP_Text _description;

    [Header("Popup Window")]
    [SerializeField] private PopupWindowDisplay _popupWindow;

    public event Action GameStarted;

    private void OnEnable()
    {
        _playGameButton.onClick.AddListener(OnPlayGameButtonClicked);
        _firstPromoButton.onClick.AddListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.AddListener(OnSecondPromoButtonClicked);
        _subscribeButton.onClick.AddListener(OnSubscribeButtonClicked);
        _getPromoButton.onClick.AddListener(OnGetPromoButtonClicked);

        UnityConnector.Singleton.UserStateChanged += UpdateViewFromUserState;
    }

    private void OnDisable()
    {
        _playGameButton.onClick.RemoveListener(OnPlayGameButtonClicked);
        _firstPromoButton.onClick.RemoveListener(OnFirstPromoButtonClicked);
        _secondPromoButton.onClick.RemoveListener(OnSecondPromoButtonClicked);
        _subscribeButton.onClick.RemoveListener(OnSubscribeButtonClicked);
        _getPromoButton.onClick.RemoveListener(OnGetPromoButtonClicked);

        UnityConnector.Singleton.UserStateChanged -= UpdateViewFromUserState;
    }

    private void Start()
    {
        // UnityConnector.Singleton.OnCheckSubscribeRequested();
        UpdateViewFromUserState(UnityConnector.Singleton.CurrentState);
    }

    private void OnPlayGameButtonClicked()
    {
        GameStarted?.Invoke();
        gameObject.SetActive(false);
    }

    private void OnGetPromoButtonClicked()
    {
        string promoCode = _getPromoButton.GetComponentInChildren<TMP_Text>().text;
        UniClipboard.SetText(promoCode);
        print(promoCode);
    }

    public void UpdateViewFromUserState(UserStates state)
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
        print("first promo");
        _popupWindow.Hide();
        UnityConnector.Singleton.OnFirstPromoUseRequested();
    }

    private void SecondPromoUse()
    {
        print("second promo");
        _popupWindow.Hide();
        UnityConnector.Singleton.OnSecondPromoUseRequested();
    }

    private void OnSubscribeButtonClicked()
    {
        print("subscribe");
        UnityConnector.Singleton.OnCheckSubscribeRequested();
    }

    private void OnNotSubscribed()
    {
        _playGameButton.gameObject.SetActive(false);
        _firstPromoButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);
        _getPromoButton.gameObject.SetActive(false);

        _subscribeButton.gameObject.SetActive(true);
        _description.text = RequestSubscribeText;
    }

    private void OnGameNotCompleted()
    {
        _firstPromoButton.gameObject.SetActive(false);
        _secondPromoButton.gameObject.SetActive(false);
        _subscribeButton.gameObject.SetActive(false);
        _getPromoButton.gameObject.SetActive(false);

        _playGameButton.gameObject.SetActive(true);
        _description.text = PlayGameText;
    }

    private void OnRewardNotClaimed()
    {
        _subscribeButton.gameObject.SetActive(false);
        _playGameButton.gameObject.SetActive(false);
        _getPromoButton.gameObject.SetActive(false);

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

        _getPromoButton.GetComponentInChildren<TMP_Text>().text = UnityConnector.Singleton.ActivePromoCode;
        _getPromoButton.gameObject.SetActive(true);
        _description.text = RewardClaimedText;
    }
}
