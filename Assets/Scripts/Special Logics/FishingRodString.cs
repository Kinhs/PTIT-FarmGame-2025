using UnityEngine;

public class FishingRodString : MonoBehaviour
{
    public Transform rodRoot;
    public Transform hookTip;
    public Transform stringSprite;
    public float multiplier;

    void Update()
    {
        if (!rodRoot || !hookTip || !stringSprite) return;

        Vector3 dir = hookTip.position - rodRoot.position;
        float length = dir.magnitude;

        stringSprite.position = rodRoot.position + dir * 0.5f;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        stringSprite.rotation = Quaternion.Euler(0, 0, angle);

        Vector3 scale = stringSprite.localScale;
        scale.y = length * multiplier;
        stringSprite.localScale = scale;
    }
}
