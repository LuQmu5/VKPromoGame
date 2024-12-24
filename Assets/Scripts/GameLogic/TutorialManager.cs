using System;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private PlayerSwipeManager _swipeManager;
    [SerializeField] private Image _leftPart;
    [SerializeField] private Image _rightPart;

    public event Action TutorialFinished;

    private void OnEnable()
    {
        _swipeManager.Swiped += OnSwiped;
    }

    private void OnDisable()
    {
        _swipeManager.Swiped -= OnSwiped;
    }

    private void OnSwiped(Vector2 direction)
    {
        if (direction == Vector2.left)
        {
            _leftPart.gameObject.SetActive(false);
        }

        if (direction == Vector2.right)
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