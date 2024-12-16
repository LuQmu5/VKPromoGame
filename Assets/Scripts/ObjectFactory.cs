using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectFactory
{
    private const string SpawnableObjectPath = "Prefabs";

    private SpawnableObject[] _prefabs;
    private List<SpawnableObject> _pool = new List<SpawnableObject>();

    public ObjectFactory()
    {
        _prefabs = Resources.LoadAll<SpawnableObject>(SpawnableObjectPath);
    }

    public SpawnableObject Get(SpawnableObjectsType type)
    {
        SpawnableObject obj = _pool.Find(i => i.gameObject.activeSelf == false && i.Type == type);

        if (obj == null)
        {
            obj = Object.Instantiate(_prefabs.First(i => i.Type == type));
            _pool.Add(obj);
        }

        return obj;
    }
}
