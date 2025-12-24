using UnityEngine;

public class AxeHitbox : MonoBehaviour
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

        if (other.CompareTag("Tree"))
        {
            hasHit = true;
            other.GetComponent<WoodTree>()?.TakeHit();
        }
    }
}
