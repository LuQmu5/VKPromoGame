using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Image _wrapper;

    public event Action TutorialFinished;

    private void OnEnable()
    {
        UnityConnector.Singleton.UserStateChanged += OnUserStateChanged;
        _playerInput.HorizontalInput += OnHorizontalInput;
    }

    private void OnDisable()
    {
        UnityConnector.Singleton.UserStateChanged -= OnUserStateChanged;
        _playerInput.HorizontalInput -= OnHorizontalInput;
    }

    private void OnUserStateChanged(UserStates state)
    {
        bool isActive = state == UserStates.GameNotCompleted? true : false;
        _wrapper.gameObject.SetActive(isActive);
    }

    private void OnHorizontalInput(Vector3 target)
    {
        if (_wrapper.gameObject.activeSelf == false)
            return;

        float minMoveDelta = 2f;

        if (Mathf.Abs(target.x) >= minMoveDelta)
        {
            TutorialFinished?.Invoke();
            _wrapper.gameObject.SetActive(false);
        }
    }
}