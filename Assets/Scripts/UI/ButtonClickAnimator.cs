using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum ButtonAnimations
{
    ButtonDown,
    ButtonUp
}

[RequireComponent(typeof(Animation))]
[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class ButtonClickAnimator : MonoBehaviour
{
    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
    }

    public void PlayDownAnimation()
    {
        _animation.Play(ButtonAnimations.ButtonDown.ToString());
    }

    public void PlayUpAnimation()
    {
        _animation.Play(ButtonAnimations.ButtonUp.ToString());
    }
}
