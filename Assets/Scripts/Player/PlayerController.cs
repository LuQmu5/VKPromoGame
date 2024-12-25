using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 50;
    [SerializeField] private float _sizeOffset = 1;
    [SerializeField] private PlayerAnimator _view;
    [SerializeField] private PlayerInput _input;
    
    private Vector3 _leftRotationEuler = Vector3.zero;
    private Vector3 _rightRotationEuler = new Vector3(0, 180, 0);

    public event Action SnowballCatched;
    public event Action GiftCatched;

    private void OnEnable()
    {
        _input.HorizontalInput += OnHorizontalInput;
    }

    private void OnDisable()
    {
        _input.HorizontalInput -= OnHorizontalInput;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Snowball snowball))
        {
            _view.SetCatchSnowballTrigger();
            snowball.gameObject.SetActive(false);
            SnowballCatched?.Invoke();
        }

        if (collision.TryGetComponent(out Gift gift))
        {
            _view.SetCatchGiftTrigger();
            gift.gameObject.SetActive(false);
            GiftCatched?.Invoke();
        }
    }

    public void PlayVictoryAnimation()
    {
        _view.SetVictoryTrigger();
    }

    private void OnHorizontalInput(Vector3 target)
    {
        float distanceDelta = Mathf.Abs(transform.position.x - target.x);
        float trashold = 0.05f;

        if (distanceDelta <= trashold)
        {
            _view.SetMovingState(false);
            return;
        }

        target.y = transform.position.y;
        target = GetConstrainedPositionByX(target);
        CheckRotation(target);
        transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime * distanceDelta);

        _view.SetMoveAnimMult(distanceDelta);
        _view.SetMovingState(true);
    }

    private void CheckRotation(Vector3 target)
    {
        if (target.x > transform.position.x)
            transform.eulerAngles = _rightRotationEuler;
        else if (target.x < transform.position.x)
            transform.eulerAngles = _leftRotationEuler;
    }

    private Vector2 GetConstrainedPositionByX(Vector2 position)
    {
        float minX = ScreenInfo.GetWorldPosition(ScreenBoundary.BottomLeft).x + _sizeOffset;
        float maxX = ScreenInfo.GetWorldPosition(ScreenBoundary.TopRight).x - _sizeOffset;

        if (position.x < minX)
        {
            return new Vector3(minX, position.y);
        }

        if (position.x > maxX)
        {
            return new Vector3(maxX, position.y);
        }

        return position;
    }
}
