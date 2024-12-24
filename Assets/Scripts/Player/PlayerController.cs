using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 50;
    [SerializeField] private float _sizeOffset = 1;
    [SerializeField] private PlayerAnimator _view;
    [SerializeField] private SlideManager _swipeManager;
    
    private Vector3 _leftRotationEuler = Vector3.zero;
    private Vector3 _rightRotationEuler = new Vector3(0, 180, 0);
    private Coroutine _movingCoroutine;

    public event Action SnowballCatched;
    public event Action GiftCatched;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 target = GetConstrainedPosition(ScreenInfo.GetTapWorldPosition());
            target.y = transform.position.y;
            transform.position = Vector2.MoveTowards(transform.position, target, _speed * Time.deltaTime);
            transform.eulerAngles = target.x > transform.position.x ? _rightRotationEuler : _leftRotationEuler;
            _view.SetMovingParam(true);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _view.SetMovingParam(false);
        }
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
