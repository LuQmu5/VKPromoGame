using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IsMoving = nameof(IsMoving);
    private const string Victory = nameof(Victory);
    private const string CatchGift = nameof(CatchGift);
    private const string CatchSnowball = nameof(CatchSnowball);

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
}