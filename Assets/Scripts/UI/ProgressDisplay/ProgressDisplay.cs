using UnityEngine;

public abstract class ProgressDisplay : MonoBehaviour
{
    [SerializeField] private Transform _wrapper;

    protected int MaxProgress;

    public abstract void Init(int maxProgress);
    public abstract void UpdateView(bool isProgressIncreased);

    public void Show()
    {
        _wrapper.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _wrapper.gameObject.SetActive(false);
    }
}