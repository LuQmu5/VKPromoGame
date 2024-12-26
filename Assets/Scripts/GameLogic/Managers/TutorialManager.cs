using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Image _wrapper;

    private float _distanceToFinish = 100;
    private float _currentDistance = 0;

    public event Action TutorialFinished;

    private void OnEnable()
    {
        _playerInput.HorizontalInput += OnHorizontalInput;
    }

    private void OnDisable()
    {
        _playerInput.HorizontalInput -= OnHorizontalInput;
    }

    private void Update()
    {
        print(_currentDistance);
    }

    public void Activate()
    {
        _wrapper.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        _wrapper.gameObject.SetActive(false);
    }

    private void OnHorizontalInput(Vector3 target)
    {
        if (_wrapper.gameObject.activeSelf == false)
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