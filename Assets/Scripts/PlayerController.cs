using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 1;
    [SerializeField] private float _sizeOffset = 1;
    [SerializeField] private PlayerAnimator _view;

    private Vector3 _lastTapPosition;

    public event Action SnowballCatched;
    public event Action GiftCatched;

    private void OnMouseDown()
    {
        _lastTapPosition = ScreenInfo.GetTapWorldPosition();
        _view.SetMovingParam(true);
    }

    private void OnMouseDrag()
    {
        float diffX = _lastTapPosition.x - ScreenInfo.GetTapWorldPosition().x;
        _view.transform.eulerAngles = diffX > 0 ? new Vector3(0, 0, 0) : new Vector3(0, 180, 0); 
        transform.Translate(Vector2.left * diffX * Time.deltaTime * _speed);
        TryConstrainPositionOnScreen();
    }

    private void OnMouseUp()
    {
        _view.SetMovingParam(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Snowball snowball))
        {
            _view.SetCatchSnowballParam();
            snowball.gameObject.SetActive(false);
            SnowballCatched?.Invoke();
        }

        if (collision.collider.TryGetComponent(out Gift gift))
        {
            _view.SetCatchGiftParam();
            gift.gameObject.SetActive(false);
            GiftCatched?.Invoke();
        }
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
