using UnityEngine;

public class PickaxeHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ore"))
        {
            other.GetComponent<Ore>()?.TakeHit();
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<SkeletonEnemy>()?.TakeHit();
        }
    }
}
