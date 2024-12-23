using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupWindowDisplay : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private TMP_Text _description;
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Button _declineButton;

    private void OnEnable()
    {
        _declineButton.onClick.AddListener(OnDeclineButtonClicked);
    }

    private void OnDisable()
    {
        _declineButton.onClick.RemoveListener(OnDeclineButtonClicked);
        _confirmButton.onClick.RemoveAllListeners();
    }

    public void Show(string message, UnityAction confirmAction)
    {
        _container.gameObject.SetActive(true);
        _description.text = message;
        _confirmButton.onClick.AddListener(confirmAction);
    }

    public void Hide()
    {
        _confirmButton.onClick.RemoveAllListeners();
        _container.gameObject.SetActive(false);
    }

    private void OnDeclineButtonClicked()
    {
        _container.gameObject.SetActive(false);
        _confirmButton.onClick.RemoveAllListeners();
    }
}