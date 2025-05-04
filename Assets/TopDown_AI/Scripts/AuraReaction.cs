using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AuraReaction : MonoBehaviour
{
    [Header("Detection Settings")]
    public float detectionRadius = 5f;
    public LayerMask enemyLayer;

    [Header("Aura Colors")]
    public Color safeColor = Color.white;
    public Color dangerColor = Color.red;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = safeColor;
    }

    void Update()
    {
        DetectEnemies();
    }

    void DetectEnemies()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        if (hits.Length > 0)
        {
            spriteRenderer.color = dangerColor;
        }
        else
        {
            spriteRenderer.color = safeColor;
        }
    }

    // Optional: draw detection radius in scene view
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
