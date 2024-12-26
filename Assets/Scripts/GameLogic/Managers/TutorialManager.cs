using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Image _wrapper;

    private float _distanceToFinish = 100;
    private float _currentDistance = 0;
    private bool _delayPassed = false;

    public event Action TutorialFinished;

    private void OnEnable()
    {
        _playerInput.HorizontalInput += OnHorizontalInput;
    }

    private void OnDisable()
    {
        _playerInput.HorizontalInput -= OnHorizontalInput;
    }

    public void Activate()
    {
        _wrapper.gameObject.SetActive(true);
        Invoke(nameof(Delay), 0.3f);
    }

    private void Delay()
    {
        _delayPassed = true;
    }

    public void Deactivate()
    {
        _wrapper.gameObject.SetActive(false);
    }

    private void OnHorizontalInput(Vector3 target)
    {
        if (_wrapper.gameObject.activeSelf == false || _delayPassed == false)
            return;

        float distanceDelta = Vector3.Distance(Vector3.zero, target);
        float minDistance = 10;

        if (distanceDelta <= minDistance)
            return;

        _currentDistance += distanceDelta;

        if (_currentDistance >= _distanceToFinish)
        {
            _currentDistance = 0;
            TutorialFinished?.Invoke();
            _wrapper.gameObject.SetActive(false);
        }
    }
}