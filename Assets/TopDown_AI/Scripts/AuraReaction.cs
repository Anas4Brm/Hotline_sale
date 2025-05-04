using UnityEngine;

public class AuraReaction : MonoBehaviour
{
    public float detectionRadius = 5f;
    public LayerMask enemyLayer;
    public Color safeColor = Color.white;
    public Color dangerColor = Color.red;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Collider2D enemy = Physics2D.OverlapCircle(transform.position, detectionRadius, enemyLayer);

        if (enemy)
        {
            sr.color = dangerColor;
        }
        else
        {
            sr.color = safeColor;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
