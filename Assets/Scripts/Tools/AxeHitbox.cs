using UnityEngine;

public class AxeHitbox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tree"))
        {
            WoodTree tree = other.GetComponent<WoodTree>();
            if (tree != null)
            {
                if (!tree.isChopped) PlayerController.instance.GetTired(5);
                tree.TakeHit();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            other.GetComponent<SkeletonEnemy>()?.TakeHit();
        }
    }
}
