using UnityEngine;

public class FloatingCloud : MonoBehaviour
{
    public float speed;
    public float offset;

    RectTransform rectTransform;
    RectTransform canvasRect;
    float leftEdge;
    float rightEdge;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        float halfWidth = canvasRect.rect.width / 2f;
        leftEdge = -halfWidth - rectTransform.rect.width / 2f - offset;
        rightEdge = halfWidth + rectTransform.rect.width / 2f + offset;
    }

    void Update()
    {
        rectTransform.anchoredPosition += Vector2.left * speed * Time.deltaTime;
        if (rectTransform.anchoredPosition.x < leftEdge)
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos.x = rightEdge;
            rectTransform.anchoredPosition = pos;
        }
    }
}
