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
        target.y = transform.position.y;
        target = GetConstrainPositionByX(target);

        float distanceDelta = Mathf.Abs(transform.position.x - target.x);
        float trashold = 0.05f;

        if (distanceDelta <= trashold)
        {
            _view.SetMovingState(false);
            return;
        }

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

    private Vector3 GetConstrainPositionByX(Vector3 pos)
    {
        float minX = ScreenInfo.GetWorldPosition(ScreenBoundary.BottomLeft).x + _sizeOffset;
        float maxX = ScreenInfo.GetWorldPosition(ScreenBoundary.BottomRight).x - _sizeOffset;

        if (pos.x < minX)
        {
            return new Vector3(minX, pos.y);
        }

        if (pos.x > maxX)
        {
            return new Vector3(maxX, pos.y);
        }

        return pos;
    }
}
