using System.Collections;
using UnityEngine;

public class HitFeedbackComponent : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InvincibleFeedback(float duration)
    {
        StartCoroutine(HandleInvincibleFeedback(duration));
    }

    public void DamagedFeedback()
    {
        StartCoroutine(HandleDamagedFeedback());
    }

    IEnumerator HandleInvincibleFeedback(float duration)
    {
        float startTime = Time.time;

        float lastAlphaChangeTime = Time.time;
        float alphaChangeTime = 0.25f;
        
        bool transparent = true;
        Color c = spriteRenderer.color;

        c.a = 0.2f;

        spriteRenderer.color = c;

        while (startTime + duration >= Time.time)
        {
            if (lastAlphaChangeTime + alphaChangeTime <= Time.time)
            {
                c = spriteRenderer.color;

                if (transparent) c.a = 1f;
                else c.a = 0.2f;

                transparent = !transparent;

                spriteRenderer.color = c;

                lastAlphaChangeTime = Time.time;
            }

            yield return null;
        }

        c = spriteRenderer.color;

        c.a = 1f;

        spriteRenderer.color = c;
    }

    IEnumerator HandleDamagedFeedback()
    {
        Color originalColor = spriteRenderer.color;

        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.15f);

        spriteRenderer.color = originalColor;
    }
}
