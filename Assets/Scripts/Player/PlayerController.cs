using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _distanceMove = 1;
    [SerializeField] private float _sizeOffset = 1;
    [SerializeField] private PlayerAnimator _view;
    [SerializeField] private PlayerSwipeManager _swipeManager;
    
    private Vector3 _leftRotationEuler = Vector3.zero;
    private Vector3 _rightRotationEuler = new Vector3(0, 180, 0);
    private Coroutine _movingCoroutine;

    public event Action SnowballCatched;
    public event Action GiftCatched;

    private void OnEnable()
    {
        _swipeManager.Swiped += OnSwiped;
    }

    private void OnDisable()
    {
        _swipeManager.Swiped -= OnSwiped;
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

    private void OnSwiped(Vector2 direction)
    {
        if (_movingCoroutine != null)
            StopCoroutine(_movingCoroutine);

        _movingCoroutine = StartCoroutine(Moving(direction));
    }

    private IEnumerator Moving(Vector3 direction)
    {
        _view.SetMovingParam(true);
        float offsetDistance = 0.1f;
        Vector3 targetPosition = GetConstrainedPosition(transform.position + direction * _distanceMove);

        while (Vector2.Distance(transform.position, targetPosition) > offsetDistance)
        {
            yield return null;

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, _speed * Time.deltaTime);
        }

        _view.SetMovingParam(false);
        _movingCoroutine = null;
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
