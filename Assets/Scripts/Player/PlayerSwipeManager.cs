using System;
using UnityEngine;

public class PlayerSwipeManager : MonoBehaviour
{
    private const float SWIPE_TRESHOLD = 50;

    private Vector2 _startTouch;
    private Vector2 _swipeDelta;
    private Vector2 _swipeDirection;
    private bool _touchMoved;

    private Vector2 TouchPosition() => (Vector2)Input.mousePosition;
    private bool TouchBegan() => Input.GetMouseButtonDown(0);
    private bool TouchEnded() => Input.GetMouseButtonUp(0);
    private bool GetTouch() => Input.GetMouseButton(0);

    public event Action<Vector2> Swiped;

    private void Update()
    {
        if (TouchBegan())
        {
            _startTouch = TouchPosition();
            _touchMoved = true;
        }
        else if (TouchEnded() && _touchMoved)
        {
            SendSwipe();
            _touchMoved = false;
        }

        _swipeDelta = Vector2.zero;

        if (_touchMoved && GetTouch())
        {
            _swipeDelta = TouchPosition() - _startTouch;
        }

        if (_swipeDelta.magnitude > SWIPE_TRESHOLD)
        {
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
            {
                if (_swipeDelta.x < 0)
                {
                    _swipeDirection = Vector2.left;
                }

                if (_swipeDelta.x > 0)
                {
                    _swipeDirection = Vector2.right;
                }
            }
        }
    }

    private void SendSwipe()
    {
        print(_swipeDirection);
        Swiped?.Invoke(_swipeDirection);

        ResetSwipe();
    }

    private void ResetSwipe()
    {
        _startTouch = Vector2.zero;
        _swipeDelta = Vector2.zero;
        _swipeDirection = Vector2.zero;
        _touchMoved = false;
    }
}