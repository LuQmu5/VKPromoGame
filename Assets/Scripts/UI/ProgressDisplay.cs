using UnityEngine;

public abstract class ProgressDisplay : MonoBehaviour
{
    protected int MaxProgress;

    public abstract void Init(int maxProgress);
    public abstract void UpdateView(bool isProgressIncreased);
}