using UnityEngine;

public class PickaxeHitbox : MonoBehaviour
{
    private bool hasHit;

    public void ResetHit()
    {
        hasHit = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit)
            return;

        if (other.CompareTag("Ore"))
        {
            hasHit = true;
            other.GetComponent<Ore>()?.TakeHit();
        }
    }
}
