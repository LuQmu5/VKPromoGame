using UnityEngine;

public class Gift : SpawnableObject
{
    [SerializeField] private Sprite[] _sprites;

    private void OnEnable()
    {
        SpriteRenderer.sprite = _sprites[Random.Range(0, _sprites.Length)];
    }
}
