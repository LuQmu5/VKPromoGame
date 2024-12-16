using UnityEngine;

public abstract class SpawnableObject : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer SpriteRenderer;
    [SerializeField] private float _fallSpeed = 1;
    [SerializeField] private Rigidbody2D _rigidbody;

    [field: SerializeField] public SpawnableObjectsType Type { get; private set; }

    public float SizeX => SpriteRenderer.size.x;

    private void Start()
    {
        _rigidbody.gravityScale = _fallSpeed;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}