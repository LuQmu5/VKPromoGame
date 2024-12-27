using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Image _wrapper;
    [SerializeField] private Canvas _mainMenuCanvas;

    private float _distanceToFinish = 100;
    private float _currentDistance = 0;
    private bool _isActive = false;

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
        Invoke(nameof(DelayActivating), 0.5f);
    }

    public void Deactivate()
    {
        _isActive = false;
        _wrapper.gameObject.SetActive(false);
    }

    private void DelayActivating()
    {
        _isActive = true;
    }

    private void OnHorizontalInput(Vector3 target)
    {
        if (_wrapper.gameObject.activeSelf == false || _mainMenuCanvas.gameObject.activeSelf || _isActive == false)
            return;

        print("hor inp");
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