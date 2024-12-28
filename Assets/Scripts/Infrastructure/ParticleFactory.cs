using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ParticleFactory
{
    private const string PlayableParticlesPrefabsPath = "Prefabs/Particles";

    private PlayableParticles[] _prefabs;
    private List<PlayableParticles> _pool = new List<PlayableParticles>();

    public ParticleFactory()
    {
        _prefabs = Resources.LoadAll<PlayableParticles>(PlayableParticlesPrefabsPath);
    }

    public PlayableParticles Get(ParticlesType type)
    {
        PlayableParticles obj = _pool.Find(i => i.gameObject.activeSelf == false && i.Type == type);

        if (obj == null)
        {
            obj = Object.Instantiate(_prefabs.First(i => i.Type == type));
            _pool.Add(obj);
        }

        return obj;
    }

    public IEnumerable<PlayableParticles> GetActiveObjects() => _pool.Where(i => i.gameObject.activeSelf);
}