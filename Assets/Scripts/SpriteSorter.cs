using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSorter : MonoBehaviour
{
    public int offset = 0;
    public int baseOrder = 1000; // Keeps characters always above ground layer

    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void LateUpdate()
    {
        // Convert Y to sorting order (higher Y = behind)
        sr.sortingOrder = baseOrder - Mathf.RoundToInt(transform.position.y * 100f) + offset;
    }
}