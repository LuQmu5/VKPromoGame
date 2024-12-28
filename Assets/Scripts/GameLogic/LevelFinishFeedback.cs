using UnityEngine;

public class LevelFinishFeedback : MonoBehaviour
{
    public AudioSource _levelComplete;
    public GameObject _particles;

    private void OnEnable()
    {
        _levelComplete.Play();
        _particles.SetActive(true);
    }
}
