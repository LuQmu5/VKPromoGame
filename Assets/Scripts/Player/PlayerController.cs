using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 50;
    [SerializeField] private float _sizeOffset = 1;
    [SerializeField] private PlayerAnimator _view;
    [SerializeField] private SlideManager _slideManager;
    
    private Vector3 _leftRotationEuler = Vector3.zero;
    private Vector3 _rightRotationEuler = new Vector3(0, 180, 0);

    public event Action SnowballCatched;
    public event Action GiftCatched;

    private void OnEnable()
    {
        _slideManager.Slide += OnSlide;
        _slideManager.SlideEnd += OnSlideEnd;
    }

    private void OnDisable()
    {
        _slideManager.Slide -= OnSlide;
        _slideManager.SlideEnd -= OnSlideEnd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Snowball snowball))
        {
            _view.SetCatchSnowballParam();
            snowball.gameObject.SetActive(false);
            SnowballCatched?.Invoke();
        }

        if (collision.TryGetComponent(out Gift gift))
        {
            _view.SetCatchGiftParam();
            gift.gameObject.SetActive(false);
            GiftCatched?.Invoke();
        }
    }

    public void PlayVictoryAnimation()
    {
        _view.SetVictoryParam();
    }

    private void OnSlide(Vector3 target)
    {
        target = GetConstrainedPosition(target);
        target.y = transform.position.y;
        Vector3 oldPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);

        if (oldPosition.x < transform.position.x)
            transform.eulerAngles = _rightRotationEuler;
        else if (oldPosition.x > transform.position.x)
            transform.eulerAngles = _leftRotationEuler;

        _view.SetMovingParam(true);
    }

    private void OnSlideEnd()
    {
        _view.SetMovingParam(false);
    }

    private Vector2 GetConstrainedPosition(Vector2 position)
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
