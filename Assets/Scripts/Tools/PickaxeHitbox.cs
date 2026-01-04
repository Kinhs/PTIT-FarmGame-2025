using UnityEngine;

public class PickaxeHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ore"))
        {
            Ore ore = other.GetComponent<Ore>();
            if (ore != null)
            {
                if (!ore.isDepleted) PlayerController.instance.GetTired(5);
                ore.TakeHit();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<SkeletonEnemy>()?.TakeHit();
        }
    }
}
