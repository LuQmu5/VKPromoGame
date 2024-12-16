using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IsMoving = nameof(IsMoving);
    private const string Victory = nameof(Victory);
    private const string CatchGift = nameof(CatchGift);
    private const string CatchSnowball = nameof(CatchSnowball);

    [SerializeField] private Animator _animator;

    public void SetMovingParam(bool state)
    {
        _animator.SetBool(IsMoving, state);
    }

    public void SetVictoryParam()
    {
        _animator.SetTrigger(Victory);
    }

    public void SetCatchGiftParam()
    {
        _animator.SetTrigger(CatchGift);
    }

    public void SetCatchSnowballParam()
    {
        _animator.SetTrigger(CatchSnowball);
    }
}