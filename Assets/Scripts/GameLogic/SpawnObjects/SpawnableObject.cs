using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer SpriteRenderer;
    [SerializeField] private float _fallSpeed = 1;
    [field: SerializeField] public SpawnableObjectsType Type { get; private set; }

    private void Update()
    {
        transform.Translate(Vector2.down * _fallSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}