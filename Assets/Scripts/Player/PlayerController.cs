using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private PlayerInput _input;
    [SerializeField] private float _speed = 50;

    [Header("View")]
    [SerializeField] private PlayerAnimator _view;
    [SerializeField] private float _sizeOffset = 1;

    [Header("SFX")]
    [SerializeField] private AudioSource _snowHitSFX;
    [SerializeField] private AudioSource[] _giftsSFX;
    
    private Vector3 _leftRotationEuler = Vector3.zero;
    private Vector3 _rightRotationEuler = new Vector3(0, 180, 0);

    public event Action SnowballCatched;
    public event Action GiftCatched;

    private int audioIndex = 0;

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

            _snowHitSFX.Play();
        }

        if (collision.TryGetComponent(out Gift gift))
        {
            _view.SetCatchGiftTrigger();
            gift.gameObject.SetActive(false);
            GiftCatched?.Invoke();

            PlayGiftSFX();
        }
    }

    public void HandleVictory()
    {
        _input.HorizontalInput -= OnHorizontalInput;
        _input.enabled = false;
        _view.SetVictoryTrigger();
    }

    private void PlayGiftSFX()
    {
        _giftsSFX[audioIndex].Play();
        audioIndex++;

        if (audioIndex > 2)
            audioIndex = 0;
    }

    private void OnHorizontalInput(Vector3 target)
    {
        if (Input.GetKey(KeyCode.Mouse0) == false)
        {
            _view.SetMovingState(false);
            return;
        }

        target.y = transform.position.y;
        target = ScreenInfo.GetConstrainPositionByX(target, _sizeOffset);

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
}
