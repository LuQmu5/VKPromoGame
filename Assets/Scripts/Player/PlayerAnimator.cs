using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IsMoving = nameof(IsMoving);
    private const string Victory = nameof(Victory);
    private const string CatchGift = nameof(CatchGift);
    private const string CatchSnowball = nameof(CatchSnowball);
    private const string MoveAnimMult = nameof(MoveAnimMult);

    [SerializeField] private Animator _animator;

    public void SetMovingState(bool state)
    {
        _animator.SetBool(IsMoving, state);
    }

    public void SetVictoryTrigger()
    {
        _animator.SetTrigger(Victory);
    }

    public void SetCatchGiftTrigger()
    {
        _animator.SetTrigger(CatchGift);
    }

    public void SetCatchSnowballTrigger()
    {
        _animator.SetTrigger(CatchSnowball);
    }

    public void SetMoveAnimMult(float value)
    {
        float minValue = 0.25f;
        float maxValue = 2f;
        value = Mathf.Clamp(value, minValue, maxValue);

        _animator.SetFloat(MoveAnimMult, value);
    }
}