using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSorter : MonoBehaviour
{
    [SerializeField] private int offset = 0;
    [SerializeField] private int baseOrder = 1000;

    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        // Sort based on Y position
        sr.sortingOrder = baseOrder - Mathf.RoundToInt(transform.position.y * 100f) + offset;
    }
}