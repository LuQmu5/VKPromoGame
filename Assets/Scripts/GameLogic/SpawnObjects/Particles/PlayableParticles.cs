using UnityEngine;

public abstract class PlayableParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private bool _isDeactivatingAfterPlay = true;

    [field: SerializeField] public ParticlesType Type { get; private set; }

    public void Play()
    {
        _particleSystem.Play();

        if (_isDeactivatingAfterPlay == false)
            return;

        float time = _particleSystem.totalTime;
        Invoke(nameof(DelayedDeactivating), time);
    }

    private void DelayedDeactivating()
    {
        gameObject.SetActive(false);
    }
}
