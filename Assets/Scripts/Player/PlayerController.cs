using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, ICoroutineRunner
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _distanceMove = 1;
    [SerializeField] private float _sizeOffset = 1;
    [SerializeField] private PlayerAnimator _view;
    
    private Vector3 _leftRotationEuler = Vector3.zero;
    private Vector3 _rightRotationEuler = new Vector3(0, 180, 0);
    private Vector3 _moveDirection;
    private bool _isMoving;

    public event Action SnowballCatched;
    public event Action GiftCatched;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            OnBeginDrag();

        if (Input.GetKeyUp(KeyCode.Mouse0)) 
            OnEndDrag();

        TryMove();
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

    private void OnBeginDrag()
    {
        _isMoving = true;
        _view.SetMovingParam(true);

        _moveDirection = ScreenInfo.GetTapWorldPosition().x > 0 ? Vector3.right : Vector3.left;
        transform.eulerAngles = _moveDirection == Vector3.right ? _rightRotationEuler : _leftRotationEuler;
    }

    private void OnEndDrag()
    {
        _isMoving = false;
        _view.SetMovingParam(false);
    }

    private void TryMove()
    {
        if (_isMoving == false)
            return;

        Vector3 targetPosition = transform.position + _moveDirection * _distanceMove;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        TryConstrainPositionOnScreen();
    }

    private void TryConstrainPositionOnScreen()
    {
        float minX = ScreenInfo.GetWorldPosition(ScreenBoundary.BottomLeft).x + _sizeOffset;
        float maxX = ScreenInfo.GetWorldPosition(ScreenBoundary.TopRight).x - _sizeOffset;

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(minX, transform.position.y, transform.position.z);
        }

        if (transform.position.x > maxX)
        {
            transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
        }
    }
}
