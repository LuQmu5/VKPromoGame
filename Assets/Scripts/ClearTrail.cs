using UnityEngine;

public class ClearTrail : MonoBehaviour
{
    public TrailRenderer _trail;

    private void ClearTrailPath()
    {
        _trail.Clear();
    }

    private void OnEnable()
    {
        ClearTrailPath();
    }
}
