using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Image _leftPart;
    [SerializeField] private Image _rightPart;

    public event Action TutorialFinished;

    private void OnEnable()
    {
        _playerInput.HorizontalInput += OnHorizontalInput;
    }

    private void OnDisable()
    {
        _playerInput.HorizontalInput -= OnHorizontalInput;
    }

    private void OnHorizontalInput(Vector3 target)
    {
        float minDelta = 2f;

        if (target.x <= -minDelta)
        {
            _leftPart.gameObject.SetActive(false);
        }

        if (target.x >= minDelta)
        {
            _rightPart.gameObject.SetActive(false);
        }

        if (_leftPart.gameObject.activeSelf == false && _rightPart.gameObject.activeSelf == false)
        {
            TutorialFinished?.Invoke();
            gameObject.SetActive(false);
        }
    }
}