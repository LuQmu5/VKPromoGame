using System;
using System.Collections;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField][Range(0, 100)] private int _chanceOfGiftSpawn = 75;
    [SerializeField] private float _minTimeBetweenSpawn = 1;
    [SerializeField] private float _maxTimeBetweenSpawn = 3;

    private ObjectFactory _objectFactory;
    private Coroutine _spawningCoroutine;

    [Inject]
    public void Construct(ObjectFactory objectFactory)
    {
        _objectFactory = objectFactory;
    }

    public void StartSpawn()
    {
        _spawningCoroutine = StartCoroutine(Spawning());
    }

    public void StopSpawn()
    {
        if (_spawningCoroutine == null)
            return;

        StopCoroutine(_spawningCoroutine);
        
        foreach (SpawnableObject item in _objectFactory.GetActiveObjects())
        {
            item.gameObject.SetActive(false);
        }
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minTimeBetweenSpawn, _maxTimeBetweenSpawn));

            CreateObject();
        }
    }

    private void CreateObject()
    {
        SpawnableObject spawnedObj = _objectFactory.Get(GetRandomType());
        spawnedObj.gameObject.SetActive(true);
        spawnedObj.transform.position = new Vector3
        {
            x = Random.Range(ScreenInfo.GetWorldPosition(ScreenBoundary.BottomLeft).x + spawnedObj.transform.localScale.x, ScreenInfo.GetWorldPosition(ScreenBoundary.BottomRight).x - spawnedObj.transform.localScale.x),
            y = ScreenInfo.GetWorldPosition(ScreenBoundary.TopLeft).y + spawnedObj.transform.localScale.y,
            z = 0
        };
    }

    private SpawnableObjectsType GetRandomType()
    {
        int randomChance = Random.Range(0, 100);
        SpawnableObjectsType type = _chanceOfGiftSpawn >= randomChance ? SpawnableObjectsType.Gift : SpawnableObjectsType.Snowball;

        return type;
    }
}
