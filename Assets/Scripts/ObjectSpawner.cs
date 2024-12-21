using System.Collections;
using UnityEngine;
using Zenject;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField][Range(1, 100)] private int _chanceOfGiftSpawn = 75;
    [SerializeField] private float _minTimeBetweenSpawn = 1;
    [SerializeField] private float _maxTimeBetweenSpawn = 3;

    private ObjectFactory _objectFactory;

    private void Start()
    {
        StartSpawn();
    }

    [Inject]
    public void Construct(ObjectFactory objectFactory)
    {
        _objectFactory = objectFactory;
    }

    public void StartSpawn()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(_minTimeBetweenSpawn, _maxTimeBetweenSpawn));

            for (int i = 0; i < Random.Range(0, 4); i++)
            {
                float minDelay = 0.2f;
                float maxDelay = 0.5f;

                yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));

                SpawnableObject spawnedObj = _objectFactory.Get(GetRandomType());
                spawnedObj.gameObject.SetActive(true);
                spawnedObj.transform.position = new Vector3
                {
                    x = Random.Range(ScreenInfo.GetWorldPosition(ScreenBoundary.BottomLeft).x, ScreenInfo.GetWorldPosition(ScreenBoundary.BottomRight).x),
                    y = ScreenInfo.GetWorldPosition(ScreenBoundary.TopLeft).y,
                    z = 0
                };
            }
        }
    }

    private SpawnableObjectsType GetRandomType()
    {
        int randomChance = Random.Range(0, 100);
        SpawnableObjectsType type = _chanceOfGiftSpawn >= randomChance ? SpawnableObjectsType.Gift : SpawnableObjectsType.Snowball;

        return type;
    }
}
