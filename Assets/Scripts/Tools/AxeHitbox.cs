using UnityEngine;

public class AxeHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tree"))
        {
            other.GetComponent<WoodTree>()?.TakeHit();
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<SkeletonEnemy>()?.TakeHit();
        }
    }
}
