using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuIntroEffectController : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public Image backgroundImage;
    public float fadeSpeed = 1f;
    public float moveSpeed = 200f;
    public float startYOffset = 100f;

    CanvasGroup titleGroup;
    RectTransform backgroundRect;
    Vector2 targetPos;
    bool done = false;

    void Start()
    {
        if (titleText != null)
        {
            titleGroup = titleText.GetComponent<CanvasGroup>();
            if (titleGroup == null) titleGroup = titleText.gameObject.AddComponent<CanvasGroup>();
            titleGroup.alpha = 0f;
        }

        if (backgroundImage != null)
        {
            backgroundRect = backgroundImage.rectTransform;
            targetPos = backgroundRect.anchoredPosition;
            backgroundRect.anchoredPosition = targetPos + Vector2.up * startYOffset;
        }
    }

    void Update()
    {
        if (done) return;

        bool finishedFade = false;
        bool finishedMove = false;

        if (titleGroup != null)
        {
            titleGroup.alpha = Mathf.MoveTowards(titleGroup.alpha, 1f, fadeSpeed * Time.deltaTime);
            finishedFade = Mathf.Abs(titleGroup.alpha - 1f) < 0.01f;
        }

        if (backgroundRect != null)
        {
            Vector2 newPos = backgroundRect.anchoredPosition;
            newPos.y = Mathf.MoveTowards(newPos.y, targetPos.y, moveSpeed * Time.deltaTime);
            backgroundRect.anchoredPosition = newPos;
            finishedMove = Mathf.Abs(newPos.y - targetPos.y) < 0.1f;
        }

        if (finishedFade && finishedMove)
            done = true;
    }
}
