using UnityEngine;

public class FlamePyre : MonoBehaviour
{
    private float elapsed = 0f;
    private float duration = 3f;
    private Vector2 initialScale;
    private Vector2 targetScale;

    void Start()
    {
        Destroy(gameObject, duration);
        initialScale = transform.localScale;
        targetScale = new Vector2(initialScale.x, initialScale.y * 10);
    }

    private void FixedUpdate()
    {
        if (elapsed < duration)
        {
            elapsed += Time.fixedDeltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.localScale = Vector2.Lerp(initialScale, targetScale, t);
        }
    }
}