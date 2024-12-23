using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private Button _leftPartButton;
    [SerializeField] private Button _rightPartButton;

    private int _counter = 0;

    public event Action TutorialFinished;

    private void OnEnable()
    {
        _leftPartButton.onClick.AddListener(OnLeftPartButtonClicked);
        _rightPartButton.onClick.AddListener(OnRightPartButtonClicked);
    }

    private void OnDisable()
    {
        _leftPartButton.onClick.RemoveListener(OnLeftPartButtonClicked);
        _rightPartButton.onClick.RemoveListener(OnRightPartButtonClicked);
    }

    private void OnLeftPartButtonClicked()
    {
        _leftPartButton.gameObject.SetActive(false);
        IncreaseCounter();
    }

    private void OnRightPartButtonClicked()
    {
        _rightPartButton.gameObject.SetActive(false);
        IncreaseCounter();
    }

    private void IncreaseCounter()
    {
        _counter++;

        if (_counter == 2)
        {
            TutorialFinished?.Invoke();
        }
    }
}